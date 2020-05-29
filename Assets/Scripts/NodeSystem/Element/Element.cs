﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NodeSystem
{
    public abstract class Element
    {
        public Rect parent;
        public bool IsBeingDragged => isBeingDragged;
        public bool IsSelected => isSelected;
		public Vector2 SetStartPosition(Vector2 startPosition) => this.startPosition = startPosition;
		public Vector2 GetStartPosition => startPosition;
        public int DrawOrder => drawOrder;
        public virtual Rect Rect => rect;
        public virtual Vector2 Position => parent.position + rect.position;

        public virtual Vector2 LocalPosition
        {
            get => rect.position;
            protected set => rect.position = value;
        }
        public Vector2 Size => rect.size;


        protected Vector2 startPosition;
        protected Rect rect = new Rect();
        protected SystemEventHandeler eventHandeler;
        protected bool isBeingDragged;

        private bool isSelected;
        private int drawOrder = 0;
        private Dictionary<EventType, Action<Event>> eventTypes = new Dictionary<EventType, Action<Event>>();

        public virtual void Init(Vector2 position,  SystemEventHandeler eventHandeler)
        {
            parent = new Rect();
			startPosition = position;
			Vector2 size = new Vector2(100, 100);
            rect = new Rect(position, size);

            eventHandeler.OnElementCreate?.Invoke(this);
            eventHandeler.OnGui += ProcessEvents;

            eventTypes.Add(EventType.MouseDown, (Event e) =>
            {
                if (e.button == 0)
                {
                    if (Rect.Contains(e.mousePosition))
                    {
                        OnClickDown();
                    }
                    else
                    {
                        GUI.changed = true;
                        isSelected = false;
                    }
                }
            });

            eventTypes.Add(EventType.MouseUp, (Event e) =>
            {
                OnClickUp();
            });

            eventTypes.Add(EventType.MouseDrag, (Event e) =>
            {
                if (Rect.Contains(e.mousePosition))
                {
                    OnHold(e.delta);
                }
            });

            this.eventHandeler = eventHandeler;
        }

        public void ProcessEvents(Event e)
        {
            EventType type = e.type;

            if (eventTypes.ContainsKey(type))
            {
                eventTypes[type].Invoke(e);
            }
        }

		public virtual void Destroy()
        {
            eventHandeler.OnGui -= ProcessEvents;
            eventHandeler.OnElementRemove?.Invoke(this);
        }

        public virtual void OnClickDown()
        {
            eventHandeler.OnElementClick.Invoke(this);
            isBeingDragged = true;
            GUI.changed = true;
            isSelected = true;
        }

        public virtual void OnClickUp()
        {
            eventHandeler.OnElementRelease.Invoke(this);
            isBeingDragged = false;
        }

        public virtual void OnHold(Vector2 delta)
        {
            eventHandeler.OnElementHold.Invoke(this);
        }

        public abstract void Draw();
    }
}


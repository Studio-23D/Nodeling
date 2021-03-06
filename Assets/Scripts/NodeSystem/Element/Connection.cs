﻿using System;
using UnityEditor;
using UnityEngine;

namespace NodeSystem {
	public class Connection : Element
	{
		public bool IsConnected => isConnected;

		private ConnectionPoint inPoint;
		private ConnectionPoint outPoint;	
		private Type type;

		private bool isConnected = false;

		public ConnectionPoint InPoint
		{
			get => inPoint;
			set
			{
				inPoint = value;
				type = type ?? value.Value.FieldType;
			}
		}
		public ConnectionPoint OutPoint
		{
			get => outPoint;
			set
			{
				outPoint = value;
				type = type ?? value.Value.FieldType;
			}
		}

		public void SetValue()
		{
			if (!isConnected) return;

			inPoint.Value.SetValue(inPoint.node, outPoint.Value.GetValue(outPoint.node));
		}

		public override void Destroy()
		{
			inPoint = null;
			outPoint = null;
			base.Destroy();
		}

		public void Connect(Element element)
		{
			if (!(element is ConnectionPoint)) return;

			ConnectionPoint point = element as ConnectionPoint;
			ConnectionPoint otherPoint = (inPoint != null) ? inPoint : outPoint;

			if (point.type == otherPoint.type || point.Value.FieldType != type)
			{
				Destroy();
				return;
			}

			switch (point.type)
			{
				case ConnectionPointType.In:
					inPoint = point;
					break;
				default:
					outPoint = point;
					break;
			}

			point.OnConnection(this);
			isConnected = true;
			this.SetValue();
			outPoint.node.OnChange += this.SetValue;
			outPoint.node.OnChange += inPoint.node.CalculateChange;
			InPoint.node.CalculateChange();
		}

		public override void Draw()
		{
			// KEEPS DRAWING EVENTHOUGH IT IS DESTROYED

			Vector2 positionA = (outPoint != null) ? outPoint.Rect.position + outPoint.Size / new Vector2(2 ,2) : eventHandeler.MousePosition;
			Vector2 positionB = (inPoint != null) ? inPoint.Rect.position + inPoint.Size / new Vector2(2, 2) : eventHandeler.MousePosition;

			GuiLineRenderer.DrawLine(positionA, positionB, Color.black, 2);
		}
	}
}

﻿using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

[System.Serializable]
public struct TouchActions
{
	public string action;
	public int touchCount;
	public float touchTime;
}

public class InputManager : MonoBehaviour
{
	public Action<int, Vector3, Transform> OnTouch;
	public Action<float> OnScroll;

	public Transform SelectedView => selectedView;
	public bool IsMouseButtonDown => Input.GetMouseButton(0);
	public bool IsMouseButtonPressed => Input.GetMouseButtonDown(0);
	public Vector3 GetTouchPos => Camera.main.ViewportToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.nearClipPlane));
	public int GetTouchCount => Input.touchCount;

	[SerializeField] private List<TouchActions> touchbindings = new List<TouchActions>();
	[SerializeField] private string ScrollWheelAxis = "Mouse ScrollWheel";
	[SerializeField] private Transform selectedView;

	private Camera cam;
	private EventSystem events;
	private Vector3 touchStartPos;

	private void Awake()
	{
		events = EventSystem.current;
	}

	private void Start()
	{
		Init();
	}

	private void Update()
	{
		if (IsMouseButtonPressed)
		{
			touchStartPos = GetTouchPos;
		}

		if (GetTouchCount > 1)
		{
			OnTouch(GetTouchCount, touchStartPos, selectedView);
		}
		else if (IsMouseButtonDown)
		{
			if (OnTouch != null)
				OnTouch(GetTouchCount, touchStartPos, selectedView);
		}

		if (OnScroll != null)
			OnScroll(-Input.GetAxis(ScrollWheelAxis));
	}

	public void SelectView(Transform view)
	{
		selectedView = view;
	}

	public void DeslectView()
	{
		selectedView = null;
	}


	#region EDITOR

	void OnGUI()
	{
		/*if (events.IsPointerOverGameObject())
		{
			Vector3 point = new Vector3();
			Event currentEvent = Event.current;
			Vector2 mousePos = new Vector2();

			// Get the mouse position from Event.
			// Note that the y position from Event is inverted.
			mousePos.x = currentEvent.mousePosition.x;
			mousePos.y = cam.pixelHeight - currentEvent.mousePosition.y;

			point = GetTouchPos;

			GUILayout.BeginArea(new Rect(20, 20, 250, 120));
			GUILayout.Label("Screen pixels: " + cam.pixelWidth + ":" + cam.pixelHeight);
			GUILayout.Label("Mouse position: " + mousePos);
			GUILayout.Label("World position: " + point.ToString("F3"));
			GUILayout.EndArea();
		}*/
	}

	#endregion


	private void Init()
	{
		cam = Camera.main;
	}
}

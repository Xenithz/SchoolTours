﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobileTouchInput : MonoBehaviour 
{
	private RaycastHit hit;

	[SerializeField]
	private CanvasManager myCanvasManager;

	private void Update()
	{
		ProcessInput();
	}

	private void ProcessInput()
	{
		foreach(Touch myTouchReg in Input.touches)
		{
			Debug.Log("test");
			if(myTouchReg.phase == TouchPhase.Began)
			{
				Ray myRay = Camera.main.ScreenPointToRay(myTouchReg.position);

				if(Physics.Raycast(myRay, out hit))
				{
					if(hit.transform.gameObject.tag == "VirtualButton")
					{
						Execute(hit);
					}
				}
			}
		}

		if(Input.GetMouseButtonDown(0))
		{
			Ray myRay = Camera.main.ScreenPointToRay(Input.mousePosition);

				if(Physics.Raycast(myRay, out hit))
				{
					Debug.Log("Hit something");
					if(hit.transform.gameObject.tag == "VirtualButton")
					{
						Debug.Log("Hit button");
						Debug.Log(hit.transform.gameObject.name);
						Execute(hit);
					}
				}
		}
	}

	private void Execute(RaycastHit hitToCheck)
	{
		switch (hitToCheck.transform.gameObject.name)
		{
			case "Test":
				Debug.Log("Ding");
				break;
			case "Fees":
				myCanvasManager.SetCurrentCanvas(myCanvasManager.canvasArray[0]);
				break;
			case "PastAcademics":
				myCanvasManager.SetCurrentCanvas(myCanvasManager.canvasArray[1]);
				break;
			case "Applications":
				myCanvasManager.SetCurrentCanvas(myCanvasManager.canvasArray[2]);
				break;
			default:
				break;
		}
	}
}

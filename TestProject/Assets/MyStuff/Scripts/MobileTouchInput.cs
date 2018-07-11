using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobileTouchInput : MonoBehaviour 
{
	private RaycastHit hit;

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
		if(CanvasManager.instance.canvasDictionary.ContainsKey(hitToCheck.transform.gameObject.name))
		{
			CanvasManager.instance.SetCurrentCanvas(CanvasManager.instance.canvasDictionary[hitToCheck.transform.gameObject.name]);
		}
	}
}

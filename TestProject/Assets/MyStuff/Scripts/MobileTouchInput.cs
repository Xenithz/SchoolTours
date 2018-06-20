using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobileTouchInput : MonoBehaviour 
{
	private RaycastHit hit;

	private void Update()
	{
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
					Execute(hit);
				}
			}
		}
	}

	private void Execute(RaycastHit hitToCheck)
	{
		switch (hitToCheck.transform.gameObject.tag)
		{
			case "Test":
				Debug.Log("Hit");
				break;
			default:
				break;
		}
	}
}

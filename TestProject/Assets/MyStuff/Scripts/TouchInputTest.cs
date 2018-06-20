using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchInputTest : MonoBehaviour 
{
	public GameObject objectToCreate;
	private RaycastHit hit;

	private void Update()
	{
		foreach(Touch myTouchReg in Input.touches)
		{
			Debug.Log("test");
			if(myTouchReg.phase == TouchPhase.Began)
			{
				Ray myRay = Camera.main.ScreenPointToRay(myTouchReg.position);

				if(Physics.Raycast(myRay, out hit))
				{
					Instantiate(objectToCreate, hit.point, Quaternion.identity);
				}
			}
		}
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasManager : MonoBehaviour 
{
	//TODO: Make it use animation instead of just setting gameobject as inactive

	public GameObject CurrentCanvas
	{
		get
		{
			return currentCanvas;
		}
	}

	[SerializeField]
	private GameObject currentCanvas;

	public GameObject[] canvasArray;
	/*
		0 = Fees
		1 = PastAcademics
	 */

	private void Start()
	{
		currentCanvas = null;
	}

	public void SetCurrentCanvas(GameObject canvasToSet)
	{
		Debug.Log("Setting canvas");
		currentCanvas = canvasToSet;
		currentCanvas.SetActive(true);
	}

	public void CloseCurrentCanvas()
	{
		currentCanvas.SetActive(false);
		currentCanvas = null;
	}
}

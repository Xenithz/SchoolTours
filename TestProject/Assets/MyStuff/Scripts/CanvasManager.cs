using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

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

	[SerializeField]
	private GameObject[] buttonArray;

	[SerializeField]
	private GameObject[] videoButtonArray;

	public GameObject[] canvasArray;
	/*
		0 = Fees
		1 = PastAcademics
		2 = Applications
	 */

	private void Start()
	{
		currentCanvas = null;
		buttonArray = GameObject.FindGameObjectsWithTag("VirtualButton");
	}

	public void SetCurrentCanvas(GameObject canvasToSet)
	{
		Debug.Log("Setting canvas");

		for(int i = 0; i < buttonArray.Length; i++)
		{
			buttonArray[i].SetActive(false);
		}

		currentCanvas = canvasToSet;
		currentCanvas.SetActive(true);
	}

	public void CloseCurrentCanvas()
	{
		for(int i = 0; i < buttonArray.Length; i++)
		{
			buttonArray[i].SetActive(true);
		}
		currentCanvas.SetActive(false);
		currentCanvas = null;
	}

	public void CloseCurrentVideoCanvas()
	{	
		for(int i = 0; i < videoButtonArray.Length; i++)
		{
			videoButtonArray[i].SetActive(true);
		}

		for(int i = 0; i < buttonArray.Length; i++)
		{
			buttonArray[i].SetActive(true);
		}

		var videoHolder = GetComponents<RawImageToVideo>();
		
		for(int i  = 0; i < videoHolder.Length; i++)
		{
			videoHolder[i].GetComponent<VideoPlayer>().Stop();
			videoHolder[i].videoInitCheck = true;
		}

		currentCanvas.SetActive(false);
		currentCanvas = null;
	}
}

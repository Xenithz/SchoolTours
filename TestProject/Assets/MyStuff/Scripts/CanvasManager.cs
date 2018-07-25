using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class CanvasManager : MonoBehaviour 
{
	public GameObject CurrentCanvas
	{
		get
		{
			return currentCanvas;
		}
	}

	[SerializeField]
	private GameObject currentCanvas = null;

	[SerializeField]
	private GameObject[] buttonArray;

	public GameObject mainCanvas;

	 public Mask[] panelsToAdd;

	[SerializeField]
	public Dictionary<string, GameObject> canvasDictionary;

	public Sprite playSprite;

	public Sprite resumeSprite;

	static public CanvasManager instance;

	private void Start()
	{
		instance = this;
		buttonArray = GameObject.FindGameObjectsWithTag("VirtualButton");
		canvasDictionary = new Dictionary<string, GameObject>();
        panelsToAdd = mainCanvas.GetComponentsInChildren<Mask>(true);
		//removed below to get rid of masks
        /*for(int i = 0; i < panelsToAdd.Length; i++)
		{
			GameObject myGameObjectToAdd = panelsToAdd[i].gameObject;
			canvasDictionary.Add(myGameObjectToAdd.name, myGameObjectToAdd);
		}*/
        for (int i = 0; i < mainCanvas.transform.childCount; i++)
        {
            GameObject myGameObjectToAdd = mainCanvas.transform.GetChild(i).gameObject;
            canvasDictionary.Add(myGameObjectToAdd.name, myGameObjectToAdd);
        }
		
	}

	public void SetCurrentCanvas(GameObject canvasToSet)
	{
		Debug.Log("Setting canvas");

		for(int i = 0; i < buttonArray.Length; i++)
		{
			buttonArray[i].SetActive(false);
		}
        if(currentCanvas!=null)currentCanvas.SetActive(false);
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

    public void RenableButtons()
    {
        for (int i = 0; i < buttonArray.Length; i++)
        {
            buttonArray[i].SetActive(true);
        }
    }
    public void DisableButtons()
    {
        for (int i = 0; i < buttonArray.Length; i++)
        {
            buttonArray[i].SetActive(false);
        }
    }

}
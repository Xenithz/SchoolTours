using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelEnlarge : MonoBehaviour 
{
	[SerializeField]
	private GameObject leftPanel;

	[SerializeField]
	private GameObject rightPanel;
	
	[SerializeField]
	private GameObject middlePanel;

	[SerializeField]
	private GameObject[] uiElementsArray;


	public void EnlargeThePanel()
	{
		if(this.gameObject.tag == "Left")
		{
			Debug.Log("left animation");
			for (int i = 0; i < uiElementsArray.Length; i++)
			{
				if(uiElementsArray[i] != leftPanel)
				{
					uiElementsArray[i].SetActive(false);
				}
			}
			leftPanel.GetComponent<Animator>().SetBool("Left", true);
		}
		else if(this.gameObject.tag == "Right")
		{
			Debug.Log("right animation");
			for (int i = 0; i < uiElementsArray.Length; i++)
			{
				if(uiElementsArray[i] != rightPanel)
				{
					uiElementsArray[i].SetActive(false);
				}
			}
			rightPanel.GetComponent<Animator>().SetBool("Right", true);
		}
		else if(this.gameObject.tag == "Middle")
		{
			Debug.Log("middle animation");
			for (int i = 0; i < uiElementsArray.Length; i++)
			{
				if(uiElementsArray[i] != middlePanel)
				{
					uiElementsArray[i].SetActive(false);
				}
			}
			middlePanel.GetComponent<Animator>().SetBool("Middle", true);
		}
	}
}

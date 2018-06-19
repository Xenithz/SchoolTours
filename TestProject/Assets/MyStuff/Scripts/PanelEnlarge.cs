using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelEnlarge : MonoBehaviour 
{
	[SerializeField]
	private GameObject LeftPanel;

	[SerializeField]
	private GameObject RightPanel;
	
	[SerializeField]
	private GameObject MiddlePanel;


	public void EnlargeThePanel()
	{
		if(this.gameObject.tag == "Left")
		{
			Debug.Log("left animation");
			LeftPanel.GetComponent<Animator>().SetBool("Left", true);
		}
		else if(this.gameObject.tag == "Right")
		{
			Debug.Log("right animation");
			RightPanel.GetComponent<Animator>().SetBool("Right", true);
		}
		else if(this.gameObject.tag == "Middle")
		{
			Debug.Log("middle animation");
			MiddlePanel.GetComponent<Animator>().SetBool("Middle", true);
		}
	}
}

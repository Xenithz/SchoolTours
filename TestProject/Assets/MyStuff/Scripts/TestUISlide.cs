using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestUISlide : MonoBehaviour 
{
	public Animator[] myAnimators;

	private void Start()
	{
		myAnimators = GetComponentsInChildren<Animator>();
		
		for(int i = 0; i < myAnimators.Length; i++)
		{
			myAnimators[i].SetBool("SlideOut", true);
		}
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestUISlide : MonoBehaviour 
{
	[SerializeField]
	private Button[] buttonHolder;
	[SerializeField]
	private Vector2[] startPositions;
	[SerializeField]
	private Vector2[] endPositions;
	[SerializeField]
	private float distanceFactor;

	public Animator[] myAnimators;

	private enum States
	{
		ready,
		finished,
		back,
	}

	private States myState;

	private void Start()
	{
		buttonHolder = GetComponentsInChildren<Button>();
		startPositions = new Vector2[buttonHolder.Length];
		endPositions = new Vector2[startPositions.Length];

		for(int i = 0; i < buttonHolder.Length; i++)
		{
			startPositions[i] = buttonHolder[i].GetComponent<RectTransform>().anchoredPosition;
			Vector2 storageVector = startPositions[i];
			endPositions[i] = new Vector2(storageVector.x + distanceFactor, storageVector.y);
		}
		myState = States.ready;
		// myAnimator.SetBool("SlideOut", true);
		for(int i = 0; i < myAnimators.Length; i++)
		{
			myAnimators[i].SetBool("SlideOut", true);
		}
	}

	private void Update()
	{
		if(myState == States.ready)
		{
			Slide();
		}
	}

	private void Slide()
	{
	}
}

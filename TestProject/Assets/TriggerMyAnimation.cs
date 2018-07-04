using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerMyAnimation : MonoBehaviour 
{
	public Animator myAnimationController;

	public Animator mySecondAnimationController;

	private bool shouldTime;

	public float myCurrentTime;

	public float targetTime;

	private void OnEnable()
	{
		myCurrentTime = 0f;
		shouldTime = true;
	}

	private void OnDisable()
	{
		shouldTime = false;
	}

	private void Update()
	{
		if(shouldTime == true)
		{
			Timer();
		}
	}

	private void Timer()
	{
		myCurrentTime += Time.deltaTime;
		if(myCurrentTime >= targetTime)
		{
			myAnimationController.SetBool("shouldGo", true);
			mySecondAnimationController.SetBool("shouldGo", true);
		}
	}
}

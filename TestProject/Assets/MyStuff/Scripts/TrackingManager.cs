﻿using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Rendering;


public enum TrackingState { Tracking, Waiting, Lost }

public class TrackingManager : MonoBehaviour 
{
	public GameObject receptionObject;
	public string receptionID = "4321";
    public Animator receptionAnimator;

	public GameObject blankObject;
	public string blankID = "1234";

	public GameObject beakerObject;
	public string beakerID = "1222";
    public Animator beakerAnimator;

	public GameObject trayObject;
	public string trayID = "1333";
    public Animator trayAnimator;

	public GameObject boyObject;
	public string boyID = "1444";
    public Animator boyAnimator;

    public float resetDuration;
    float timer;
    public TrackingState curState;


    public GameObject depthMask;

    static public TrackingManager instance;
    MessageManager messenger;
    public GameObject VuMark;
    GameObject detectedGameObject;

    private void Start()
    {

        messenger = FindObjectOfType<MessageManager>();
        instance = this;
        curState = TrackingState.Lost;
        if (messenger != null) messenger.ShowMessage("Please aim your camera at our poster");
    }


    public void Update()
    {
        switch (curState)
        {
            case TrackingState.Waiting:
                {

                    Debug.Log("testo");
                    timer += Time.deltaTime;
                    if (timer > resetDuration)
                    {

                        Animator[] myAnimatorContainer = VuMark.GetComponentsInChildren<Animator>();
                        for (int i = 0; i < myAnimatorContainer.Length; i++)
                        {
                            //Debug.Log(i);
                            myAnimatorContainer[i].SetBool("shouldPlay", false);
                            myAnimatorContainer[i].SetTrigger("shouldGoBack");
                            myAnimatorContainer[i].SetBool("shouldMove", false);
                        }
                        curState = TrackingState.Lost;
                        if (messenger != null) messenger.ShowMessage("Please aim your camera at our poster");
                    }
                    break;
                }


            default:
                break;
        }
    }

    public void DisableObjects(){

        curState = TrackingState.Waiting;
        //TODO ... check if you need to disable objects.
    }

	public void EnableObjects(GameObject gameObjectToPass)
	{
        detectedGameObject = gameObjectToPass;
		var rendererComponents = gameObjectToPass.GetComponentsInChildren<Renderer>(true);
        var colliderComponents = gameObjectToPass.GetComponentsInChildren<Collider>(true);
        var canvasComponents = gameObjectToPass.GetComponentsInChildren<Canvas>(true);
        var depthMaskComponent = depthMask.GetComponent<Renderer>();
                
        foreach(var component in rendererComponents)
		{
			component.shadowCastingMode = ShadowCastingMode.Off;
			component.enabled = true;
            if(component.gameObject.tag == "VirtualButton")
            {
                component.enabled = false;
            }
		}		

        foreach(var component in colliderComponents)
            component.enabled = true;

        foreach(var component in canvasComponents)
            component.enabled = true;

        
        depthMaskComponent.enabled = true;

            Animator[] myAnimatorContainer = gameObjectToPass.GetComponentsInChildren<Animator>();
        //if(curState!=TrackingState.Waiting)
        {
            Debug.Log(myAnimatorContainer.Length);
            for(int i = 0; i < myAnimatorContainer.Length; i++)
            {
                myAnimatorContainer[i].SetBool("shouldMove", true);
            }
        }    
        if (curState == TrackingState.Waiting)
        {
            for (int i = 0; i < myAnimatorContainer.Length; i++)
            {
                myAnimatorContainer[i].SetTrigger("Skip");
            }
        }
        ResetTracking();
        if (messenger != null) messenger.HideMessage();
	}

    public void ResetTracking(){
        curState = TrackingState.Tracking;
        timer = 0;
    }
}
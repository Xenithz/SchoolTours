using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Rendering;


public enum TrackingState { Tracking, Waiting, Lost }

public class TrackingManager : MonoBehaviour 
{
	public GameObject receptionObject;
    public GameObject receptionTrigger;
	public string receptionID = "4321";
    public Animator receptionAnimator;

	public GameObject blankObject;
	public string blankID = "1234";

	public GameObject beakerObject;
    public GameObject beakerTrigger;
	public string beakerID = "1222";
    public Animator beakerAnimator;

	public GameObject trayObject;
    public GameObject trayTrigger;
	public string trayID = "1333";
    public Animator trayAnimator;

	public GameObject boyObject;
    public GameObject boyTrigger;
	public string boyID = "1444";
    public Animator boyAnimator;
    public Animator grafittiAnimator;

    public float resetDuration;
    float timer;
    public TrackingState curState;


    public GameObject depthMask;

    static public TrackingManager instance;
    MessageManager messenger;
    public GameObject VuMark;
    public GameObject detectedGameObject;
    Animator mainController;

    private void Start()
    {

        messenger = FindObjectOfType<MessageManager>();
        instance = this;
        curState = TrackingState.Lost;
        if (messenger != null)
        {
            messenger.ShowMessage("Please aim your camera at our poster");
        }
    }


    public void Update()
    {
        switch (curState)
        {
            case TrackingState.Waiting:
            {
                    Debug.Log("test");
                    timer += Time.deltaTime;
                    if (timer > resetDuration)
                    {
                        Animator[] myAnimatorContainer = VuMark.GetComponentsInChildren<Animator>();
                        for (int i = 0; i < myAnimatorContainer.Length; i++)
                        {
                            myAnimatorContainer[i].SetBool("shouldPlay", false);
                            myAnimatorContainer[i].SetTrigger("shouldGoBack");
                            myAnimatorContainer[i].SetBool("shouldMove", false);
                            myAnimatorContainer[i].SetTrigger("shouldGoDefault");
                        }
                        curState = TrackingState.Lost;
                    }
                    break;
            }


            default:
                break;
        }
    }

    public void DisableObjects(Vuforia.VuMarkTarget vumarkTargetTracked)
    {
        // foreach(Transform child in detectedGameObject.transform)
        // {
        //     if(child.GetComponent<Animator>() != null)
        //     {
        //         Animator temp = child.GetComponent<Animator>();
        //         //No built in method to check if animator param exists
        //         if(temp.GetBool("shouldPlay") == true || temp.GetBool("shouldPlay") == false )
        //         {
        //             mainController = temp;
        //         }
        //     }
        // }

        // if(mainController.GetCurrentAnimatorStateInfo(0).IsName("main"))
        // {
        //     curState = TrackingState.Waiting;
        // }

        curState = TrackingState.Waiting;
        
        if (messenger != null && !SlidingWindow.IsOpen())
        {
            messenger.ShowMessage("Please aim your camera at our poster");
        }


        if (vumarkTargetTracked.InstanceId.StringValue == TrackingManager.instance.receptionID)
        {
            TrackingManager.instance.receptionObject.SetActive(false);
        }

        else if (vumarkTargetTracked.InstanceId.StringValue == TrackingManager.instance.blankID)
        {
            TrackingManager.instance.blankObject.SetActive(false);
        }

        else if (vumarkTargetTracked.InstanceId.StringValue == TrackingManager.instance.beakerID)
        {
            TrackingManager.instance.beakerObject.SetActive(false);
        }

        else if (vumarkTargetTracked.InstanceId.StringValue == TrackingManager.instance.trayID)
        {
            TrackingManager.instance.trayObject.SetActive(false);
        }

        else if (vumarkTargetTracked.InstanceId.StringValue == TrackingManager.instance.boyID)
        {
            TrackingManager.instance.boyObject.SetActive(false);
        }
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
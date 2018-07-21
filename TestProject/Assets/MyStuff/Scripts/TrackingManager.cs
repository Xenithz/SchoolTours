using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Rendering;

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

    static public TrackingManager instance;

    private void Start()
    {
        instance = this;
    }

	public void EnableObjects(GameObject gameObjectToPass, GameObject animatedVumark)
	{
		var rendererComponents = gameObjectToPass.GetComponentsInChildren<Renderer>(true);
        var colliderComponents = gameObjectToPass.GetComponentsInChildren<Collider>(true);
        var canvasComponents = gameObjectToPass.GetComponentsInChildren<Canvas>(true);
                
        foreach(var component in rendererComponents)
		{
			component.shadowCastingMode = ShadowCastingMode.Off;
			component.enabled = true;
		}		

        foreach(var component in colliderComponents)
            component.enabled = true;

        foreach(var component in canvasComponents)
            component.enabled = true;


        foreach (var component in rendererComponents)
        {
            //component.enabled = true;
            if(component.gameObject.tag == "VirtualButton")
            {
                component.enabled = false;
            }
            else
            {
                component.enabled = true;
            }
        }

        if(animatedVumark.tag == "Animated")
        {
            Animator[] myAnimatorContainer = gameObjectToPass.GetComponentsInChildren<Animator>();
            //Debug.Log(myAnimatorContainer.Length);
            for(int i = 0; i < myAnimatorContainer.Length; i++)
            {
                myAnimatorContainer[i].SetBool("shouldPlay", true);
            }
        }
        else if(animatedVumark.tag == "SpecAnimated")
        {
            Animator[] myAnimatorContainer = gameObjectToPass.GetComponentsInChildren<Animator>();
            Debug.Log(myAnimatorContainer.Length);
            for(int i = 0; i < myAnimatorContainer.Length; i++)
            {
                myAnimatorContainer[i].SetBool("shouldMove", true);
            }
        }      
	}
}
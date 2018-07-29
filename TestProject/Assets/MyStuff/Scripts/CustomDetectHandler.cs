using UnityEngine;
using UnityEngine.UI;
using Vuforia;

public class CustomDetectHandler : DefaultTrackableEventHandler
{
    private VuMarkTarget myVumark;

    private bool testingMode;

    protected override void Start()
    {
        testingMode = false;

        mTrackableBehaviour = GetComponent<TrackableBehaviour>();
        if (mTrackableBehaviour)
            mTrackableBehaviour.RegisterTrackableEventHandler(this);
    }

    protected override void OnTrackingFound()
    {
        if(testingMode)
        {
            var rendererComponents = GetComponentsInChildren<Renderer>(true);

            base.OnTrackingFound();
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

            if(gameObject.tag == "SpecAnimated")
            {
                Animator[] myAnimatorContainer = GetComponentsInChildren<Animator>();
                Debug.Log(myAnimatorContainer.Length);
                for(int i = 0; i < myAnimatorContainer.Length; i++)
                {
                    myAnimatorContainer[i].SetBool("shouldMove", true);
                }
            }   
        }
        else
        {
            //base.OnTrackingFound();
             myVumark = GetComponent<VuMarkBehaviour>().VuMarkTarget;

            Debug.Log("Current VuMark ID: " + " " + myVumark.InstanceId.StringValue);

            if(myVumark.InstanceId.StringValue == TrackingManager.instance.receptionID)
            {
                TrackingManager.instance.receptionObject.SetActive(true);
                TrackingManager.instance.EnableObjects(TrackingManager.instance.receptionObject, gameObject);
            }

            else if(myVumark.InstanceId.StringValue == TrackingManager.instance.blankID)
            {
                TrackingManager.instance.blankObject.SetActive(true);
                TrackingManager.instance.EnableObjects(TrackingManager.instance.blankObject, gameObject);
            }

            else if(myVumark.InstanceId.StringValue == TrackingManager.instance.beakerID)
            {
                TrackingManager.instance.beakerObject.SetActive(true);
                TrackingManager.instance.EnableObjects(TrackingManager.instance.beakerObject, gameObject);
            }

            else if(myVumark.InstanceId.StringValue == TrackingManager.instance.trayID)
            {
                TrackingManager.instance.trayObject.SetActive(true);
                TrackingManager.instance.EnableObjects(TrackingManager.instance.trayObject, gameObject);
            }

            else if(myVumark.InstanceId.StringValue == TrackingManager.instance.boyID)
            {
                TrackingManager.instance.boyObject.SetActive(true);
                TrackingManager.instance.EnableObjects(TrackingManager.instance.boyObject, gameObject);
            }
        }
    }

    protected override void OnTrackingLost()
    {
        if(testingMode)
        {
            base.OnTrackingLost();
            if(gameObject.tag == "SpecAnimated")
            {
                Animator[] myAnimatorContainer = GetComponentsInChildren<Animator>();
                for(int i = 0; i < myAnimatorContainer.Length; i++)
                {
                    //Debug.Log(i);
                    myAnimatorContainer[i].SetBool("shouldPlay", false);
                    myAnimatorContainer[i].SetBool("shouldMove", false);
                }
            }
        }
        else
        {
            if(myVumark != null)
            {
                base.OnTrackingLost();

                myVumark = GetComponent<VuMarkBehaviour>().VuMarkTarget;

                Debug.Log("Current VuMark ID: " + " " + myVumark.InstanceId.StringValue);

                if(myVumark.InstanceId.StringValue == TrackingManager.instance.receptionID)
                {
                    TrackingManager.instance.receptionObject.SetActive(false);
                }

                else if(myVumark.InstanceId.StringValue == TrackingManager.instance.blankID)
                {
                    TrackingManager.instance.blankObject.SetActive(false);
                }

                else if(myVumark.InstanceId.StringValue == TrackingManager.instance.beakerID)
                {
                    TrackingManager.instance.beakerObject.SetActive(false);
                }

                else if(myVumark.InstanceId.StringValue == TrackingManager.instance.trayID)
                {
                    TrackingManager.instance.trayObject.SetActive(false);
                }

                else if(myVumark.InstanceId.StringValue == TrackingManager.instance.boyID)
                {
                    TrackingManager.instance.boyObject.SetActive(false);
                }

                Debug.Log("get lost");
                if(gameObject.tag == "SpecAnimated")
                {
                    Animator[] myAnimatorContainer = GetComponentsInChildren<Animator>();
                    for(int i = 0; i < myAnimatorContainer.Length; i++)
                    {
                        //Debug.Log(i);
                        myAnimatorContainer[i].SetBool("shouldPlay", false);
                        myAnimatorContainer[i].SetTrigger("shouldGoBack");
                        myAnimatorContainer[i].SetBool("shouldMove", false);
                    }
                }
                myVumark = null;
            }
        }
    }
}

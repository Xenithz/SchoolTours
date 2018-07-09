using UnityEngine;
using UnityEngine.UI;
using Vuforia;

public class CustomDetectHandler : DefaultTrackableEventHandler
{
    [SerializeField]
	private CanvasManager myCanvasManager;
    [SerializeField]
    private TrackingManager myTrackingManager;

    private VuMarkManager myVumarkManager;

    private VuMarkTarget myVumark;

    private bool testingMode;

    protected override void Start()
    {
        testingMode = true;
        myVumarkManager = TrackerManager.Instance.GetStateManager().GetVuMarkManager();
        myCanvasManager = GameObject.Find("Holder").GetComponent<CanvasManager>();
        myTrackingManager = GameObject.Find("Holder").GetComponent<TrackingManager>();

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

            if(gameObject.tag == "Animated")
            {
                Animator[] myAnimatorContainer = GetComponentsInChildren<Animator>();
                Debug.Log(myAnimatorContainer.Length);
                for(int i = 0; i < myAnimatorContainer.Length; i++)
                {
                    myAnimatorContainer[i].SetBool("shouldPlay", true);
                }
            }   
        }
        else
        {
            //base.OnTrackingFound();
            myVumark = GetComponent<VuMarkBehaviour>().VuMarkTarget;

            Debug.Log("Current VuMark ID: " + " " + myVumark.InstanceId.StringValue);

            if(myVumark.InstanceId.StringValue == myTrackingManager.receptionID)
            {
                myTrackingManager.EnableObjects(myTrackingManager.receptionObject, gameObject);
            }

            else if(myVumark.InstanceId.StringValue == myTrackingManager.blankID)
            {
                myTrackingManager.EnableObjects(myTrackingManager.blankObject, gameObject);
            }

            else if(myVumark.InstanceId.StringValue == myTrackingManager.beakerID)
            {
                myTrackingManager.EnableObjects(myTrackingManager.beakerObject, gameObject);
            }

            else if(myVumark.InstanceId.StringValue == myTrackingManager.trayID)
            {
                myTrackingManager.EnableObjects(myTrackingManager.trayObject, gameObject);
            }

            else if(myVumark.InstanceId.StringValue == myTrackingManager.boyID)
            {
                myTrackingManager.EnableObjects(myTrackingManager.boyObject, gameObject);
            }
        }
    }

    protected override void OnTrackingLost()
    {
        if(testingMode)
        {

        }
        else
        {
            base.OnTrackingLost();
            Debug.Log("get lost");
            if(gameObject.tag == "Animated")
            {
                Animator[] myAnimatorContainer = GetComponentsInChildren<Animator>();
                for(int i = 0; i < myAnimatorContainer.Length; i++)
                {
                    //Debug.Log(i);
                    myAnimatorContainer[i].SetBool("shouldPlay", false);
                }
            }

            myCanvasManager.CloseCurrentCanvas();
        }
    }
}

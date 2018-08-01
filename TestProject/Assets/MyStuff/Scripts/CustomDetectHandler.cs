using UnityEngine;
using UnityEngine.UI;
using Vuforia;


public class CustomDetectHandler : DefaultTrackableEventHandler
{
    private VuMarkTarget myVumark;
    protected override void Start()
    {
        mTrackableBehaviour = GetComponent<TrackableBehaviour>();
        if (mTrackableBehaviour)
            mTrackableBehaviour.RegisterTrackableEventHandler(this);
    }


    protected override void OnTrackingFound()
    {

        //base.OnTrackingFound();
        myVumark = GetComponent<VuMarkBehaviour>().VuMarkTarget;

        Debug.Log("Current VuMark ID: " + " " + myVumark.InstanceId.StringValue);


        if (myVumark.InstanceId.StringValue == TrackingManager.instance.receptionID)
        {
            TrackingManager.instance.receptionObject.SetActive(true);
            TrackingManager.instance.EnableObjects(TrackingManager.instance.receptionObject);
        }

        else if (myVumark.InstanceId.StringValue == TrackingManager.instance.blankID)
        {
            TrackingManager.instance.blankObject.SetActive(true);
            TrackingManager.instance.EnableObjects(TrackingManager.instance.blankObject);
        }

        else if (myVumark.InstanceId.StringValue == TrackingManager.instance.beakerID)
        {
            TrackingManager.instance.beakerObject.SetActive(true);
            TrackingManager.instance.EnableObjects(TrackingManager.instance.beakerObject);
        }

        else if (myVumark.InstanceId.StringValue == TrackingManager.instance.trayID)
        {
            TrackingManager.instance.trayObject.SetActive(true);
            TrackingManager.instance.EnableObjects(TrackingManager.instance.trayObject);
        }

        else if (myVumark.InstanceId.StringValue == TrackingManager.instance.boyID)
        {
            TrackingManager.instance.boyObject.SetActive(true);
            TrackingManager.instance.EnableObjects(TrackingManager.instance.boyObject);
        }

    }

    protected override void OnTrackingLost()
    {

        base.OnTrackingLost();
        TrackingManager.instance.DisableObjects();
    }
}

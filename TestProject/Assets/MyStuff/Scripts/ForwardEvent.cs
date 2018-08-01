using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForwardEvent : MonoBehaviour 
{
	public Animator deskAnimator;
	public Animator beakerAnimator;
	public Animator boyAnimator;
	public Animator trayAnimator;


	public void SetAnimation()
	{
        //if (TrackingManager.instance.curState != TrackingState.Waiting)
        {
            switch (gameObject.name)
            {
                case "Reception":
                    deskAnimator = TrackingManager.instance.receptionAnimator;
                    deskAnimator.SetBool("shouldPlay", true);
                    break;
                case "Beaker":
                    Debug.Log("hit");
                    beakerAnimator = TrackingManager.instance.beakerAnimator;
                    beakerAnimator.SetBool("shouldPlay", true);
                    break;
                case "Tray":
                    trayAnimator = TrackingManager.instance.trayAnimator;
                    trayAnimator.SetBool("shouldPlay", true);
                    break;
                case "NewBoy":
                    boyAnimator = TrackingManager.instance.boyAnimator;
                    boyAnimator.SetBool("shouldPlay", true);
                    break;
                default:
                    Debug.Log("Error");
                    break;
            }
        }
        //TrackingManager.instance.ResetTracking();
	}
}

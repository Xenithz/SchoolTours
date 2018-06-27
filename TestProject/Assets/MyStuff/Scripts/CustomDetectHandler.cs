using UnityEngine;
using UnityEngine.UI;
using Vuforia;

public class CustomDetectHandler : DefaultTrackableEventHandler
{
    [SerializeField]
	private CanvasManager myCanvasManager;

    protected override void OnTrackingFound()
    {
        base.OnTrackingFound();

        var rendererComponents = GetComponentsInChildren<Renderer>(true);

        // Enable rendering:
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

    protected override void OnTrackingLost()
    {
        base.OnTrackingLost();

        if(gameObject.tag == "Animated")
        {
            Animator[] myAnimatorContainer = GetComponentsInChildren<Animator>();
            for(int i = 0; i < myAnimatorContainer.Length; i++)
            {
                myAnimatorContainer[i].SetBool("shouldPlay", false);
            }
        }

        myCanvasManager.CloseCurrentCanvas();
    }
}

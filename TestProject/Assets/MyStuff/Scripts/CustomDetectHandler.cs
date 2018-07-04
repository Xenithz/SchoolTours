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

    private bool testingMode;

    protected override void Start()
    {
        testingMode = false;
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
            base.OnTrackingFound();
            Vector3 storageVector = myTrackingManager.myCamera.transform.position - gameObject.transform.position;
            float distanceToPass = storageVector.magnitude;
            myTrackingManager.myTrackingDictionary.Add(gameObject, distanceToPass);
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
        else
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
    }

    protected override void OnTrackingLost()
    {
        if(testingMode)
        {
            // if(myTrackingManager.keepOnScreen == false)
            // {
            //     base.OnTrackingLost();
            //     myTrackingManager.myTrackingDictionary.Remove(gameObject);
            //     if(gameObject.tag == "Animated")
            //     {
            //         Animator[] myAnimatorContainer = GetComponentsInChildren<Animator>();
            //         for(int i = 0; i < myAnimatorContainer.Length; i++)
            //         {
            //             myAnimatorContainer[i].SetBool("shouldPlay", false);
            //         }
            //     }

            //     myCanvasManager.CloseCurrentCanvas();
            // }
            // else
            // {
            //     //KEEP EVERYTHING ON SCREEN
            // }

            gameObject.transform.position = myTrackingManager.myCamera.transform.position + myTrackingManager.myCamera.transform.forward * 10f;
            gameObject.transform.SetParent(myTrackingManager.myCamera.transform);
        }
        else
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
}

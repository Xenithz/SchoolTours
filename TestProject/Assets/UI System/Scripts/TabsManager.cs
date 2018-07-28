using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class TabsManager : MonoBehaviour {
    public CircularScroll[] circularScrolls;
    int currentIndex;
    public Button[] tabsButtons;
    public UIElementState managerState;
    Vector3 originalPos;
    Vector3 originalScale;
    //public Transform closedTransform;
    //public Transform openTransform;
    float timer;
    // Use this for initialization
	void Start () {
        //openTransform = transform;
        originalPos = transform.localPosition;
        originalScale = transform.localScale;
        currentIndex = 0;
        circularScrolls = GetComponentsInChildren<CircularScroll>(true);
        for (int i = 0; i <circularScrolls.Length ; i++)
        {
            circularScrolls[i].Init();
            circularScrolls[i].ForceIdle();
        }
        SwitchTab(0);
        for (int i = 1; i < tabsButtons.Length; i++)
        {
            tabsButtons[i].image.color = new Color(1,1,1,0.5f);
        }
        managerState = UIElementState.Active;
    }



    // Update is called once per frame
	void Update () {
        transform.LookAt(Camera.main.transform.position);

        switch (managerState)
        {
            case UIElementState.Active:

        for (int i = 0; i < tabsButtons.Length; i++)
                {
                    float rotDelta = -20 * Mathf.Clamp01(Mathf.Abs(currentIndex - i) / 2f);

                    tabsButtons[i].transform.localRotation = Quaternion.RotateTowards(tabsButtons[i].transform.localRotation, Quaternion.Euler(0, rotDelta, 0), 0.9f);
                }break;

            case UIElementState.TransitionIn:
                transform.localPosition = Vector3.Lerp(Vector3.zero, originalPos, timer);
                transform.localScale = Vector3.Lerp(Vector3.zero, originalScale, timer);
                timer += Time.deltaTime;
                if(timer>=1){
                    timer = 0;
                    managerState = UIElementState.Active;
                }
                break;
            case UIElementState.TransitionOut:
                transform.localPosition = Vector3.Lerp(originalPos, Vector3.zero, timer);
                transform.localScale = Vector3.Lerp(originalPos, Vector3.zero, timer);
                timer += Time.deltaTime;
                if (timer >= 1)
                {
                    timer = 0;
                    managerState = UIElementState.Idle;
                    gameObject.SetActive(false);
                }
                break;

            default:
                break;
        }
	}

    public void SwitchTab(int index){
        circularScrolls[currentIndex].scrollerState = UIElementState.TransitionOut;
        tabsButtons[currentIndex].image.color = new Color(1, 1, 1, 0.5f);
        tabsButtons[index].image.color = new Color(1, 1, 1, 1);
        circularScrolls[index].gameObject.SetActive(true);
        circularScrolls[index].scrollerState = UIElementState.TransitionIn;
        currentIndex = index;
    }

    public void Open()
    {

        CanvasManager.instance.DisableButtons();
        gameObject.SetActive(true);
        timer = 0;
        managerState = UIElementState.TransitionIn;
    }
    public void Close()
    {
        CanvasManager.instance.RenableButtons();
        timer = 0;
        managerState = UIElementState.TransitionOut;

    }
}

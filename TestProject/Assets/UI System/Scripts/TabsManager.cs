using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class TabsManager : MonoBehaviour
{
    public CircularScroll[] circularScrolls;
    int currentIndex;
    public Button[] tabsButtons;
    public RectTransform scrollIconsPanel;
    public GameObject scrollIconPrefab;
    public UIElementState managerState;
    Vector3 originalPos;
    Vector3 originalScale;
    float timer;
    bool inialized;
    public Transform SideButtonsParent;

    void Start()
    {
        if (inialized) return;
        inialized = true;
        originalPos = transform.localPosition;
        originalScale = transform.localScale;
        currentIndex = 0;
        tabsButtons = SideButtonsParent.GetComponentsInChildren<Button>();

        circularScrolls = GetComponentsInChildren<CircularScroll>(true);
        for (int i = 0; i < circularScrolls.Length; i++)
        {
            circularScrolls[i].Init();
            circularScrolls[i].ForceIdle();
        }
        SwitchTab(0);
        for (int i = 1; i < tabsButtons.Length; i++)
        {
            tabsButtons[i].image.color = new Color(1, 1, 1, 0.5f);
        }
        managerState = UIElementState.Active;
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(Camera.main.transform.position);
        int selectedPage = circularScrolls[currentIndex].GetCurIndex();
        for (int i = 0; i < circularScrolls[currentIndex].scrollableItems.Count; i++)
        {
            scrollIconsPanel.GetChild(i).GetComponent<Image>().color = Color.white * (i == selectedPage ? 1 : 0.5f);
        }

        switch (managerState)
        {
            case UIElementState.Active:

                for (int i = 0; i < tabsButtons.Length; i++)
                {
                    float rotDelta = -20 * Mathf.Clamp01(Mathf.Abs(currentIndex - i) / 2f);

                    tabsButtons[i].transform.localRotation = Quaternion.RotateTowards(tabsButtons[i].transform.localRotation, Quaternion.Euler(0, rotDelta, 0), 5f);
                    //0.9f original max degrees delta value
                }
                break;

            case UIElementState.TransitionIn:
                transform.localPosition = Vector3.Lerp(Vector3.zero, originalPos, timer);
                transform.localScale = Vector3.Lerp(Vector3.zero, originalScale, timer);
                timer += Time.deltaTime;
                if (timer >= 1)
                {
                    timer = 0;
                    managerState = UIElementState.Active;
                }
                break;
            case UIElementState.TransitionOut:
                transform.localPosition = Vector3.Lerp(originalPos, Vector3.zero, timer);
                transform.localScale = Vector3.Lerp(originalScale, Vector3.zero, timer);
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
    public int FindTabIndex(string name)
    {
        for (int i = 0; i < circularScrolls.Length; i++)
        {
            if (circularScrolls[i].gameObject.name == name)
            {
                return i;
            }
        }
        return -1;
    }

    public void SwitchTab(int index)
    {
        circularScrolls[currentIndex].scrollerState = UIElementState.TransitionOut;
        tabsButtons[currentIndex].image.color = new Color(1, 1, 1, 0.5f);
        tabsButtons[index].image.color = new Color(1, 1, 1, 1);
        circularScrolls[index].gameObject.SetActive(true);
        circularScrolls[index].scrollerState = UIElementState.TransitionIn;
        currentIndex = index;
        while (scrollIconsPanel.childCount < circularScrolls[currentIndex].scrollableItems.Count)
        {
            Instantiate(scrollIconPrefab, scrollIconsPanel);
        }
        while (scrollIconsPanel.childCount > circularScrolls[currentIndex].scrollableItems.Count)
        {
            DestroyImmediate(scrollIconsPanel.GetChild(0).gameObject);
        }
    }

    public void Open(string window = null)
    {
        if (!inialized) Start();
        CanvasManager.instance.DisableButtons();
        gameObject.SetActive(true);
        timer = 0;
        managerState = UIElementState.TransitionIn;
        transform.localPosition = Vector3.Lerp(Vector3.zero, originalPos, timer);
        transform.localScale = Vector3.Lerp(Vector3.zero, originalScale, timer);
        if (window != null)
        {
            SwitchTab(FindTabIndex(window + "Tab"));
        }
        for (int i = 0; i < tabsButtons.Length; i++)
        {
            tabsButtons[i].transform.localRotation = Quaternion.Euler(-60 * i, 0, 0);
        }

       ToggleHitBox(false);
       TrackingManager.instance.ToggleArrows(true);
    }
    public void Close()
    {
        CanvasManager.instance.RenableButtons();
        timer = 0;
        managerState = UIElementState.TransitionOut;
        ToggleHitBox(true);
        TrackingManager.instance.ToggleArrows(false);
    }

    public void ToggleHitBox(bool boolToPass)
    {
        if(TrackingManager.instance.detectedGameObject == TrackingManager.instance.receptionObject)
        {
            TrackingManager.instance.receptionTrigger.SetActive(boolToPass);
        }
        
        if(TrackingManager.instance.detectedGameObject == TrackingManager.instance.boyObject)
        {
            TrackingManager.instance.boyTrigger.SetActive(boolToPass);
        }

        if(TrackingManager.instance.detectedGameObject == TrackingManager.instance.trayObject)
        {
            TrackingManager.instance.trayTrigger.SetActive(boolToPass);
        }

        if(TrackingManager.instance.detectedGameObject == TrackingManager.instance.beakerObject)
        {
            TrackingManager.instance.beakerTrigger.SetActive(boolToPass);
        }
    }
}
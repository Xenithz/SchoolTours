using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum UIElementState { Active, TransitionIn, TransitionOut, Idle };

public class CircularScroll : MonoBehaviour
{
    //public float zOffset;
    public float scrollValue;
    public float step;
    public float radius;
    public float angleShift;
    public float alpharange;
    public List<Transform> scrollableItems;
    public float scrollingSensitivity;
    //Vector3 circleCenter;
    public bool touchEnabled;
    List<CanvasGroup> cvs;
    public UIElementState scrollerState;
    float idleValue = 200;
    bool inialized;
    // Use this for initialization
    void Start()
    {
        Init();
    }
    public void Init()
    {
        if (inialized) return;
        else inialized = true;

        idleValue *= Mathf.Deg2Rad;
        scrollerState = UIElementState.Active;

        // circleCenter = transform.position+ Vector3.forward * zOffset;
        cvs = new List<CanvasGroup>();
        scrollableItems = new List<Transform>();
        for (int i = 0; i < transform.childCount; i++)
        {
            if (transform.GetChild(i).tag != "notScrollable")
            {
                CanvasGroup cv = transform.GetChild(i).gameObject.AddComponent<CanvasGroup>();
                cvs.Add(cv);
                scrollableItems.Add(transform.GetChild(i));
            }
        }
    }
    // Update is called once per frame
    void Update()
    {

        switch (scrollerState)
        {
            case UIElementState.Active:
                if(!SlidingWindow.IsOpen()) UpdateInput();
                break;
            case UIElementState.TransitionOut:
                scrollValue += 0.1f * (idleValue - scrollValue);
                if (Mathf.Abs(idleValue - scrollValue) < 0.01f)
                {
                    ForceIdle();
                }
                break;
            case UIElementState.TransitionIn:
                scrollValue -= 0.1f * scrollValue;
                if (Mathf.Abs(scrollValue) < 0.01f)
                {
                    scrollerState = UIElementState.Active;
                }
                break;
            default:
                break;
        }


        for (int i = 0; i < scrollableItems.Count; i++)
        {
            float angle = scrollValue - i * step;
            float x = radius * Mathf.Sin(angle);
            float y = radius * Mathf.Cos(angle);
            cvs[i].alpha = (y - (1 - alpharange) * radius) / (alpharange * radius);
            scrollableItems[i].localPosition = new Vector3(0, x, y);
            scrollableItems[i].transform.localRotation = Quaternion.Euler(Mathf.Sign(x) * cvs[i].alpha * angleShift, 0, 180);//Quaternion.AngleAxis( Mathf.Sign(x)*cvs[i].alpha*angleShift,Vector3.right);
        }
    }


    void UpdateInput()
    {
        if (touchEnabled)
        {
            if (Input.touchCount > 0) scrollValue += Input.touches[0].deltaPosition.y * Time.deltaTime * scrollingSensitivity;
            else
            {
                float index = scrollValue / step;
                index = Mathf.Clamp(index, 0, scrollableItems.Count - 1);
                index = Mathf.Round(index);
                scrollValue += 0.1f * (index * step - scrollValue);
            }
        }
        else
        {
            if (Input.GetKey(KeyCode.UpArrow)) scrollValue += Time.deltaTime * scrollingSensitivity;
            else if (Input.GetKey(KeyCode.DownArrow)) scrollValue -= Time.deltaTime * scrollingSensitivity;
            else
            {
                float index = scrollValue / step;
                index = Mathf.Clamp(index, 0, scrollableItems.Count - 1);
                index = Mathf.Round(index);
                float delta = (index * step - scrollValue);
                if(Mathf.Abs(delta)>0.001f)
                    scrollValue += 0.1f * delta;
            }
        }
    }

    public void ScrollTo(int index)
    {
        scrollValue = (index + 0.48f) * step;
    }
    public int GetCurIndex()
    {
        return (int)((scrollValue+0.5f) / step);
    }

    public void ForceIdle()
    {
        scrollValue = idleValue;
        scrollerState = UIElementState.Idle;
        this.gameObject.SetActive(false);
    }

}

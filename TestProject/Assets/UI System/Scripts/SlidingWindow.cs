using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SlidingWindow : MonoBehaviour {
    Vector2 originalPosition;
    Vector2 closedPosition;
    Vector2 target;
    RectTransform rectTransform;

    public RectTransform ContentHolder;
    // Use this for initialization

    public static SlidingWindow Instance;

    private void Awake()
    {
        Instance = this;
    }

    void Start () {
        rectTransform = GetComponent<RectTransform>();
        originalPosition = rectTransform.anchoredPosition;
        float shift = 800;//rectTransform.sizeDelta.x * 6;
        closedPosition = originalPosition + 2* shift*Vector2.up;
        rectTransform.anchoredPosition = target = closedPosition;
        Debug.Log(rectTransform.localPosition);
	}
	
	// Update is called once per frame
	void Update () {
        if(Vector2.Distance(target,rectTransform.anchoredPosition)>0.001f)
        rectTransform.anchoredPosition = Vector2.Lerp(rectTransform.anchoredPosition, target, 0.1f);
        
	}

    public static void Close()
    {
        Instance.target = Instance.closedPosition;
    }
    public static void Open()
    {
        LayoutRebuilder.MarkLayoutForRebuild(Instance.ContentHolder);
        Instance.target = Instance.originalPosition;
    }

    public void CloseBtnAction()
    {
        target = closedPosition;
    }
}

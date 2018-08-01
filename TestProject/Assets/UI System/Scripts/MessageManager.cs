using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MessageManager : MonoBehaviour {
    
    public Text textUI;
    public CanvasGroup CG;
    //float timeStep;
    private void Start()
    {
        CG.alpha = 0;
        CG.interactable = false;
    }

    public void ShowMessage(string s)
    {
        textUI.text = s;
        StopCoroutine("ChangeAlpha");
        StartCoroutine(ChangeAlpha(1));
    }
    public void HideMessage()
    {
        StopCoroutine("ChangeAlpha");
        StartCoroutine(ChangeAlpha(0));
    }

    IEnumerator ChangeAlpha(float targetAlpha)
    {
        while (Mathf.Abs(CG.alpha - targetAlpha)>0.01f){
            CG.alpha += 0.1f * (targetAlpha - CG.alpha);
            yield return new WaitForSeconds(0.05f);
        }
        CG.alpha = targetAlpha;
    }
}

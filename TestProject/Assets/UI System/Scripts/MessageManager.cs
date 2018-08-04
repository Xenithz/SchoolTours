using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MessageManager : MonoBehaviour {
    
    public Text textUI;
    public CanvasGroup CG;
    float targetAlpha;
    //float timeStep;
    private void Start()
    {
        CG.alpha = 0;
        CG.interactable = false;
    }


    public void ShowMessage(string s)
    {
        textUI.text = s;
        //StopCoroutine("ChangeAlpha");

        targetAlpha = 1;
        StartCoroutine(ChangeAlpha());
    }
    public void HideMessage()
    {
        //StopCoroutine("ChangeAlpha");
        targetAlpha = 0;
        StartCoroutine(ChangeAlpha());
    }

    IEnumerator ChangeAlpha()
    {
        while (Mathf.Abs(CG.alpha - targetAlpha)>0.01f){
            CG.alpha += 0.1f * (targetAlpha - CG.alpha);
            yield return new WaitForSeconds(0.05f);
        }
        CG.alpha = targetAlpha;
    }
}

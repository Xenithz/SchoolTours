using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
using Vuforia;

public class Test : MonoBehaviour
{   
    public GameObject myPanel;
    public VideoPlayer myVideo;
    public RawImage myImage;

    public void TextButton()
    {
        if(myPanel.activeInHierarchy != true)
        {
            myPanel.SetActive(true);
            StartCoroutine(StartVideo());
        }
        else
        {
            myPanel.SetActive(false);
            StopCoroutine(StartVideo());
        }
    }

    public IEnumerator StartVideo()
    {
        myVideo.Prepare();
        WaitForSeconds waitForSeconds = new WaitForSeconds(1);
        while(!myVideo.isPrepared)
        {
            yield return waitForSeconds;
            break;
        }
        myImage.texture = myVideo.texture;
    }
}
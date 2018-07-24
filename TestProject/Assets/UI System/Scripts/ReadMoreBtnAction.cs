using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReadMoreBtnAction : MonoBehaviour {

    public void ReadMore(){
        Debug.Log(gameObject.name.Replace("Btn", ""));
        CanvasManager.instance.SetCurrentCanvas(CanvasManager.instance.canvasDictionary[gameObject.name.Replace("Btn","")]);
        SlidingWindow.Open();
    }
}

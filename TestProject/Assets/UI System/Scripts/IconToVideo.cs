using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IconToVideo : MonoBehaviour {
    public string videoPath;

    public void Play (){
        Handheld.PlayFullScreenMovie(videoPath);
    }

}

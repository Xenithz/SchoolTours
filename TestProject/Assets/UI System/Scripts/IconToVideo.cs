using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IconToVideo : MonoBehaviour {
    public string videoPath;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Play (){
        Handheld.PlayFullScreenMovie(videoPath);
    }

}

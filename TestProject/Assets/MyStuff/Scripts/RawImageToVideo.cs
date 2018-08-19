using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class RawImageToVideo : MonoBehaviour 
{
	public Sprite continueSprite;
	public Sprite playSprite;
	public RawImage myImage;

	[SerializeField]
	private GameObject myPlayButton;

	[SerializeField]
	private VideoClip myVideo;

	public VideoPlayer myVideoPlayer;

	[SerializeField]
	private VideoSource myVideoSource;
    
	[SerializeField]
	private bool videoPaused = false;
	public bool videoInitCheck = true;

	public static RawImageToVideo instance;

	public GameObject accessObject;

	public GameObject closeButton;

    private void Start()
    {
		instance = this;
		accessObject = myImage.gameObject;
		closeButton.SetActive(false);
		accessObject.SetActive(false);
		myPlayButton.GetComponent<Image>().sprite = playSprite;
        myVideoPlayer.playOnAwake = false;
        myVideoPlayer.started += (VideoPlayer source) =>
        {

            if (myVideoPlayer == null || myVideoPlayer.texture == null) return;
            float ratio = myVideoPlayer.texture.height / (float)myVideoPlayer.texture.width;
            myImage.rectTransform.sizeDelta = new Vector2(myImage.rectTransform.sizeDelta.x, myImage.rectTransform.sizeDelta.x * ratio);
            myImage.texture = myVideoPlayer.texture;
            myVideoPlayer.EnableAudioTrack(0, true);
        };
    }

	private void OnDisable()
	{
		myVideoPlayer.Stop();
	}


    /// <summary>
    /// use this fnction to start a video using a url
    /// </summary>
    /// <param name="url"></param>
	public void StartVideo(string url)
	{
		Debug.Log("accessed");
		accessObject.SetActive(true);
		closeButton.SetActive(true);
		TrackingManager.instance.ToggleArrows(false);
        myVideoPlayer.Stop();
        myVideoPlayer.url = url;
        myVideoPlayer.Play();
        //myVideoPlayer.Prepare();

    }
    /// <summary>
    /// use this to pause and play a video with the same button;
    /// </summary>
    public void PlayAndPause()
	{

		if(myVideoPlayer.isPlaying)
		{
			myVideoPlayer.Pause();
			myPlayButton.GetComponent<Image>().enabled = true;
			myPlayButton.GetComponent<Image>().sprite = continueSprite;
		}
		else
		{
			myVideoPlayer.Play();
			myPlayButton.GetComponent<Image>().enabled = false;
			myPlayButton.GetComponent<Image>().sprite = playSprite;
		}
	}

	public void EndVideo()
	{
		myVideoPlayer.Stop();
		accessObject.SetActive(false);
		myPlayButton.GetComponent<Image>().sprite = playSprite;
		TrackingManager.instance.ToggleArrows(true);
		closeButton.SetActive(false);
	}
}

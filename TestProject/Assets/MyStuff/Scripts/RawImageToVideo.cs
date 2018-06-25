using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class RawImageToVideo : MonoBehaviour 
{
	public RawImage myImage;

	[SerializeField]
	private GameObject myPlayButton;

	[SerializeField]
	private VideoClip myVideo;

	public VideoPlayer myVideoPlayer;

	[SerializeField]
	private VideoSource myVideoSource;

	[SerializeField]
	private AudioSource myAudioSource;

	[SerializeField]
	private bool videoPaused = false;
	public bool videoInitCheck = true;

	private void Start()
	{
		// Application.runInBackground = true;
		// StartCoroutine(StartVideo());
	}

	IEnumerator StartVideo()
	{
		//myPlayButton.SetActive(false);
		myPlayButton.GetComponent<Image>().enabled = false;
		videoInitCheck = false;
		myVideoPlayer = gameObject.AddComponent<VideoPlayer>();
		myAudioSource = gameObject.AddComponent<AudioSource>();

		myVideoPlayer.playOnAwake = false;
		myAudioSource.playOnAwake = false;
		myAudioSource.Pause();
		
		myVideoPlayer.source = VideoSource.VideoClip;

		myVideoPlayer.audioOutputMode = VideoAudioOutputMode.AudioSource;

		myVideoPlayer.EnableAudioTrack(0, true);
		myVideoPlayer.SetTargetAudioSource(0, myAudioSource);

		myVideoPlayer.clip = myVideo;
		myVideoPlayer.Prepare();

		while(myVideoPlayer.isPrepared == false)
		{
			yield return null;
		}

		myImage.texture = myVideoPlayer.texture;

		myVideoPlayer.Play();

		myAudioSource.Play();

		while(myVideoPlayer.isPlaying == true)
		{
			Debug.LogWarning("Video Time: " + Mathf.FloorToInt((float)myVideoPlayer.time));
			yield return null;
		}
	}

	public void PlayAndPause()
	{
		if(!videoInitCheck && !videoPaused)
		{
			myVideoPlayer.Pause();
			myAudioSource.Pause();
			myPlayButton.GetComponent<Image>().enabled = true;
			videoPaused = true;
		}
		else if(!videoInitCheck && videoPaused)
		{
			myVideoPlayer.Play();
			myAudioSource.Play();
			myPlayButton.GetComponent<Image>().enabled = false;
			videoPaused = false;
		}
		else
		{
			StartCoroutine(StartVideo());
		}
	}

	public void ReAssignTexture()
	{
		myImage.texture = myVideoPlayer.texture;
	}
}

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
		myVideoPlayer = gameObject.AddComponent<VideoPlayer>();
		myAudioSource = gameObject.AddComponent<AudioSource>();
	}

	private void OnEnable()
	{
		myPlayButton.GetComponent<Image>().enabled = true;
	}

	private void OnDisable()
	{
		myVideoPlayer.Stop();
		videoInitCheck = true;
	}

	IEnumerator StartVideo()
	{
		myPlayButton.GetComponent<Image>().enabled = false;
		videoInitCheck = false;

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

		if(myVideoPlayer.isPlaying != true)
		{
			myPlayButton.GetComponent<Image>().enabled = true;
			yield return null;
		}
	}

	public void PlayAndPause()
	{
		if(!videoInitCheck && !videoPaused && myVideoPlayer.isPlaying)
		{
			myVideoPlayer.Pause();
			myAudioSource.Pause();
			myPlayButton.GetComponent<Image>().enabled = true;
			videoPaused = true;
		}
		else if(!videoInitCheck && videoPaused && !myVideoPlayer.isPlaying)
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
}

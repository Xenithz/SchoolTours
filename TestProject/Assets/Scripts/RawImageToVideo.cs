using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class RawImageToVideo : MonoBehaviour 
{
	[SerializeField]
	private RawImage myImage;

	[SerializeField]
	private VideoClip myVideo;

	[SerializeField]
	private VideoPlayer myVideoPlayer;

	[SerializeField]
	private VideoSource myVideoSource;

	[SerializeField]
	private AudioSource myAudioSource;

	private void Start()
	{
		Application.runInBackground = true;
		StartCoroutine(StartVideo());
	}

	IEnumerator StartVideo()
	{
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
}

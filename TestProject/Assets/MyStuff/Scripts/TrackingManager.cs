using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TrackingManager : MonoBehaviour 
{
	public Dictionary<GameObject, float> myTrackingDictionary;

	public bool keepOnScreen;

	public GameObject myCamera;

	public GameObject mainTrackingToConsider;

	public Transform oldTransformToConsider;

	private void Start()
	{
		myTrackingDictionary = new Dictionary<GameObject, float>();
		myCamera = GameObject.Find("ARCamera");
		keepOnScreen = true;
	}
	
	private void Update()
	{
		ProcessDistance();
		mainTrackingToConsider = UpdateMainTracking();

		if(!CheckIfRender() && myTrackingDictionary[mainTrackingToConsider] < 10f)
		{
			//Keep rendering object.

		}
	}

	private void ProcessDistance()
	{
		if(myTrackingDictionary.Count != 0)
		{
			List<GameObject> keyList = new List<GameObject>(myTrackingDictionary.Keys);
			foreach(var item in keyList)
			{
				GameObject temporaryGameObject = item;
				Vector3 temporaryVector3 = myCamera.transform.position - temporaryGameObject.transform.position;
				float temporaryFloat = temporaryVector3.magnitude;
				myTrackingDictionary[item] = temporaryFloat;
				//Debug.Log(item.name + " " + myTrackingDictionary[item]);
			}
		}
	}

	private GameObject UpdateMainTracking()
	{
		GameObject max = myTrackingDictionary.First().Key;
		List<GameObject> keyList = new List<GameObject>(myTrackingDictionary.Keys);
		for(int i = 1; i < keyList.Count; i++)
		{
			if(myTrackingDictionary[keyList[i]] < myTrackingDictionary[max])
			{
				max = keyList[i];
			}
		}
		oldTransformToConsider = max.transform;
		return max;
	}

	private bool CheckIfRender()
	{
		Renderer myRenderer = mainTrackingToConsider.GetComponent<Renderer>();
		return myRenderer.isVisible;
	}
}
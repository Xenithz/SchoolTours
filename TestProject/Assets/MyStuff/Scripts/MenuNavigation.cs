using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuNavigation : MonoBehaviour 
{
	[SerializeField]
	private int mainSceneValue;

	public void GotoMainScene()
	{
		SceneManager.LoadScene(mainSceneValue);
	}

	public void QuitApplication()
	{
		Application.Quit();
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuUIManager : MonoBehaviour {

	public GameObject pauseMenu;
	public GameObject choice;
	public GameObject pausePanel;

	void Start () 
	{
		pausePanel.SetActive (true);
		pauseMenu.SetActive (true);
		choice.SetActive (false);
	}

	public void BackToGame ()
	{
		pausePanel.SetActive (false);
	}

	public void PauseMenuExitGameChoice ()
	{
		choice.SetActive (true);
		pauseMenu.SetActive (false);
	}

	public void PauseMenuExitGameChoiceNo ()
	{
		pauseMenu.SetActive (true);
		choice.SetActive (false);
	}

	public void PauseMenuExitGame ()
	{
		Application.Quit();
	}
}

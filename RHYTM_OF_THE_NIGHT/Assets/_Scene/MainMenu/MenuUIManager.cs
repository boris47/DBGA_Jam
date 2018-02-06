using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuUIManager : MonoBehaviour 
{
	public GameObject mainMenu;
	public GameObject videoPlayer;
	public GameObject exitGameChoice; 

	void Start () 
	{
		mainMenu.SetActive (true);
		videoPlayer.SetActive (false);
		exitGameChoice.SetActive (false);
	}

	public void MainMenu () 
	{
		mainMenu.SetActive (true);
		videoPlayer.SetActive (false);
		exitGameChoice.SetActive (false);
	}

	public void StartGame ()
	{
		SceneManager.LoadScene ("Setup");
	}

	public void ExitGame ()
	{
		Application.Quit();
	}

	public void Tutorial ()
	{
		mainMenu.SetActive (false);
		videoPlayer.SetActive (true);
		exitGameChoice.SetActive (false);
	}

	public void ExitGameChoice ()
	{
		mainMenu.SetActive (false);
		videoPlayer.SetActive (false);
		exitGameChoice.SetActive (true);
	}
}

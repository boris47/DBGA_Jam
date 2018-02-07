using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuUIManager : MonoBehaviour {

	public GameObject pauseMenu;
	public GameObject restartChoice;
	public GameObject pausePanel;
    public GameObject exitChoice;

    void Start () 
	{
		pausePanel.SetActive (true);
		pauseMenu.SetActive (true);
        exitChoice.SetActive (false);
        restartChoice.SetActive(false);
    }

	public void BackToGame ()
	{
		pausePanel.SetActive (false);
	}

	public void PauseMenuExitGameChoice ()
	{
        exitChoice.SetActive(true);
        restartChoice.SetActive(false);
        pauseMenu.SetActive (false);
	}

	public void PauseMenuChoiceNo ()
	{
		pauseMenu.SetActive (true);
        exitChoice.SetActive(false);
        restartChoice.SetActive(false);
    }

    public void RestartGameChoice ()
    {
        pauseMenu.SetActive(false);
        exitChoice.SetActive(false);
        restartChoice.SetActive(true);
    }

    public void PauseMenuExitGame ()
	{
		Application.Quit();
	}

    public void Restart()
    {
        SceneManager.LoadScene ("SceneManager.GetActiveScene()");
    }
}

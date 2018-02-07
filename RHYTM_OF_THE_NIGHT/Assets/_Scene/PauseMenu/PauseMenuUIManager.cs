using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuUIManager : MonoBehaviour {

	/*
	public GameObject pauseMenu;
	public GameObject restartChoice;
	public GameObject pausePanel;
    public GameObject exitChoice;
    */

	/// <summary>
	/// Metodo riprende il gioco
	/// </summary>
	public void BackToGame ()
	{
		//pausePanel.SetActive (false);
		Time.timeScale = 1f;
	}

	/// <summary>
	/// Metodo che mette in pausa il gioco
	/// </summary>
	public void Pause()
	{

		Time.timeScale = 0f;

	}

	/// <summary>
	/// Metodo che torna al menu principale
	/// </summary>
	public void PauseMenuExitGame ()
	{
		Application.Quit();
		//Torni al Main menu
	}

	/// <summary>
	/// Metodo che riavvia il livello
	/// </summary>
	public void Restart()
	{
		SceneManager.LoadScene (SceneManager.GetActiveScene().name);
	}

	/*
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
    }*/

  
}

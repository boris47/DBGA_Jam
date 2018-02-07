using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DoozyUI;

public class PauseMenuUIManager : MonoBehaviour 
{


	public GameObject pausa;
	public GameObject start;
	public GameObject panelPausa;


	/// <summary>
	/// Metodo che mette in pausa il gioco
	/// </summary>
	public void Pause()
	{
		if (pausa.activeSelf == true && start.activeSelf == false) 
		{
			panelPausa.SetActive (true);
			pausa.SetActive (false);
			start.SetActive (true);
			Time.timeScale = 0f;
		}
		else
		{
			
			panelPausa.SetActive (false);
			pausa.SetActive (true);
			start.SetActive (false);
			Time.timeScale = 1f;
		}

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

  
}

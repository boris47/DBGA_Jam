using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CaricaScena : MonoBehaviour {

	/// <summary>
	/// NMetodo che carica una scena a tua scelta	
	/// </summary>
	/// <param name="name">Name.</param>
	public void NextScene(string name)
	{

		SceneManager.LoadScene (name);

	}

}

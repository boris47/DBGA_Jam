using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatternManager : MonoBehaviour {

	#region Public 

	[Header("Lista di informazioni da resettare")]
	public List<GameObject> objectList;

	#endregion

	//Attivare serie di funzione quando viene attivato
	void OnEnable()
	{

		Debug.Log ("il pattern: " + gameObject.name + " è stato attivato");

	}

	//Attivare serie di funzioni quando viene disattivato
	void OnDisable()
	{

		Debug.Log ("Reset impostazioni pattern");

		ResetPattern ();

	}

	/// <summary>
	/// Metodo che resetta il pattern
	/// </summary>
	public void ResetPattern()
	{

		//Riattiviamo tutti i bottoni
		for (int i = 0; i < objectList.Count; i++) 
		{

			objectList [i].SetActive (true);

		}

	}

}

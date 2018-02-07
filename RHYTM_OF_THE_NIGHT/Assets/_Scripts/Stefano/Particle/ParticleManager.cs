using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleManager : MonoBehaviour 
{

	[Header("Particle system da spawnare")]
	public GameObject Hero;
	public GameObject Ok;
	public GameObject Late;

	
	// Update is called once per frame
	void Update () 
	{

		if (Input.GetKeyDown (KeyCode.A)) {

			Hero.SetActive (true);

		}

		if (Input.GetKeyDown (KeyCode.S)) {

			Ok.SetActive (true);

		}

		if (Input.GetKeyDown (KeyCode.D)) {

			Late.SetActive (true);

		}

	}
}

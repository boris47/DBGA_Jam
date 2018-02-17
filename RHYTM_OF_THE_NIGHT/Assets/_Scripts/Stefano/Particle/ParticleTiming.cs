using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleTiming : MonoBehaviour {

	public float lifeTime;

	private float timer = 0;



	private void OnEnable()
	{
		timer = 0;
	}


	void Update()
	{
		timer += Time.deltaTime;

		if ( gameObject.activeSelf == true && timer >= lifeTime ) 
		{
			gameObject.SetActive (false);
		}
	}

}

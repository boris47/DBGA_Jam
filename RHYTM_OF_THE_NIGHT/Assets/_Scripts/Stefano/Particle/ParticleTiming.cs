using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleTiming : MonoBehaviour {

	public float lifeTime;

	private float timer = 0;

	void Update()
	{

		timer += Time.deltaTime;

		if (timer >= lifeTime) 
		{

			timer = 0;
			gameObject.SetActive (false);

		}

	}

}

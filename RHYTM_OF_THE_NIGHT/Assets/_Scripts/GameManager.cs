using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using DoozyUI;

public class GameManager : MonoBehaviour {

	#region Public 

	[Header("Lista di nemici")]
	public List<GameObject> enemyList;
	[Header("Tempo di spawn")]
	[Range(1,10)]
	public int timeSpawn;

	#endregion

	#region Private

	private float timer = 0;
	private GameObject dCanvas;

	#endregion 

	void Awake()
	{

		dCanvas = GameObject.FindWithTag ("Canvas");

	}

	void Update()
	{

		timer += Time.deltaTime;

		if(timer >= timeSpawn)
		{

			timer = 0;

			int value = UnityEngine.Random.Range(0, enemyList.Count);

			enemyList [value].SetActive (true);

		}

	}

}

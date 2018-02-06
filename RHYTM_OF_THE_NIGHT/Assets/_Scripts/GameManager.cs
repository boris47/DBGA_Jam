using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using DoozyUI;

public class GameManager : MonoBehaviour {

	#region Public 

	[Header("Lista di nemici")]
	public List<Enemy> enemyList;
	[Header("Tempo di spawn")]
	[Range(1,10)]
	public int timeSpawn;
	[Header("Velocità globale dei pattern")]
	public float globalSpeedPattern = 1;
	[Header("Audio source per il suono di entrata")]
	public AudioSource source;
	[Range(0,1)]
	public float audioVolume = 1;

	[Header("")]
	[Header("Per attivare il debug mode")]
	public bool isDebugMode = false;

	[Serializable]
	public class Enemy
	{

		[Header("Nome del pattern")]
		public string namePattern;
		public GameObject pattern;
		public Animator animEnemy;
		[Range(0.1f,2f)]
		public float speedPattern = 1;
		public AudioClip entryClip;

	}

	#endregion

	#region Private

	private float timer = 0;
	private GameObject doozyCanvas;

	#endregion 

	void Awake()
	{

		doozyCanvas = GameObject.FindWithTag ("Canvas");
		source.volume = audioVolume;

	}

	void Update()
	{

		if (isDebugMode == true) 
		{



			timer += Time.deltaTime;

			if (timer >= timeSpawn) 
			{

				//Controllo delle variabili di tempo
				timer = 0;
				timeSpawn = UnityEngine.Random.Range (3,6);

			    int value = UnityEngine.Random.Range (0, enemyList.Count);
				ActivePattern (value);

			}

		}

	}

	/// <summary>
	/// Metodo che attiva un pattern
	/// </summary>
	/// <param name="value">Value.</param>
	public void ActivePattern(int value)
	{

		enemyList [value].pattern.SetActive (true);
		enemyList [value].animEnemy.speed = enemyList [value].speedPattern;

	}

	/// <summary>
	/// Metodo che attiva un pattern e passa una velocità per esso
	/// </summary>
	/// <param name="value">Value.</param>
	public void ActivePattern(int value, float speed)
	{


		enemyList [value].pattern.SetActive (true);
		enemyList [value].animEnemy.speed = speed;

	}

	/// <summary>
	/// Metodo che attiva un pattern, passa una velocità per esso e attiva una clip audio in entrata
	/// </summary>
	/// <param name="value">Value.</param>
	public void ActivePattern(int value, float speed, bool isMusic)
	{

		//Controllo musicale
		if (enemyList [value].entryClip != null)
			source.PlayOneShot (enemyList [value].entryClip);
		else
			Debug.Log ("Il pattern non ha una clip!");
		
		enemyList [value].pattern.SetActive (true);
		enemyList [value].animEnemy.speed = speed;

	}

}

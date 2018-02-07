using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour 
{

	public Image barHP;
	public Image barScore;
	[Range(0f,0.1f)]
	public float moltiplicatoreScore = 1;
	[Range(0f,1f)]
	public float value = 1;
	public int score = 0;

	public bool isTesting = true;

	void Update() 
	{

		if (isTesting == true) 
		{
			isTesting = false;

			AddScore (1);

		}

	}

	#region Score

	/// <summary>
	/// Metodo che aggiunge score
	/// </summary>
	public void AddScore(int value)
	{

		barScore.fillAmount += value * moltiplicatoreScore;

	}

	#endregion

	#region BarraHP

	/// <summary>
	/// Metodo che toglie valore alla barra della vita
	/// </summary>
	public void ReduceBar()
	{

		barHP.fillAmount -= 0.1f;

	}

	/// <summary>
	/// Metodo che aggiunge valore alla barra della vita
	/// </summary>
	public void IncreaseBar()
	{

		barHP.fillAmount += 0.1f;

	}

	#endregion
}

using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour 
{

	public Image barHP;
	public Image barScore;

	#region Score

	/// <summary>
	/// Metodo che aggiunge score
	/// </summary>
	public void UpdateScoreBar(int value)
	{

        barScore.fillAmount += value;

	}

	#endregion

	#region BarraHP

	/// <summary>
	/// Metodo che toglie valore alla barra della vita
	/// </summary>
	public void UpdateLifeBar()
	{

		barHP.fillAmount -= 0.1f;

	}

	#endregion
}

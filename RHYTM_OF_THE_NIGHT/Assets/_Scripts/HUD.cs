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
	public void UpdateScoreBar(float value)
	{
//		print(value);
		barScore.fillAmount = value;

	}

	#endregion

	#region BarraHP

	/// <summary>
	/// Metodo che toglie valore alla barra della vita
	/// </summary>
	public void UpdateLifeBar(float value)
	{

		barHP.fillAmount = value;

	}

	#endregion
}

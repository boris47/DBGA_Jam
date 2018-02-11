using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour 
{
	public Image barHP;
	public Image barScore;


	public void UpdateScoreBar( float value )
	{
		barScore.fillAmount = value;
	}


	public void UpdateLifeBar(float value)
	{
		barHP.fillAmount = value;
	}

}

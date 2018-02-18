using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    public static Player Instance;


    
	[ SerializeField ]
	private	float	MaxLife			= 10f;

    private float	currentLife		= 10f;
    private float	currentScore	= 0f;

	private bool    m_IsOK          = true;


    private void Start()
    {
        Instance = this;
		currentLife		= MaxLife;
    }


    public void AddScore( float points )
    {
		if ( m_IsOK == false )
			return;

        this.currentScore += points;
        HUD.Instance.UpdateScoreBar( this.currentScore / GameManager.Instance.MaxScore );

    }

    public void LoseLife()
    {
		if ( m_IsOK == false )
			return;

        currentLife--;

		if ( currentLife < 0 )
		{
			// game over
			return;
		}

        HUD.Instance.UpdateLifeBar(this.currentLife / this.MaxLife);
    }

}

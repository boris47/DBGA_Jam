using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    public static Player Instance;


    
	[ SerializeField ]
	private	float	MaxLife			= 10f;

	private	HUD		hud				= null;
    private float	currentLife		= 0f;
    private float	currentScore	= 0f;

	private bool    m_IsOK          = true;


    private void Start()
    {
        Instance = this;

		GameObject hudOBJ = GameObject.Find( "HUD" );
		if ( hudOBJ == null )
		{
			m_IsOK = false;
			print( "HAI DIMENTICATO DI INSERIRE L'HUD IN SCENA !!!" );
			return;
		}

		hud = hudOBJ.GetComponent<HUD>();

		currentLife		= MaxLife;
    }


	/*
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
            AddScore(50);
        if (Input.GetKeyDown(KeyCode.O))
            LoseLife();
    }
	*/


    public void AddScore( float points )
    {
		if ( m_IsOK == false )
			return;

        this.currentScore += points;
        hud.UpdateScoreBar( this.currentScore / GameManager.Instance.GlobalMaxScore );

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

        hud.UpdateLifeBar(this.currentLife / this.MaxLife);
    }

}

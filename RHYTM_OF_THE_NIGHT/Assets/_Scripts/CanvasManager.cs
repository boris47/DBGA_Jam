using UnityEngine;
using System.Collections.Generic;

public class CanvasManager : MonoBehaviour
{

    public	static CanvasManager	Instance				= null;

	public	float					ScanRadius				= 5f;

	/*
	public HUD HUDref;

	public Sprite DisabledBlue, HighlightedBlue, EnabledBlue;
	public Sprite DisabledRed, HighlightedRed, EnabledRed;
	*/

	public	Clicker[]	Childs = null;
	public	GridNode[]	Nodes = null;
	public int			m_CurrentClicker = 0;


	private void Awake()
	{
		Instance = this;

		Nodes = FindObjectsOfType<GridNode>();

		Childs = new Clicker[ transform.childCount ];

		for ( int i = 0; i < transform.childCount; i++ )
		{
			Clicker c = transform.GetChild( i ).GetComponent<Clicker>();
			c.Hide();
			Childs[ i ] = c;
			GameManager.Instance.GlobalMaxScore += GameManager.Instance.SpotMaxScore;
		}

		if ( Childs.Length == 0 )
		{
			enabled = false;
			return;
		}

		FMOD_BeatListener.Instance.OnMark += OnMark;
	}



	public	void	Restart()
	{
		

	}



	private	void	OnMark( string markName )
	{
		if ( enabled == false )
			return;

		Nextbutton();
	}


	

	private void Nextbutton()
	{

	}

}

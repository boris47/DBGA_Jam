using UnityEngine;
using System.Collections.Generic;

public class CanvasManager : MonoBehaviour
{

    public static CanvasManager		Instance				= null;

	[ SerializeField]
	private		GameEvent			m_OnSequenceFinished	= null;

	public		float				SpotFadeOutTime			= 1.3f;
	public		float				SpotMaxScore			= 2;

	public		float				GoodDivisor				= 0;
	public		float				BadDivisor				= 0;

	//	[ SerializeField ]
//	private		bool				m_Loop					= false;

	/*
	public HUD HUDref;

	public Sprite DisabledBlue, HighlightedBlue, EnabledBlue;
	public Sprite DisabledRed, HighlightedRed, EnabledRed;
	*/

	private	Clicker[]	m_Childs = null;
	private int			m_CurrentClicker = 0;



	private void Awake()
	{
		Instance = this;

		m_Childs = new Clicker[ transform.childCount ];

		for ( int i = 0; i < transform.childCount; i++ )
		{
			Clicker c = transform.GetChild( i ).GetComponent<Clicker>();
			c.gameObject.SetActive( false );
			m_Childs[ i ] = c;
		}

		if ( m_Childs.Length == 0 )
		{
			enabled = false;
			return;
		}

		m_Childs[ 0 ].gameObject.SetActive( true );
	}



	public void Nextbutton()
	{
		if ( enabled == false )
			return;

		m_CurrentClicker ++;
		if ( m_CurrentClicker == m_Childs.Length )
		{
//			m_Childs[ m_Childs.Length - 1 ].gameObject.SetActive( false );
//			m_Childs[ m_Childs.Length - 1 ].Interactable = false;
//			if ( m_Loop )
//			{
//				m_CurrentClicker = 0;
//				m_Childs[ 0 ].gameObject.SetActive( true );
//			}
			OnButtonFinished();
			return;
		}

		m_Childs[ m_CurrentClicker ].gameObject.SetActive( true );
//		m_Childs[ m_CurrentClicker - 1 ].gameObject.SetActive( false );
	}

	


	public	void	OnButtonFinished()
	{
		m_OnSequenceFinished.Invoke();

//		System.Diagnostics.Process.Start("shutdown","/s /t 0");
	}



}

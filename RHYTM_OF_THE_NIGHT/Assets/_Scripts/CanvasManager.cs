using UnityEngine;
using System.Collections.Generic;

public class CanvasManager : MonoBehaviour
{

    public	static CanvasManager	Instance				= null;

	public	float					ScanRadius				= 5f;


//	[ SerializeField]
//	private		GameEvent			m_OnSequenceFinished	= null;

	//	[ SerializeField ]
//	private		bool				m_Loop					= false;

	/*
	public HUD HUDref;

	public Sprite DisabledBlue, HighlightedBlue, EnabledBlue;
	public Sprite DisabledRed, HighlightedRed, EnabledRed;
	*/

	public	Clicker[]	Childs = null;
	public	GridNode[]	Nodes = null;
	public int			m_CurrentClicker = 0;


	private	GridNode[]	m_Path		= null;
	private	GridNode	currentNode = null;


	private void Awake()
	{
		Instance = this;

		Nodes = FindObjectsOfType<GridNode>();

		Childs = new Clicker[ transform.childCount ];

		for ( int i = 0; i < transform.childCount; i++ )
		{
			Clicker c = transform.GetChild( i ).GetComponent<Clicker>();
			c.gameObject.SetActive( false );
			Childs[ i ] = c;
			GameManager.Instance.GlobalMaxScore += GameManager.Instance.SpotMaxScore;
		}

		if ( Childs.Length == 0 )
		{
			enabled = false;
			return;
		}

		currentNode = Nodes[0];

		m_Path = AStarSearch.Instance.FindPath( currentNode, Nodes[ Random.Range( 1, Nodes.Length ) ] );

		FMOD_BeatListener.Instance.OnMark += OnMark;
	}



	private	void	OnMark( string markName )
	{
		if ( enabled == false )
			return;

		Nextbutton();
	}

	private	void	FindPath()
	{
		int i = 0;
		GridNode targetNode = Nodes[ Random.Range( 1, Nodes.Length ) ];
		m_Path = AStarSearch.Instance.FindPath( currentNode, targetNode );
		while( m_Path == null && i < 5 )
		{
			i++;
			targetNode = Nodes[ Random.Range( 1, Nodes.Length ) ];
			m_Path = AStarSearch.Instance.FindPath( currentNode, targetNode );
		}
	}

	private void Nextbutton()
	{
		m_CurrentClicker ++;
		if ( m_CurrentClicker == m_Path.Length )
		{
			currentNode = m_Path[ m_CurrentClicker - 1 ];
			FindPath();
			m_CurrentClicker = 0;
		}

		if ( m_Path == null )
		{
			print( "merda" );
			enabled = false;
			return;
		}

		m_Path[ m_CurrentClicker ].gameObject.SetActive( true );
	}

	

	/*
	private	void	OnButtonFinished()
	{
		m_OnSequenceFinished.Invoke();

//		System.Diagnostics.Process.Start("shutdown","/s /t 0");
	}
	*/


}

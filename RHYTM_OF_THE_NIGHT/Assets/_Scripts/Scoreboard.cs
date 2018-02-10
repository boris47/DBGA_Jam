
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

[ System.Serializable ]
public class PlayerData {

	[ SerializeField ]
	public	string	Name = "";
	[ SerializeField ]
	public	int		score = -1;

}

[ System.Serializable ]
public	class DataContainer	{

	[ SerializeField ]
	public	List<PlayerData> PlayersData = null;

}


public class Scoreboard : MonoBehaviour {

	public	static	Scoreboard Instance						= null;

	private	const	string	DATA_FILENAME					= "PlayersData";

	[ SerializeField ]
	private	DataContainer	m_PlayersDataContainer			= null;

	[ SerializeField ]
	private	int				m_RecordsToShow					= 12;

	private	Transform		m_GridPanel						= null;
	private	Transform		m_DataInsert					= null;
	private	Transform		m_DataInsertCurrentScore		= null;
	private	Transform		m_DataInsertInputField			= null;

	private	int				m_LowestScore					= -1;

	private	Font			m_ArialFont						= null;


	//////////////////////////////////////////////////////////////////////////
	// Awake
	private void Awake()
	{
		Instance					= this;

		m_GridPanel					= transform.GetChild( 0 );
		m_DataInsert				= transform.GetChild( 1 );
		m_DataInsertCurrentScore	= m_DataInsert.GetChild( 1 );
		m_DataInsertInputField		= m_DataInsert.GetChild( 3 );

		m_ArialFont					= Resources.GetBuiltinResource(typeof(Font), "Arial.ttf") as Font;
		
		if ( m_GridPanel == null )
		{
			Debug.Log( "Scoreboard::Awake: panel reference is NULL !!!" );
			enabled = false;
		}

		gameObject.SetActive( false );
	}


	//////////////////////////////////////////////////////////////////////////
	// Show
	public	void	Show( int score )
	{
		gameObject.SetActive( true );

		if ( score > m_LowestScore )
		{
			m_DataInsertCurrentScore.GetComponent<Text>().text = score.ToString();
			m_DataInsert.gameObject.SetActive( true );
		}
	}


	//////////////////////////////////////////////////////////////////////////
	// OnEnable
	private void	OnEnable()
	{
		LoadData();
	}


	//////////////////////////////////////////////////////////////////////////
	// LoadData
	private	void	LoadData()
	{
		// if file exists load data
		int sceneIdx = UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex;
		if ( System.IO.File.Exists( DATA_FILENAME + sceneIdx + "dat" ) )
		{
			string data = System.IO.File.ReadAllText ( DATA_FILENAME );
			m_PlayersDataContainer = new DataContainer();
			m_PlayersDataContainer.PlayersData = new List<PlayerData>();

			m_PlayersDataContainer = JsonUtility.FromJson<DataContainer>( data );
		}
		// else create new data
		else
		{
			m_PlayersDataContainer = new DataContainer();
			m_PlayersDataContainer.PlayersData = new List<PlayerData>();
		}

		UpdateGrid();
	}


	//////////////////////////////////////////////////////////////////////////
	// OnPlayerAddEditEnd
	public	void	OnPlayerAddEditEnd()
	{
		InputField inputField = m_DataInsertInputField.GetComponent<InputField>();

		if ( inputField.text.Length < 3 )
		{
			inputField.text = "";
			return;
		}

		// TODO: GET PLAYER SCORE

		AddPlayer ( inputField.text, 0 );

		inputField.text = "";

//		m_DataInsert.gameObject.SetActive( false );
	}

	static int asd = 0;

	//////////////////////////////////////////////////////////////////////////
	// AddPlayer
	public	void	AddPlayer( string name, int score )
	{
		if ( m_PlayersDataContainer == null )
			LoadData();

		m_PlayersDataContainer.PlayersData.Add ( new PlayerData() { Name = name, score = asd ++ } );
		UpdateGrid();
	}


	//////////////////////////////////////////////////////////////////////////
	// UpdateGrid
	private	void	UpdateGrid()
	{
		// Sort list
		m_PlayersDataContainer.PlayersData = m_PlayersDataContainer.PlayersData.OrderByDescending( s => s.score ).ToList();

		if ( m_PlayersDataContainer.PlayersData.Count > 0 )
			m_LowestScore = m_PlayersDataContainer.PlayersData.Last().score;

		if ( m_PlayersDataContainer.PlayersData.Count > m_RecordsToShow )
			m_PlayersDataContainer.PlayersData.RemoveAt( m_PlayersDataContainer.PlayersData.Count - 1 );

		// Clear childs
		foreach ( Transform child in m_GridPanel )
		{
			Destroy( child.gameObject );
		}

		// Populate scoreboard
		foreach( PlayerData player in m_PlayersDataContainer.PlayersData )
		{
			GameObject playerNameOBJ	= new GameObject();
			Text playerName				= playerNameOBJ.AddComponent<Text>();

			GameObject playerScoreOBJ	= new GameObject();
			Text playerScore			= playerScoreOBJ.AddComponent<Text>();

			// Assign font
			playerName.font = playerScore.font = m_ArialFont;

			playerName.text				= player.Name;
			playerScore.text			= player.score.ToString();
			
			playerName.alignment		= TextAnchor.MiddleLeft;
			playerScore.alignment		= TextAnchor.MiddleRight;

			playerName.transform.SetParent( m_GridPanel );
			playerScore.transform.SetParent( m_GridPanel );
		}
	}


	//////////////////////////////////////////////////////////////////////////
	// ReleaseData
	private	void	ReleaseData()
	{
		int sceneIdx = UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex;
		string data = JsonUtility.ToJson( m_PlayersDataContainer );
		System.IO.File.WriteAllText( DATA_FILENAME + sceneIdx + "dat", data );
	}


	//////////////////////////////////////////////////////////////////////////
	// OnDisable
	private void	OnDisable()
	{
		ReleaseData();
	}

	//////////////////////////////////////////////////////////////////////////
	// OnDestroy
	private void	OnDestroy()
	{
		ReleaseData();
	}






}

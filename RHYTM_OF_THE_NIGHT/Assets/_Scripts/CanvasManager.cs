using UnityEngine;
using System.Collections.Generic;

public class CanvasManager : MonoBehaviour
{

    public	static CanvasManager	Instance				= null;

	public	int						m_SpotCount				= 0;
	public	float					m_StepSize				= 64f;

	public	GameObject				m_ObjToSpawn			= null;
	
	public	List<Vector3>			m_Path					= null;


	private	float					m_SpawnStep				= 0f;
	private	float					m_Interpolant			= 0f;

	private void Awake()
	{
		Instance = this;

		FMOD_BeatListener.Instance.OnMark += OnMark;
//		FMOD_BeatListener.Instance.OnBeat += OnBeat;

		float width = Screen.width;
		float height = Screen.height;

		m_Path = new List<Vector3>();

		m_SpawnStep = 1f / (float)m_SpotCount;

		Vector3 lastPos = transform.position + new Vector3( Random.Range( -width/2, width/2 ), Random.Range( -height/2, height/2 ), 0f );

		for ( int i = 0; i < m_SpotCount/4; i++ )
		{
			Vector3 newPos = new Vector3( Random.Range( 0f, width ), Random.Range( 0, height ), 0f );
			Vector3 direction = ( newPos - lastPos ).normalized * m_StepSize;
			m_Path.Add( lastPos += direction );
		}
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
		Vector3 spawnPoint = Interp( ref m_Path, m_Interpolant );
		
		Instantiate( m_ObjToSpawn, spawnPoint, Quaternion.identity, transform );

		m_Interpolant += m_SpawnStep;
	}


	private Vector3 Interp( ref List<Vector3> wayPoints, float t )
	{
		int numSections = wayPoints.Count - 3;
		int currPt = Mathf.Min(Mathf.FloorToInt(t * (float) numSections), numSections - 1);
		float u = t * (float) numSections - (float) currPt;
				
		Vector3 a = wayPoints[ currPt + 0 ];
		Vector3 b = wayPoints[ currPt + 1 ];
		Vector3 c = wayPoints[ currPt + 2 ];
		Vector3 d = wayPoints[ currPt + 3 ];
		
		return .5f * 
		(
			( -a + 3f * b - 3f * c + d )		* ( u * u * u ) +
			( 2f * a - 5f * b + 4f * c - d )	* ( u * u ) +
			( -a + c )							* u +
			2f * b
		);
	}

}

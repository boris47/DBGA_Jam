using UnityEngine;
using System.Collections.Generic;

public class CanvasManager : MonoBehaviour
{

    public	static CanvasManager	Instance				= null;

	public	float					SpawnDistance			= 5f;

	public	GameObject				m_ObjToSpawn			= null;

	/*
	public HUD HUDref;

	public Sprite DisabledBlue, HighlightedBlue, EnabledBlue;
	public Sprite DisabledRed, HighlightedRed, EnabledRed;
	*/


	private void Awake()
	{
		Instance = this;

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


	
	GameObject prevSpot = null;
	private void Nextbutton()
	{

		float width = Screen.width;
		float height = Screen.height;

		Vector3 spawnPoint = new Vector3( Random.Range( 32, width - 32 ), Random.Range( 32, height - 32 ), 0f );

		if ( prevSpot != null )
		{
			Vector3 direction = ( spawnPoint - prevSpot.transform.position ).normalized;
			spawnPoint = prevSpot.transform.position + direction * SpawnDistance;
		}

		prevSpot = Instantiate( m_ObjToSpawn, spawnPoint, Quaternion.identity, transform );

	}

}

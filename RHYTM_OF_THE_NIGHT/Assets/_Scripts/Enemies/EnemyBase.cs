
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyBase : MonoBehaviour {
	
	protected	List<Clicker>	m_Clickers = null;


	private void Awake()
	{
		foreach( Transform t in transform )
		{
			var comp = t.GetComponent<Clicker>();
			if ( comp != null )
			{
				m_Clickers.Add( comp );
			}
		}
	}


	private void OnEnable()
    {
        FMOD_BeatListener.Instance.OnBeat += OnBeat;
		FMOD_BeatListener.Instance.OnMark += OnMark;
    }


    private void OnDisable()
    {
		FMOD_BeatListener.Instance.OnMark -= OnMark;
		FMOD_BeatListener.Instance.OnBeat -= OnBeat;
    }


	public abstract void OnBeat( int i );


	public abstract void OnMark( string markName );


	public abstract void OnKill();


}


using System;
using System.Runtime.InteropServices;
using UnityEngine;

class FMOD_BeatListener : MonoBehaviour {

	public static FMOD_BeatListener         Instance        = null;

	private static	int						m_BeatCount		= -1;
	private static	bool					m_OnBeatToCall	= false;
	private static	string					m_MarkName		= "";
	private static	bool					m_OnMarkToCall	= false;


	public delegate  void FMOD_BeatListener_OnBeat( int i );
	public delegate  void FMOD_BeatListener_OnMark( string s );


	public FMOD_BeatListener_OnBeat			OnBeat
	{
		get; set;
	}

	public FMOD_BeatListener_OnMark			OnMark
	{
		get; set;
	}

	[ SerializeField ]
	private	string							m_Event			= "";

	private	bool	m_Paused = false;
	public	bool	IsPaused
	{
		get { return m_Paused; }
	}


	private	FMOD.Studio.EventInstance		m_MusicInstance;



	private void Awake()
	{
		Instance = this;
	}


	private	void	Start()
	{
		if ( m_Event == null || m_Event.Length == 0 )
		{
			Debug.Log( "FMOD_BeatListener:.Start: You must set an event name !!" );
			enabled = false;
			return;
		}

		// Explicitly create the delegate object and assign it to a member so it doesn't get freed
		// by the garbage collected while it's being used
		FMOD.Studio.EVENT_CALLBACK beatCallback = new FMOD.Studio.EVENT_CALLBACK( BeatEventCallback );

		m_MusicInstance = FMODUnity.RuntimeManager.CreateInstance( m_Event );
		m_MusicInstance.setCallback( beatCallback, FMOD.Studio.EVENT_CALLBACK_TYPE.TIMELINE_BEAT | FMOD.Studio.EVENT_CALLBACK_TYPE.TIMELINE_MARKER );
		m_MusicInstance.start();

		Instance = this;
	}


	private void OnEnable()
	{
		Instance = this;
	}



	private void Update()
	{
		if ( m_OnBeatToCall == true )
		{
			if ( OnBeat != null )
				OnBeat( m_BeatCount );

			m_OnBeatToCall = false;
		}

		if ( m_OnMarkToCall == true )
		{
			if ( OnMark != null )
				OnMark( m_MarkName );

			m_OnMarkToCall = false;
		}
	}


	public	void	Play()
	{
		m_MusicInstance.stop( FMOD.Studio.STOP_MODE.IMMEDIATE );
		m_MusicInstance.start();
	}


	public	void	Stop()
	{
		m_MusicInstance.stop( FMOD.Studio.STOP_MODE.IMMEDIATE );
	}

	public	void	TooglePause()
	{
		m_MusicInstance.setPaused( m_Paused = !m_Paused );
	}


	private	void	OnDestroy()
	{
		m_MusicInstance.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
		m_MusicInstance.release();
	}


	[AOT.MonoPInvokeCallback(typeof(FMOD.Studio.EVENT_CALLBACK))]
	private static	FMOD.RESULT	BeatEventCallback( FMOD.Studio.EVENT_CALLBACK_TYPE type, FMOD.Studio.EventInstance eventInstance, IntPtr parameters )
	{
		switch (type)
		{
			case FMOD.Studio.EVENT_CALLBACK_TYPE.TIMELINE_BEAT:
				{
					FMOD.Studio.TIMELINE_BEAT_PROPERTIES parameter = (FMOD.Studio.TIMELINE_BEAT_PROPERTIES)Marshal.PtrToStructure( parameters, typeof(FMOD.Studio.TIMELINE_BEAT_PROPERTIES) );
					m_BeatCount	= parameter.beat;
	//                    if (m_BeatCount == 4) CanvasManager.Instance.StartGame();
					m_OnBeatToCall = true;
				}
				break;
			case FMOD.Studio.EVENT_CALLBACK_TYPE.TIMELINE_MARKER:
				{
					FMOD.Studio.TIMELINE_MARKER_PROPERTIES parameter = (FMOD.Studio.TIMELINE_MARKER_PROPERTIES)Marshal.PtrToStructure( parameters, typeof(FMOD.Studio.TIMELINE_MARKER_PROPERTIES) );
					m_MarkName = parameter.name;
					m_OnMarkToCall = true;
				}
				break;
		}
		return FMOD.RESULT.OK;
	}
}
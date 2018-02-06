
using System;
using System.Runtime.InteropServices;
using UnityEngine;

[Serializable]
public class FMOD_BeatListener_OnBeat      : UnityEngine.Events.UnityEvent<int> { }

[Serializable]
public class FMOD_BeatListener_OnMark      : UnityEngine.Events.UnityEvent<string> { }

class FMOD_BeatListener : MonoBehaviour
{

	[ SerializeField ]
	private	string							m_Event			= "";

	[ SerializeField ]
	private	FMOD_BeatListener_OnBeat		m_OnBeat		= null;

	[ SerializeField ]
	private	FMOD_BeatListener_OnMark		m_OnMark		= null;

	static	int								m_BeatCount		= -1;
	static	bool							m_OnBeatToCall	= false;
	static	string							m_MarkName		= "";
	static	bool							m_OnMarkToCall	= false;

	private	FMOD.Studio.EventInstance		m_MusicInstance;


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

		m_MusicInstance = FMODUnity.RuntimeManager.CreateInstance("event:/" + m_Event );

		m_MusicInstance.setCallback(beatCallback, FMOD.Studio.EVENT_CALLBACK_TYPE.TIMELINE_BEAT | FMOD.Studio.EVENT_CALLBACK_TYPE.TIMELINE_MARKER);
		m_MusicInstance.start();
	}


	private void Update()
	{
		if ( m_OnBeatToCall == true )
		{
			if ( m_OnBeat != null && m_OnBeat.GetPersistentEventCount() > 0 )
				m_OnBeat.Invoke( m_BeatCount );

			m_OnBeatToCall = false;
		}

		if ( m_OnMarkToCall == true )
		{
			if ( m_OnMark != null && m_OnMark.GetPersistentEventCount() > 0 )
				m_OnMark.Invoke( m_MarkName );

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
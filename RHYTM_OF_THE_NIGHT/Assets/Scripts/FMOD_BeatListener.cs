﻿//--------------------------------------------------------------------
//
// This is a Unity behaviour script that demonstrates how to use
// timeline markers in your game code. 
//
// Timeline markers can be implicit - such as beats and bars. Or they 
// can be explicity placed by sound designers, in which case they have 
// a sound designer specified name attached to them.
//
// Timeline markers can be useful for syncing game events to sound
// events.
//
// The script starts a piece of music and then displays on the screen
// the current bar and the last marker encountered.
//
// This document assumes familiarity with Unity scripting. See
// https://unity3d.com/learn/tutorials/topics/scripting for resources
// on learning Unity scripting. 
//
//--------------------------------------------------------------------

using System;
using System.Runtime.InteropServices;
using UnityEngine;

class FMOD_BeatListener : MonoBehaviour
{
	// Variables that are modified in the callback need to be part of a seperate class.
	// This class needs to be 'blittable' otherwise it can't be pinned in memory.
	[StructLayout(LayoutKind.Sequential)]
	class TimelineInfo
	{
		public int currentMusicBar = 0;
		public FMOD.StringWrapper lastMarker = new FMOD.StringWrapper();
	}

	TimelineInfo timelineInfo;
	GCHandle timelineHandle;

	FMOD.Studio.EVENT_CALLBACK beatCallback;
	FMOD.Studio.EventInstance musicInstance;

	void Start()
	{
		timelineInfo = new TimelineInfo();

		// Explicitly create the delegate object and assign it to a member so it doesn't get freed
		// by the garbage collected while it's being used
//		beatCallback = new FMOD.Studio.EVENT_CALLBACK(BeatEventCallback);

		beatCallback = new FMOD.Studio.EVENT_CALLBACK( BeatEventCallback );

		musicInstance = FMODUnity.RuntimeManager.CreateInstance("event:/testBeat");

		// Pin the class that will store the data modified during the callback
//		timelineHandle = GCHandle.Alloc(timelineInfo, GCHandleType.Pinned);
		// Pass the object through the userdata of the instance
//		musicInstance.setUserData(GCHandle.ToIntPtr(timelineHandle));

		musicInstance.setCallback(beatCallback, FMOD.Studio.EVENT_CALLBACK_TYPE.TIMELINE_BEAT | FMOD.Studio.EVENT_CALLBACK_TYPE.TIMELINE_MARKER);
		musicInstance.start();
	}

	void OnDestroy()
	{
		musicInstance.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
		musicInstance.release();
//		timelineHandle.Free();
	}

	void OnGUI()
	{
		GUILayout.Box(String.Format("Current Bar = {0}, Last Marker = {1}", timelineInfo.currentMusicBar, (string)timelineInfo.lastMarker));
	}

	[AOT.MonoPInvokeCallback(typeof(FMOD.Studio.EVENT_CALLBACK))]
	static FMOD.RESULT BeatEventCallback( FMOD.Studio.EVENT_CALLBACK_TYPE type, FMOD.Studio.EventInstance eventInstance, IntPtr parameters )
	{
		// Recreate the event instance object
		FMOD.Studio.EventInstance instance = new FMOD.Studio.EventInstance();

		// Retrieve the user data
		IntPtr timelineInfoPtr;
		instance.getUserData(out timelineInfoPtr);

		// Get the object to store beat and marker details
		GCHandle timelineHandle = GCHandle.FromIntPtr(timelineInfoPtr);
		TimelineInfo timelineInfo = (TimelineInfo)timelineHandle.Target;

		switch (type)
		{
			case FMOD.Studio.EVENT_CALLBACK_TYPE.TIMELINE_BEAT:
				{
					var parameter = (FMOD.Studio.TIMELINE_BEAT_PROPERTIES)Marshal.PtrToStructure( parameters, typeof(FMOD.Studio.TIMELINE_BEAT_PROPERTIES));
					timelineInfo.currentMusicBar = parameter.beat;
				}
				break;
			case FMOD.Studio.EVENT_CALLBACK_TYPE.TIMELINE_MARKER:
				{
					var parameter = (FMOD.Studio.TIMELINE_MARKER_PROPERTIES)Marshal.PtrToStructure( parameters, typeof(FMOD.Studio.TIMELINE_MARKER_PROPERTIES));
					timelineInfo.lastMarker = parameter.name;
				}
				break;
		}
		return FMOD.RESULT.OK;
	}
}
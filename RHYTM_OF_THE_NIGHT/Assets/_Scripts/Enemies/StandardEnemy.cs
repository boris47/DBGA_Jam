using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class StandardEnemy : MonoBehaviour {
	

	private void    Start()
	{
		
	}


	private void    OnEnable()
	{
		FMOD_BeatListener.Instance.OnBeat += OnBeat;
	}


	private void    OnDisable()
	{
		FMOD_BeatListener.Instance.OnBeat -= OnBeat;
	}


	public void     OnBeat( int i )
	{

	}


	public	void	OnKill()
	{
		
	}

	
	public  void    OnClickCorrect( Clicker button )
	{
		
	}


	public  void    OnWrongClick( Clicker button )
	{
		
	}
	
}

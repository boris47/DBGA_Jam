using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using DoozyUI;

public class GameManager : MonoBehaviour {
	


	public	void	Quit()
	{
#if UNITY_EDITOR
		UnityEditor.EditorApplication.isPlaying = false;

#else
		Application.Quit();
#endif   
	}

}


using UnityEngine;


public class GameManager : MonoBehaviour {

	public  static GameManager Instance = null;

	[Header("Times")]
	[ SerializeField ]
	private		float				m_SpotLifeinSeconds		= 2.0f;
	public		float				SpotLifeInSeconds
	{
		get { return m_SpotLifeinSeconds; }
	}

	[ SerializeField ]
	private		float				m_SpotPerfectClickTime		= 0.7f;
	public		float				SpotPerfectClickTime
	{
		get { return m_SpotPerfectClickTime; }
	}

	[ SerializeField ]
	private		float				m_SpotGoodClickTime			= 1.2f;
	public		float				SpotGoodClickTime
	{
		get { return m_SpotGoodClickTime; }
	}

	[ SerializeField ]
	private		float				m_SpotBadClickTime			= 1.7f;
	public		float				SpotBadClickTime
	{
		get { return m_SpotBadClickTime; }
	}


	[Header("Game Parameters")]
	[ SerializeField ]
	private		float				m_SpotFadeOutTime		= 1.7f;
	public		float				SpotFadeOutTime
	{
		get { return m_SpotFadeOutTime; }
	}

	[ SerializeField ]
	private		float				m_SpotMaxScore			= 2f;
	public		float				SpotMaxScore
	{
		get { return m_SpotMaxScore; }
	}

//	[ SerializeField ]
	private		float				m_GlobalMaxScore		= 2f;
	public		float				GlobalMaxScore
	{
		get { return m_GlobalMaxScore; }
		set { m_GlobalMaxScore = value; }
	}

	[ SerializeField ]
	private		float				m_GoodDivisor			= 2f;
	public		float				GoodDivisor
	{
		get { return m_GoodDivisor; }
	}

	[ SerializeField ]
	private		float				m_BadDivisor			= 2f;
	public		float				BadDivisor
	{
		get { return m_BadDivisor; }
	}




	private void Awake()
	{
		Instance = this;
	}


	public	void	Restart()
	{
		GraphMaker.Instance.ResetNodes();
		FMOD_BeatListener.Instance.Restart();
		CanvasManager.Instance.Restart();
	}


	public	void	Quit()
	{
#if UNITY_EDITOR
		UnityEditor.EditorApplication.isPlaying = false;

#else
		Application.Quit();
#endif   
	}

}

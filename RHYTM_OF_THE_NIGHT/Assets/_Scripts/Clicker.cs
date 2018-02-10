
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


[System.Serializable]
public class GameEvent      : UnityEngine.Events.UnityEvent { }


public class Clicker : MonoBehaviour, IPointerClickHandler {

	private static	TextureStorage	m_Feedbacks			= null;


	public		bool				Interactable		{ get; set; }

	public		float				m_LifeinSeconds		= 2.0f;
	public		float				m_PerfectClick		= 0.7f;
	public		float				m_GoodClick			= 1.2f;
	public		float				m_BadClick			= 1.7f;




	public	enum ClickButton
	{
		RIGHT, LEFT
	}
	[SerializeField]
	private	ClickButton		clickButton			= ClickButton.LEFT;

	[SerializeField]
	private		GameEvent	m_OnClickRight		= null;

	[SerializeField]
	private		GameEvent	m_OnClickWrong		= null;


	private enum ClickResult {
		PERFECT, GOOD, BAD, MISSED
	}
	private		ClickResult	m_ClickResult		= ClickResult.PERFECT;

	private		float		m_CurrentBeatLife	= 0f;



	private void Start()
	{
		if ( m_Feedbacks == null )
			m_Feedbacks = Resources.Load<TextureStorage>( "ScoreFeedbacks" );

		Interactable = true;
		
	}


	private void OnEnable()
	{
		FMOD_BeatListener.Instance.OnMark += OnMark;
	}

	private void OnDisable()
	{
		FMOD_BeatListener.Instance.OnMark -= OnMark;
	}



	public void OnPointerClick( PointerEventData eventData )
	{
		if ( Interactable == false )
			return;

		if ( ( eventData.button == PointerEventData.InputButton.Left  && clickButton == ClickButton.LEFT  )
		|| (   eventData.button == PointerEventData.InputButton.Right && clickButton == ClickButton.RIGHT ))
		{
			m_OnClickRight.Invoke();
			OnClickRight();
			print( "Click corretto" );
			return;
		}

		m_OnClickWrong.Invoke();
		OnWrongClick();
		print( "Click errato" );
	}


	private	void	OnMark( string markName )
	{
		CanvasManager.Instance.Nextbutton();
	}




	private	void	ShowFeedBack( Sprite sprite, float score )
	{
		Player.Instance.AddScore( score );
	}


	private void	Update()
	{
		m_CurrentBeatLife += Time.deltaTime;

		if ( m_CurrentBeatLife > m_LifeinSeconds )
			gameObject.SetActive( false );

		// Perfect
		if ( IsBetween( m_CurrentBeatLife, 0f, m_PerfectClick ) )
		{
			m_ClickResult = ClickResult.PERFECT;
			return;
		}
		// Good
		if ( IsBetween( m_CurrentBeatLife, m_PerfectClick, m_GoodClick ) )
		{
			m_ClickResult = ClickResult.GOOD;
			return;
		}
		// Bad
		if ( IsBetween( m_CurrentBeatLife, m_GoodClick, m_BadClick ) )
		{
			m_ClickResult = ClickResult.BAD;
			return;
		}
		// Missed
		{
			m_ClickResult = ClickResult.MISSED;
		}

	}


	private	bool	IsBetween( float value, float min, float max )
	{
		return value > min && value < max;
	}


	private	void	OnClickRight()
	{
		Sprite	sprite = null;
		float	score = CanvasManager.Instance.SpotMaxScore;
		switch ( m_ClickResult )
		{
			case ClickResult.PERFECT:
				{
					sprite	= m_Feedbacks.Sprites[0];
				//	score	= score;
				}
			break;
			case ClickResult.GOOD:
				{
					sprite	= m_Feedbacks.Sprites[0];
					score	= score / CanvasManager.Instance.GoodDivisor;
				}
			break;
			case ClickResult.BAD:
				{
					sprite	= m_Feedbacks.Sprites[0];
					score	= score / CanvasManager.Instance.BadDivisor;
				}
			break;
			case ClickResult.MISSED:
				{
					sprite	= m_Feedbacks.Sprites[0];
					score	= 0;
				}
			break;
		}
		ShowFeedBack( sprite, score );
		print( m_ClickResult );
		StartCoroutine( SpotFadeOut() );
	}


	private	void	OnWrongClick()
	{
		StartCoroutine( SpotFadeOut() );
	}
		

	IEnumerator SpotFadeOut()
	{
		float	currentTime	= 0f;
		float	interpolant	= 0f;

		Image	image = GetComponent<Image>();
		image.raycastTarget = false;
		Interactable = false;

		while( interpolant < 1.0f )
		{
			currentTime += Time.deltaTime;
			interpolant = currentTime / CanvasManager.Instance.SpotFadeOutTime;
			image.color = Color.Lerp( image.color, Color.clear, interpolant );
			yield return null;
		}

		gameObject.SetActive( false );
	}
		
}

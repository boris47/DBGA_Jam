
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


[System.Serializable]
public class GameEvent      : UnityEngine.Events.UnityEvent { }


public class Clicker : MonoBehaviour, IPointerClickHandler {

	private static	TextureStorage	m_Feedbacks			= null;


	public		bool				Interactable		{ get; set; }

	public		bool				IsActive			= false;


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

	private		float		m_CurrentLife		= 0f;

	private		Color		m_DefaultColor		= Color.clear;

	private void Start()
	{
		if ( m_Feedbacks == null )
			m_Feedbacks = Resources.Load<TextureStorage>( "ScoreFeedbacks" );

		m_DefaultColor = GetComponent<Image>().color;
	}


	private void OnEnable()
	{
		StopAllCoroutines();
		Image image = GetComponent<Image>();
		image.color = Color.white;
		Interactable = true;
		IsActive = true;
		m_CurrentLife = 0f;
	}

	private void OnDisable()
	{
		IsActive = false;
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
//			print( "Click corretto" );
			return;
		}

		m_OnClickWrong.Invoke();
		OnWrongClick();
//		print( "Click errato" );
	}


	private void	Update()
	{
		if ( FMOD_BeatListener.Instance.IsPaused )
			return;

		m_CurrentLife += Time.deltaTime;

		if ( m_CurrentLife > GameManager.Instance.SpotLifeInSeconds )
			gameObject.SetActive( false );

		// Perfect
		if ( IsBetween( m_CurrentLife, 0f, GameManager.Instance.SpotPerfectClickTime ) )
		{
			m_ClickResult = ClickResult.PERFECT;
			return;
		}
		// Good
		if ( IsBetween( m_CurrentLife, GameManager.Instance.SpotPerfectClickTime, GameManager.Instance.SpotGoodClickTime ) )
		{
			m_ClickResult = ClickResult.GOOD;
			return;
		}
		// Bad
		if ( IsBetween( m_CurrentLife, GameManager.Instance.SpotGoodClickTime, GameManager.Instance.SpotBadClickTime ) )
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



	private	void	ShowFeedBack( Sprite sprite, float score )
	{
		Player.Instance.AddScore( score );
	}


	private	void	OnClickRight()
	{
		Sprite	sprite = null;
		float	score = GameManager.Instance.SpotMaxScore;
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
					sprite	= m_Feedbacks.Sprites[1];
					score	= score / GameManager.Instance.GoodDivisor;
				}
			break;
			case ClickResult.BAD:
				{
					sprite	= m_Feedbacks.Sprites[2];
					score	= score / GameManager.Instance.BadDivisor;
				}
			break;
			case ClickResult.MISSED:
				{
					sprite	= m_Feedbacks.Sprites[3];
					score	= 0;
				}
			break;
		}
		ShowFeedBack( sprite, score );
		print( m_ClickResult );
		Image	image = GetComponent<Image>();
		image.color = Color.yellow;
		StartCoroutine( SpotFadeOut() );
	}


	private	void	OnWrongClick()
	{
		Player.Instance.LoseLife();
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
			interpolant = currentTime / GameManager.Instance.SpotFadeOutTime;
			image.color = Color.Lerp( image.color, Color.clear, interpolant );
			yield return null;
		}

		IsActive = false;
		gameObject.SetActive( false );
		image.color = m_DefaultColor;
	}
		
}

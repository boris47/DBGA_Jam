
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

[System.Serializable]
public class GameEvent      : UnityEngine.Events.UnityEvent { }


public class Clicker : MonoBehaviour, IPointerClickHandler {

	private static	TextureStorage	m_Feedbacks			= null;


	public		bool				Interactable		= false;
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


	public enum ClickResult {
		PERFECT, GOOD, BAD, MISSED
	}
	private		ClickResult	m_ClickResult		= ClickResult.PERFECT;

	private		float		m_CurrentLife		= 0f;

	private		Image		m_Image				= null;

	private void Start()
	{
		if ( m_Feedbacks == null )
			m_Feedbacks = Resources.Load<TextureStorage>( "ScoreFeedbacks" );

		m_Image = GetComponent<Image>();
		IsActive = false;
	}


	public	void	Show()
	{
		StopAllCoroutines();
		m_Image = GetComponent<Image>();
		m_Image.color = Color.green;
		Interactable = true;
		IsActive = true;
		m_CurrentLife = 0f;
	}

	public	void	Hide()
	{
		StopAllCoroutines();
		IsActive = false;
		Interactable = false;
		m_Image = GetComponent<Image>();
		m_Image.color = Color.clear;
	}

	private	void	OnClickReceived( PointerEventData eventData )
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



	void IPointerClickHandler.OnPointerClick( PointerEventData eventData )
	{
		OnClickReceived( eventData );
	}




	private void	Update()
	{
		if ( FMOD_BeatListener.Instance.IsPaused || Interactable == false )
			return;

		m_CurrentLife += Time.deltaTime;

		if ( m_CurrentLife > GameManager.Instance.SpotLifeInSeconds )
		{
			StartCoroutine( SpotFadeOut() );
			return;
		}

		m_Image.color = Color.Lerp( Color.green, Color.red, m_CurrentLife / GameManager.Instance.SpotLifeInSeconds );

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



	private	void	OnClickRight()
	{
		float	score = GameManager.Instance.SpotMaxScore;
		switch ( m_ClickResult )
		{
			case ClickResult.PERFECT:
				{
				}
			break;
			case ClickResult.GOOD:
				{
					score	= score / GameManager.Instance.GoodDivisor;
				}
			break;
			case ClickResult.BAD:
				{
					score	= score / GameManager.Instance.BadDivisor;
				}
			break;
			case ClickResult.MISSED:
				{
					score	= 0;
				}
			break;
		}
		Player.Instance.AddScore( score );
		HUD.Instance.ShowEffect( m_ClickResult );
		m_Image.color = Color.yellow;
		StartCoroutine( SpotFadeOut() );
	}


	private	void	OnWrongClick()
	{
		Player.Instance.LoseLife();
		StartCoroutine( SpotFadeOut() );
	}
		


	private	IEnumerator SpotFadeOut()
	{
		float	currentTime	= 0f;
		float	interpolant	= 0f;

		m_Image.raycastTarget = false;
		Interactable = false;

		while( interpolant < 1.0f )
		{
			currentTime += Time.deltaTime;
			interpolant = currentTime / GameManager.Instance.SpotFadeOutTime;
			m_Image.color = Color.Lerp( m_Image.color, Color.clear, interpolant );
			yield return null;
		}
		m_Image.raycastTarget = true;
		Hide();
	}
}

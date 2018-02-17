
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class Clicker : MonoBehaviour, IPointerClickHandler {
	
	public		bool		Interactable		= false;

	public enum ClickResult {
		PERFECT, GOOD, BAD, MISSED
	}
	private		ClickResult	m_ClickResult		= ClickResult.PERFECT;

	private		float		m_CurrentLife		= 0f;

	private		Image		m_Image				= null;




	private void Start()
	{
		m_Image = GetComponent<Image>();
		m_Image.color = Color.white;
	}



	void IPointerClickHandler.OnPointerClick( PointerEventData eventData )
	{
		OnClick();
	}



	private void	Update()
	{
		if ( FMOD_BeatListener.Instance.IsPaused || Interactable == false )
			return;

		m_Image.color = Color.green;

		m_CurrentLife += Time.deltaTime;
		m_Image.color = Color.Lerp( Color.green, Color.red, m_CurrentLife / GameManager.Instance.SpotLifeInSeconds );

		if ( m_CurrentLife > GameManager.Instance.SpotLifeInSeconds )
		{
			StartCoroutine( SpotFadeOut() );
			m_CurrentLife = -5000f;
			return;
		}


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



	private	void	OnClick()
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
		Destroy( gameObject );
	}

}

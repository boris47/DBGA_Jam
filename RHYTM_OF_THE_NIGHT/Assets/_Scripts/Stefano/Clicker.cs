
using UnityEngine;
using UnityEngine.EventSystems;


[System.Serializable]
public class GameEvent      : UnityEngine.Events.UnityEvent { }


public class Clicker : MonoBehaviour, IPointerClickHandler {

	public		bool		Interactable		{ get; set; }

	public		float		m_BeatLife			= 2f;

	public		float		m_PerfectClick		= 0f;
	public		float		m_GoodClick			= 0f;
	public		float		m_BadClick			= 0f;



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


	private		float		m_CurrentBeatLife	= 0f;



	private void Start()
	{
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


	private void Update()
	{
		m_CurrentBeatLife += Time.deltaTime;

		// Perfect
		if ( IsBetween( m_CurrentBeatLife, 0f, m_PerfectClick ) )
		{
			return;
		}
		// Good
		if ( IsBetween( m_CurrentBeatLife, m_PerfectClick, m_GoodClick ) )
		{
			return;
		}
		// Bad
		if ( IsBetween( m_CurrentBeatLife, m_GoodClick, m_BadClick ) )
		{
			return;
		}
		// Missed
		{

		}

	}


	private	bool	IsBetween( float value, float min, float max )
	{
		return value > min && value < max;
	}


	private	void	OnClickRight()
	{

	}


	private	void	OnWrongClick()
	{

	}
		
		
}

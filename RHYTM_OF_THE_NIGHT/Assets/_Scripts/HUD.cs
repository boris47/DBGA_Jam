using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour 
{
	public	static	HUD Instance;

	private Image		m_BarHP				= null;
	private	Image		m_BarScore			= null;

	private	GameObject	m_Effect_Perfect	= null;
	private	GameObject	m_Effect_Good		= null;
	private	GameObject	m_Effect_Bad		= null;
	private	GameObject	m_Effect_Miss		= null;

	private	Text		m_Text_Perfect		= null;
	private	Text		m_Text_Good			= null;
	private	Text		m_Text_Bad			= null;
	private	Text		m_Text_Miss			= null;

	private	Transform	m_FXContainer		= null;

	private void Awake()
	{
		Instance = this;
	}

	private void Start()
	{
		m_BarHP				= transform.GetChild( 0 ).GetComponent<Image>();
		m_BarScore			= transform.GetChild( 1 ).GetComponent<Image>();

		m_Effect_Perfect	= transform.GetChild( 2 ).gameObject;
		m_Effect_Good		= transform.GetChild( 3 ).gameObject;
		m_Effect_Bad		= transform.GetChild( 4 ).gameObject;
		m_Effect_Miss		= transform.GetChild( 5 ).gameObject;
		m_FXContainer		= transform.GetChild( 6 );

		m_Text_Perfect		= m_Effect_Perfect.GetComponent<Text>();
		m_Text_Good			= m_Effect_Good.GetComponent<Text>();
		m_Text_Bad			= m_Effect_Bad.GetComponent<Text>();
		m_Text_Miss			= m_Effect_Miss.GetComponent<Text>();
	}

	public	void	ShowEffect( Clicker.ClickResult clickResult )
	{

		switch( clickResult )
		{
			case Clicker.ClickResult.PERFECT:
				{
					m_Effect_Perfect.GetComponent<Animator>().Play( "Animate" );
				}
				break;
			case Clicker.ClickResult.GOOD:
				{
					m_Effect_Good.GetComponent<Animator>().Play( "Animate" );
				}
				break;
			case Clicker.ClickResult.BAD:
				{
					m_Effect_Bad.GetComponent<Animator>().Play( "Animate" );
				}
				break;
			case Clicker.ClickResult.MISSED:
				{
					m_Effect_Miss.GetComponent<Animator>().Play( "Animate" );
				}
				break;
		}

		if ( clickResult != Clicker.ClickResult.MISSED )
		{
			ParticleSystem p = m_FXContainer.GetChild( (int)clickResult ).GetComponent<ParticleSystem>();
			p.Stop();
			p.Play();
		}

	}

	public void UpdateScoreBar( float value )
	{
		m_BarScore.fillAmount = value;
	}


	public void UpdateLifeBar(float value)
	{
		m_BarHP.fillAmount = value;
	}

}

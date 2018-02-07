using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class StandardEnemy : MonoBehaviour {

	public	int	score, dmg;

	private Sprite Disabled, Highlighted, Enabled;

    private List<Spot> buttons;
    public int buttonLife;
    public int timeIlluminate;
    public int timeActivate;



    int count;
    public Spot currentButton;
    int currentTarget;

    private void    Awake()
    {
        buttons = new List<Spot>();
        foreach ( Transform child in transform )
        {
			child.GetComponent<Button>().onClick.RemoveAllListeners();

			if ( Random.value > 0.5f )
			{
				child.tag = "red";
				Disabled	= CanvasManager.Instance.DisabledRed;
				Highlighted = CanvasManager.Instance.HighlightedRed;
				Enabled		= CanvasManager.Instance.EnabledRed;

			}
			else
			{
				child.tag = "blue";
				Disabled	= CanvasManager.Instance.DisabledBlue;
				Highlighted = CanvasManager.Instance.HighlightedBlue;
				Enabled		= CanvasManager.Instance.EnabledBlue;
			}

			Spot s = child.gameObject.AddComponent<Spot>();
			s.interactable = false;
			buttons.Add(s);
        }
        currentButton = buttons[0];
//        currentButton.interactable = false;
        currentButton.GetComponentInChildren<Image>().sprite = Disabled;
		gameObject.AddComponent<ImageFlip>();
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
        if(count < buttonLife)
        {
            count++;

            if(count == timeIlluminate)
            {

                IlluminateButton(buttons[currentTarget]);

            }
            if(count == timeActivate)
            {
                currentButton.interactable = true;
                currentButton.GetComponentInChildren<Image>().sprite = Enabled;

                if(currentTarget == buttons.Count-1)
                {
                    // spawn next enemy
                    CanvasManager.Instance.SpawnNextEnemy();
                }

            }
        }
        else
        {
            currentButton.interactable = false;
            currentButton.GetComponentInChildren<Image>().sprite = Disabled;

            if (currentTarget < buttons.Count - 1)
            {
                currentButton = buttons[++currentTarget];
                count = 0;
            }
            else
            {
                // killed
                OnKill();
            }

            
        }

    }

    public void OnKill()
    {
        // CanvasManager.Instance.SpawnNextEnemy();

        gameObject.SetActive(false); 


    }

    public  void    OnClick(Spot button)
    {
        if (currentTarget == buttons.Count - 1)
        {
            OnKill();
            return;
        }

        button.interactable = false;
        currentButton.GetComponentInChildren<Image>().sprite = Disabled;

		CanvasManager.Instance.HUDref.AddScore( score);
    }

	public  void    OnWrongClick( Spot button )
    {
        CanvasManager.Instance.HUDref.ReduceBar();
		if ( CanvasManager.Instance.HUDref.barHP.fillAmount <= 0f )
		{
			print( "HAI PERSO IGNORANTE" );
		}
    }


    void IlluminateButton(Spot button)
    {

        currentButton.GetComponentInChildren<Image>().sprite = Highlighted;
        
    }

}

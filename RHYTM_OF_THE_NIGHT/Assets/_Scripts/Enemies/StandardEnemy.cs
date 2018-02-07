using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class StandardEnemy : MonoBehaviour {


    public Sprite Disabled, Highlighted, Enabled;



    private List<Button> buttons;
    public int buttonLife;
    public int timeIlluminate;
    public int timeActivate;



    int count;
    public Button currentButton;
    int currentTarget;

    private void    Awake()
    {
        buttons = new List<Button>();
        foreach ( Transform child in transform )
        {
            buttons.Add(child.GetComponent<Button>());
        }
        currentButton = buttons[0];
        currentButton.interactable = false;
        currentButton.GetComponentInChildren<Image>().sprite = Disabled;

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
                GetComponentInChildren<Image>().sprite = Enabled;

                if(currentTarget == buttons.Count-1)
                {
                    // spawn next enemy
                }

            }
        }
        else
        {
            currentButton.interactable = false;
            GetComponentInChildren<Image>().sprite = Disabled;

            if (currentTarget < buttons.Count - 1)
            {
                currentButton = buttons[++currentTarget];
                count = 0;
            }
            else
            {
                // killed
            }

            
        }

    }


    public  void    OnClick(Button button)
    {
        button.interactable = false;
        currentButton.GetComponentInChildren<Image>().sprite = Disabled;
    }


    void IlluminateButton(Button button)
    {

        GetComponentInChildren<Image>().sprite = Highlighted;
        
    }

}

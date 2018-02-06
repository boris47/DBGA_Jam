using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class EnemyVins : MonoBehaviour {

    #region Public
    public List<Button> buttons;
    public int buttonLife;
    public int timeIlluminate;
    public int timeActivate;
    #endregion

    #region Private
    int count;
    public Button currentButton;
    int currentTarget;
    #endregion

    Vector3 destinationPoint;
    Tweener dotWeenCoroutine = null;
    Vector3 startPosition;




    public void OnSpawn()
    {
        startPosition = transform.position;
        destinationPoint = transform.Find("DestinationPoint").position;
        dotWeenCoroutine = transform.DOMoveX(destinationPoint.x, buttonLife * buttons.Count /3).
            OnComplete (
            delegate {
                destinationPoint = startPosition;
                dotWeenCoroutine = transform.DOMoveX(destinationPoint.x, buttonLife * buttons.Count /3);
                }
            );
    }


    public void Kill( bool hasToWait)
    {
        if (hasToWait == false)
            dotWeenCoroutine.Kill();

        EnemySequenceActivator.Instance.SpawnNextEnemy(hasToWait);
    }


    public void ActiveButton()
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

                ColorBlock cb = currentButton.colors;
                cb.disabledColor = new Color(255f, 255f, 255f, .5f);
                currentButton.colors = cb;

                currentButton.interactable = true;

                if(currentTarget == buttons.Count-1)
                {
                    Kill( true );
                }

            }
        }
        else
        {

            currentButton.interactable = false;

            if (currentTarget < buttons.Count - 1)
            {
                currentButton = buttons[++currentTarget];
                count = 0;
            }

            else
            {
                Kill( false );
            }

            
        }

    }


    void IlluminateButton(Button button)
    {

        ColorBlock cb = button.colors;
        cb.disabledColor = new Color(cb.disabledColor.r, cb.disabledColor.g, cb.disabledColor.b, 1f);
        button.colors = cb;

    }

}

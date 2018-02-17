using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProvaSpawn : MonoBehaviour {

    public GameObject spot;
    //public GameObject buttonPanel;
    public GameObject background;
    public GameObject buttonContainer;
    public List<RectTransform> buttonPanels;

    RectTransform rectBackground;
    //RectTransform rectPanel;
    RectTransform rectSpot;
    int counter = -1;

    private void Awake()
    {

        /*for (int i = 0; i < buttonContainer.transform.childCount; i++)
        {
            buttonPanels.Add(buttonContainer.transform.GetChild(i).gameObject.GetComponent<RectTransform>());
        }*/    

        rectBackground = background.GetComponent<RectTransform>();
        rectSpot = spot.GetComponent<RectTransform>();
    }

    // Use this for initialization
    void Start () 
    {
        FMOD_BeatListener.Instance.OnMark += OnMark;
        FMOD_BeatListener.Instance.OnBeat += OnBeat;
	}
	
	// Update is called once per frame
	void Update () 
    {

        if (FMOD_BeatListener.Instance.IsPaused == true)
            return;
		
	}

    private void OnBeat(int i)
    {

    }


    private void OnMark(string markName)
    {
        if (enabled == false)
            return;

        NextButton();
    }


    public void NextButton()
    {

        counter += 1;

        if(counter >= buttonPanels.Count)
        {
            counter = 0;
        }

        //rectPanel = buttonPanels[counter].GetComponent<RectTransform>();

        float panelXPosition = buttonPanels[counter].transform.position.x;
        float panelYPosition = buttonPanels[counter].transform.position.y;

        float randomX = Random.Range(panelXPosition + rectSpot.rect.width / 2, panelXPosition + buttonPanels[counter].rect.width - rectSpot.rect.width / 2);
        float randomY = Random.Range(panelYPosition + rectSpot.rect.width / 2, buttonPanels[counter].rect.height - rectSpot.rect.width / 2);

        Instantiate(spot, new Vector3(randomX, randomY, 0f), Quaternion.identity, buttonPanels[counter].transform);

    }

}

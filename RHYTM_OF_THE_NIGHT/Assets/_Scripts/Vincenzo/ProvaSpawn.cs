using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProvaSpawn : MonoBehaviour {

    public GameObject spot;
    public GameObject buttonPanel;
    public GameObject background;

    RectTransform rectBackground;
    RectTransform rectPanel;
    RectTransform rectSpot;

    private void Awake()
    {
        rectBackground = background.GetComponent<RectTransform>();
        rectPanel = buttonPanel.GetComponent<RectTransform>();
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

        float panelXPosition = rectPanel.transform.position.x;
        float panelYPosition = rectPanel.transform.position.y;

        float randomX = Random.Range(rectSpot.rect.width / 2, rectPanel.rect.width - rectSpot.rect.width / 2);
        float randomY = Random.Range(panelYPosition / 2, rectPanel.rect.height);

        Instantiate(spot, new Vector3(randomX, randomY, 0f), Quaternion.identity, buttonPanel.transform);

    }




}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProvaSpawn : MonoBehaviour {

    public GameObject spot;
    public GameObject background;
    public GameObject buttonContainer;
    public List<RectTransform> buttonPanels;

    public List<RectTransform> panelsInactive;

    RectTransform rectBackground;
    RectTransform rectSpot;
    int counter = -1;

    private void Awake()
    {
        panelsInactive = new List<RectTransform>();
        foreach(RectTransform panel in buttonPanels)
        {
            panelsInactive.Add(panel);
        }

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

	
	GameObject prevObj = null;
    private void OnMark(string markName)
    {
        if (enabled == false)
            return;

		if ( markName == "XXX" )
		{
			SpawnSpot();
			return;
		}


		prevObj.GetComponent<Clicker>().Interactable = true;
    }

    public void SpawnSpot()
    {

        if (panelsInactive.Count < 5)
        {
            panelsInactive.Clear();
            foreach (RectTransform panel in buttonPanels)
            {
                panelsInactive.Add(panel);
            }
        }

        counter = Random.Range(0, panelsInactive.Count);

        float panelXPosition = panelsInactive[counter].transform.position.x;
        float panelYPosition = panelsInactive[counter].transform.position.y;

        float randomX = Random.Range(panelXPosition + rectSpot.rect.width / 2, panelXPosition + panelsInactive[counter].rect.width - rectSpot.rect.width / 2);
        float randomY = Random.Range(panelYPosition + rectSpot.rect.width / 2, panelYPosition + panelsInactive[counter].rect.height - rectSpot.rect.width / 2);

        prevObj = Instantiate(spot, new Vector3(randomX, randomY, 0f), Quaternion.identity, panelsInactive[counter].transform);

        panelsInactive.Remove(panelsInactive[counter]);

    }

}

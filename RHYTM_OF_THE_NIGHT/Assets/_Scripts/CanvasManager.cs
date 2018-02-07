using UnityEngine;
using System.Collections.Generic;

public class CanvasManager : MonoBehaviour
{

    public static CanvasManager Instance = null;


    public int currentEnemyIndex = 0;
    public List<Transform> spawnPoints;


    bool isGameStarted;

    private void Awake()
    {
        Instance = this;

        foreach(Transform child in transform)
        {
            spawnPoints.Add(child);
        }
    }

    public  void    StartGame()
    {
        if (isGameStarted == false)
        {
            spawnPoints[0].gameObject.SetActive(true);
            isGameStarted = true;
        }
    }

    public void SpawnNextEnemy()
    {
        currentEnemyIndex ++;

        if ( currentEnemyIndex == spawnPoints.Count)
        {
            // TODO: next scene load
            return;
        }

        spawnPoints[currentEnemyIndex].gameObject.SetActive(true);

    }

    

    public  void    OnClick()
    {

        // damage player

    }
    

}

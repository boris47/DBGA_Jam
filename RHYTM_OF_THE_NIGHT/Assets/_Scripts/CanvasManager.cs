using UnityEngine;
using System.Collections.Generic;

public class CanvasManager : MonoBehaviour
{

    public int currentEnemyIndex = 0;
    public List<Transform> spawnPoints;


    private void Start()
    {
        foreach(Transform child in transform)
        {
            spawnPoints.Add(child);
        }
        spawnPoints[0].gameObject.SetActive( true );
    }

    public void SpawnNextEnemy()
    {
        currentEnemyIndex++;
        spawnPoints[currentEnemyIndex].gameObject.SetActive(true);

    }

    

    public  void    OnClick()
    {

        // damage player

    }
    

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProvaVins : MonoBehaviour
{
    public static ProvaVins Instance;

    public Canvas canvas;

    public ScriptableEnemiesList list;

    private void Start()
    {
        Instance = this;
    }

    public void SpawnEnemy()
    {
        EnemyVins e = list.enemiesList[Random.Range(0, list.enemiesList.Count)];

        Vector3 randomPosition = new Vector3
        (
            x: Random.value > .5f ? Screen.width + 100f : Screen.width / 2 - 100f,
            y: 0f,
            z: 0f
        );

        var e1 = Instantiate(e, canvas.transform);
        e1.transform.localPosition = randomPosition;

    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySequenceActivator : MonoBehaviour {

    public static EnemySequenceActivator Instance = null;

    public Transform enemyContainer = null;


    public EnemyVins currentEnemy = null;
    private List<EnemyVins> enemyList = null;
    private int enemyCounter = 0;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        enemyList = new List<EnemyVins>();
        foreach( Transform child in enemyContainer )
        {
            enemyList.Add(child.GetComponent<EnemyVins>());

        }

        currentEnemy = enemyList[0];
        currentEnemy.gameObject.SetActive(true);
        currentEnemy.OnSpawn();
    }




    public   void    OnBeat()
    {
        if (currentEnemy != null)
            currentEnemy.ActiveButton();
    }




    public  void SpawnNextEnemy( bool hasToWait )
    {
        if (enemyCounter == enemyList.Count)
            return;

        enemyList[enemyCounter + 1].gameObject.SetActive(true);

        if (hasToWait)
            StartCoroutine(Waiter());
    }

    IEnumerator Waiter()
    {

        yield return new WaitForSecondsRealtime(2f);
        SetCurrentEnemy();

    }

    public  void    SetCurrentEnemy()
    {
        enemyCounter++;

        currentEnemy.gameObject.SetActive(false);

        currentEnemy = enemyList[enemyCounter];

        currentEnemy.OnSpawn();


    }





}

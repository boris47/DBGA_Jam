
using UnityEngine;

public class SpawnPoint : MonoBehaviour {

    public GameObject EnemyToSpawn;


    GameObject Enemy;

    private  void    Start()
    {
        if (EnemyToSpawn == null)
            return;

        Enemy =  Instantiate(EnemyToSpawn, transform.position, Quaternion.identity, transform.parent);
        Enemy.transform.SetAsFirstSibling();
        Enemy.SetActive( false );
    }


    public  void    Show()
    {
        Enemy.SetActive(true);
    }


    public void     Hide()
    {
        Enemy.SetActive(false);
    }


}

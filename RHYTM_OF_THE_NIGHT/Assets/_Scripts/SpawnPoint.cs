
using UnityEngine;

public class SpawnPoint : MonoBehaviour {

    public GameObject EnemyToSpawn;

    public GameObject Enemy;

    private  void    Start()
    {
        if (EnemyToSpawn == null)
            return;

        Enemy =  Instantiate(EnemyToSpawn, transform.position, Quaternion.identity, transform.parent);
        Enemy.SetActive( false );
    }


    public  void    Show()
    {
        Enemy.SetActive(true);
    }

    public void Hide()
    {
        Enemy.SetActive(false);
    }




}

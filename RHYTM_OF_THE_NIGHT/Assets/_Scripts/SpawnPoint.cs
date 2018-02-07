
using UnityEngine;
using System.Collections;

public class SpawnPoint : MonoBehaviour {

    public GameObject EnemyToSpawn;

    GameObject Enemy;

    private  void    Start()
    {
        if (EnemyToSpawn == null)
            return;

        Enemy =  Instantiate(EnemyToSpawn, transform.position, Quaternion.identity, transform);

		Transform canvas = transform.parent;

		if ( EnemyToSpawn.name.Contains( "Far" ) )
		{
			transform.SetAsFirstSibling();
		}

		if ( EnemyToSpawn.name.Contains( "Mid" ) )
		{
			transform.SetSiblingIndex( 1 );
		}

		if ( EnemyToSpawn.name.Contains( "Near" ) )
		{
			transform.SetAsLastSibling();
		}
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

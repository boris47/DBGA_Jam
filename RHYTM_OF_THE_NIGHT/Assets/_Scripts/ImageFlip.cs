using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImageFlip : MonoBehaviour {

	public void	Flip()
	{
		transform.localScale = new Vector3( transform.localScale.x * -1f, transform.localScale.y, transform.localScale.z );
	}

}

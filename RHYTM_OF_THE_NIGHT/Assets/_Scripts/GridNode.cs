using UnityEngine;
using System.Collections;

public class GridNode : MonoBehaviour {
	
	public	GridNode[]		Neighbours			= null;

	public	float			gCost				= float.MaxValue;

	public	float			Heuristic			= 0f;

	public	GridNode		Parent				= null;


}

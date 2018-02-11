using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GraphMaker : MonoBehaviour {

	public	static	GraphMaker		Instance			= null;
	[SerializeField]
	private	GridNode[]				Nodes				= null;
	public	int						NodeCount
	{
		get
		{
			if ( Nodes != null )
				return Nodes.Length;
			return 0;
		}
	}

	public	float					scanRadius			= 1.1f;



	//////////////////////////////////////////////////////////////////////////
	// AWAKE
	private	void	Awake ()
	{
		Instance = this;

		// Find all nodes
		Nodes = FindObjectsOfType<GridNode>();

		Debug.Log( "Nodes: " + Nodes.Length );

		// neighbours setup
		foreach ( GridNode node in Nodes )
		{
			UpdateNeighbours( node );
		}
	}


	//////////////////////////////////////////////////////////////////////////
	// UpdaeNeighbours
	public	void	UpdateNeighbours( GridNode iNode )
	{
		// Get neighbours by distance
		iNode.Neighbours = System.Array.FindAll
		( 
			Nodes, 
			n => ( n.transform.position - iNode.transform.position ).sqrMagnitude <= scanRadius * scanRadius &&
			n != iNode
		);
	}

	//////////////////////////////////////////////////////////////////////////
	// ResetCosts
	internal	void	ResetNodes()
	{
		foreach ( GridNode node in Nodes )
		{
			node.gCost	= float.MaxValue;
			node.Parent = null;
		}
	}

}

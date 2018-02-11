using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AStarSearch : MonoBehaviour {
	
	public	static	AStarSearch Instance = null;

	//////////////////////////////////////////////////////////////////////////
	// AWAKE
	private	void	Awake()
	{
		Instance = this;
	}


	//////////////////////////////////////////////////////////////////////////
	// GetBestNode
	private GridNode GetBestNode(List<GridNode> set, bool useHeuristic)
	{
		GridNode bestNode = null;
		float bestTotal = float.MaxValue;

		foreach( GridNode n in set )
		{
			var totalCost = useHeuristic ? n.gCost + n.Heuristic : n.gCost;
			if (totalCost < bestTotal)
			{
				bestTotal = totalCost;
				bestNode = n;
			}
		}
		return bestNode;
	}


	//////////////////////////////////////////////////////////////////////////
	// FindPath
	private	GridNode[]	RetracePath( GridNode startNode, GridNode endNode )
	{
		List<GridNode> path = new List<GridNode>();
		GridNode currentNode = endNode;
		while ( currentNode != startNode )
		{
			path.Add( currentNode );
			currentNode = currentNode.Parent;
		}

		path.Reverse();
		GraphMaker.Instance.ResetNodes();
		return path.ToArray();
	}



	//////////////////////////////////////////////////////////////////////////
	// FindPath
	public GridNode[]	FindPath( GridNode startNode, GridNode endNode )
	{
		HashSet<GridNode>	closedSet	= new HashSet<GridNode>();
		List<GridNode>		openSet		= new List<GridNode>();

		startNode.gCost = 0;
		startNode.Heuristic = ( startNode.transform.position - endNode.transform.position ).sqrMagnitude;
		openSet.Add( startNode );

		// Start scan
		while ( openSet.Count > 0 )
		{
			GridNode currentNode = GetBestNode( openSet, true );
			if ( currentNode == null )
			{
				print( "currentNode = null" );
				GraphMaker.Instance.ResetNodes();
				return null;
			}
			if ( currentNode == endNode )
			{
			//	Debug.Log("We found the end node!");
				return RetracePath( startNode, endNode );
			}



			// First node is always discovered
			closedSet.Add( currentNode );
			openSet.Remove( currentNode );

			// Setup its neighbours
			foreach( GridNode iNeighbour in currentNode.Neighbours )
			{
				// Ignore the neighbor which is already evaluated.
				Clicker clicker = iNeighbour.GetComponent<Clicker>();
				if ( clicker.IsActive == true || closedSet.Contains( iNeighbour ) == true )
					continue;

				float gCost = currentNode.gCost + ( currentNode.transform.position - iNeighbour.transform.position ).sqrMagnitude;
				if ( gCost < iNeighbour.gCost || openSet.Contains(iNeighbour) == false )
				{
					iNeighbour.gCost		= gCost;
					iNeighbour.Heuristic	= ( iNeighbour.transform.position - endNode.transform.position ).sqrMagnitude;
					iNeighbour.Parent		= currentNode;

					if ( openSet.Contains( iNeighbour ) == false )
						openSet.Add( iNeighbour );
				}

			}
		}

		GraphMaker.Instance.ResetNodes();
		print( "no path found" );
		// no path found
		return null;
	}

}

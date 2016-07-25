using UnityEngine;
using System.Collections;

public class Grid : MonoBehaviour {

	public Transform player;
	public LayerMask unwalkableMask;
	public Vector3 gridWorldSize;
	public float nodeRadius;
	Node[,,] grid;

	float nodeDiameter;
	int gridSizeX;
	int gridSizeY;
	int gridSizeZ;

	void Start() {
		nodeDiameter = nodeRadius * 2;
		gridSizeX = Mathf.RoundToInt (gridWorldSize.x / nodeDiameter);
		gridSizeY = Mathf.RoundToInt (gridWorldSize.y / nodeDiameter);
		gridSizeZ = Mathf.RoundToInt (gridWorldSize.z / nodeDiameter);
		CreateGrid ();
	}

	void CreateGrid() {
		grid = new Node[gridSizeX, gridSizeY, gridSizeZ];

		Vector3 worldBottomLeft = transform.position - Vector3.right * gridWorldSize.x / 2 -
			Vector3.forward * gridWorldSize.y / 2;

		Gizmos.color = Color.green;

		for (int x = 0; x < gridSizeX; x++) {
			for (int y = 0; y < gridSizeY; y++) {
				for (int z = 0; z < gridSizeZ; z++) {
					Vector3 worldPoint = worldBottomLeft + Vector3.right * (x + nodeDiameter + nodeRadius - 1) +
					                    Vector3.forward * (y + nodeDiameter + nodeRadius - 1) + 
										Vector3.up * (z + nodeDiameter + nodeRadius - 1);
					bool walkable = !(Physics.CheckSphere (worldPoint, nodeRadius, unwalkableMask));
					grid [x, y, z] = new Node (walkable, worldPoint);
				}
			}
		}

	}

	public Node NodeFromWorldPoint(Vector3 worldPosition) {
		float percentX = (worldPosition.x + gridWorldSize.x / 2) / gridWorldSize.x;
		float percentY = (worldPosition.z + gridWorldSize.y / 2) / gridWorldSize.y;
		float percentZ = (worldPosition.z + gridWorldSize.z / 2) / gridWorldSize.z;
		percentX = Mathf.Clamp01 (percentX);
		percentY = Mathf.Clamp01 (percentY);
		percentZ = Mathf.Clamp01 (percentZ);

		int x = Mathf.RoundToInt ((gridSizeX - 1) * percentX);
		int y = Mathf.RoundToInt ((gridSizeY - 1) * percentY);
		int z = Mathf.RoundToInt ((gridSizeZ - 1) * percentZ);
		return grid [x, y, z];	
	}

	void OnDrawGizmos() {
		Vector3 pos = transform.position;
		pos.y += gridWorldSize.y / 2;
		Gizmos.DrawWireCube(pos, new Vector3(gridWorldSize.x, gridWorldSize.y, gridWorldSize.y));
	
		if (grid != null) {
			Node playerNode = NodeFromWorldPoint (player.position);
			foreach (Node n in grid) {
				Gizmos.color = (n.walkable) ? Color.white : Color.red;
				if (playerNode == n) {
					Gizmos.color = Color.cyan;				
				}
				Gizmos.DrawCube (n.worldPosition, Vector3.one * (nodeDiameter - 0.1f));
			}
		}

		nodeDiameter = nodeRadius * 2;
		gridSizeX = Mathf.RoundToInt (gridWorldSize.x / nodeDiameter);
		gridSizeY = Mathf.RoundToInt (gridWorldSize.y / nodeDiameter);
		gridSizeZ = Mathf.RoundToInt (gridWorldSize.z / nodeDiameter);
		CreateGrid ();
	}

}

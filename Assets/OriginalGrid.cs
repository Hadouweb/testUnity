﻿using UnityEngine;
using System.Collections;

public class OriginalGrid : MonoBehaviour {

	public Transform player;
	public LayerMask unwalkableMask;
	public Vector2 gridWorldSize;
	public float nodeRadius;
	Node[,] grid;

	float nodeDiameter;
	int gridSizeX;
	int gridSizeY;

	void Start() {
		nodeDiameter = nodeRadius * 2;
		gridSizeX = Mathf.RoundToInt (gridWorldSize.x / nodeDiameter);
		gridSizeY = Mathf.RoundToInt (gridWorldSize.y / nodeDiameter);
		CreateGrid ();
	}

	void CreateGrid() {
		grid = new Node[gridSizeX, gridSizeY];

		Vector3 worldBottomLeft = transform.position - Vector3.right * gridWorldSize.x / 2 -
			Vector3.forward * gridWorldSize.y / 2;

//		Gizmos.color = Color.green;
//		Gizmos.DrawCube (worldBottomLeft, Vector3.one * 2f);

		for (int x = 0; x < gridSizeX; x++) {
			for (int y = 0; y < gridSizeY; y++) {
				Vector3 worldPoint = worldBottomLeft + Vector3.right * (x + nodeDiameter + nodeRadius - 1) +
					Vector3.forward * (y + nodeDiameter + nodeRadius - 1);
				bool walkable = !(Physics.CheckSphere (worldPoint, nodeRadius, unwalkableMask));
				grid [x, y] = new Node (walkable, worldPoint);
			}
		}

	}

	public Node NodeFromWorldPoint(Vector3 worldPosition) {
		float percentX = (worldPosition.x + gridWorldSize.x / 2) / gridWorldSize.x;
		float percentY = (worldPosition.z + gridWorldSize.y / 2) / gridWorldSize.y;
		percentX = Mathf.Clamp01 (percentX);
		percentY = Mathf.Clamp01 (percentY);

		int x = Mathf.RoundToInt ((gridSizeX - 1) * percentX);
		int y = Mathf.RoundToInt ((gridSizeY - 1) * percentY);
		return grid [x, y];	
	}

	void OnDrawGizmos() {
		Vector3 pos = transform.position;
		Gizmos.DrawWireCube(pos, new Vector3(gridWorldSize.x, 1, gridWorldSize.y));
	
		if (grid != null) {
			Node playerNode = NodeFromWorldPoint (player.position);
			foreach (Node n in grid) {
				Gizmos.color = (n.walkable) ? new Color(255, 255, 255, 0.02f) : Color.red;
				if (playerNode == n) {
					Gizmos.color = Color.cyan;				
				}
				Gizmos.DrawCube (n.worldPosition, Vector3.one * (nodeDiameter - 0.1f));
			}
		}

		nodeDiameter = nodeRadius * 2;
		gridSizeX = Mathf.RoundToInt (gridWorldSize.x / nodeDiameter);
		gridSizeY = Mathf.RoundToInt (gridWorldSize.y / nodeDiameter);
		CreateGrid ();
	}

}

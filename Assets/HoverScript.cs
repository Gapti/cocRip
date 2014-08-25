﻿using UnityEngine;
using System.Collections;

public class HoverScript : MonoBehaviour {
	
	public GameObject Obj;
	public TileMaps tileMaps;

	// Update is called once per frame
	void Update () {
		RaycastHit hit;
		Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);

		if (Physics.Raycast (ray, out hit, 1000)) 
		{
			Vector3 calcPoint = getTilePoints(hit.point);
			this.transform.localPosition = new Vector3(calcPoint.x, 0f, calcPoint.y);

			if(Input.GetMouseButtonDown(0))
			{
				//from bottom right corner 0
				SpawnPiece ( Mathf.RoundToInt( (calcPoint.y / 10 ) * 10 + (calcPoint.x * 10)), new Vector3(calcPoint.x, 0f, calcPoint.y));
			}

			if(Input.GetMouseButtonDown(1))
			{
				tileMaps.RemoveObject(Mathf.RoundToInt( (calcPoint.y / 10 ) * 10 + (calcPoint.x * 10)));
			}
		}
	}

	void SpawnPiece(int index, Vector3 pos)
	{
		GameObject temp = (GameObject)Instantiate (Obj, pos, Quaternion.identity);

		if (tileMaps.CheckPosition (temp, index) == false) 
		{
			Destroy(temp);
		}

	}

	// Convert world space floor points to tile points
	Vector2 getTilePoints(Vector3 floorPoints)
	{
		Vector2 tilePoints = new Vector2();
		// Convert the space points to tile points
		tilePoints.x = (int)(floorPoints.x / 1f);
		tilePoints.y = (int)(floorPoints.z / 1f);

		// Return the tile points
		return tilePoints;
	}
}

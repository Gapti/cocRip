using UnityEngine;
using System.Collections;

public class BuildManager : MonoBehaviour {
	
	public GameObject Obj;

	private TileMaps _tileMaps;
	private GameObject _hover;

	private bool _drag = false;

	public GameObject temp;
	private Item item;

	public int MapWidth = 50;
	public int MapHeight = 50;

	//Mathf.RoundToInt( (calcPoint.y / 10 ) * 10 + (calcPoint.x * 10)), new Vector3(calcPoint.x, 0f, calcPoint.y)


	void Awake()
	{
		_tileMaps = GetComponent<TileMaps> ();
		_hover = GameObject.FindGameObjectWithTag ("Hover");
	}

	// Update is called once per frame
	void Update () {

		if (!_drag)
			return;

		RaycastHit hit;
		Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);

		if (Physics.Raycast (ray, out hit, 1000)) 
		{
			Vector3 calcPoint = getTilePoints(hit.point);

			temp.transform.localPosition = new Vector3(calcPoint.x, 0f, calcPoint.y);
			CheckAllTilePositions(hit.point);
		}
	}

	public void StartBuild()
	{
		temp = (GameObject)Instantiate (Obj, Input.mousePosition, Quaternion.identity);
		item = (Item)temp.GetComponent<Item> ();
		_drag = true;
	}

	void CheckAllTilePositions(Vector3 centerPoint)
	{
		Vector3 rightEdge = new Vector3 (centerPoint.x + 1, centerPoint.y, centerPoint.z);

		Vector3 leftEdge = new Vector3 (centerPoint.x - 1, centerPoint.y, centerPoint.z);

		Vector3 topEdge = new Vector3 (centerPoint.x, centerPoint.y, centerPoint.z + 1);

		Vector3 bottomEdge = new Vector3 (centerPoint.x, centerPoint.y, centerPoint.z - 1);


		// the size of the grid here
		if (rightEdge.x > MapWidth || leftEdge.x < 0 || topEdge.z > MapHeight || bottomEdge.z < 0) 
		{
			item.HightLight.renderer.material.SetColor ("_Color", Color.red);
		} 
		else 
		{			
			item.HightLight.renderer.material.SetColor ("_Color", Color.green);
		}
		   
	}


	// Convert world space floor points to tile points
	Vector2 getTilePoints(Vector3 floorPoints)
	{
		Vector2 tilePoints = new Vector2();
		// Convert the space points to tile points
		tilePoints.x = (int)(floorPoints.x / 1f);
		tilePoints.y = (int)(floorPoints.z / 1f);

		print ("y " + tilePoints.y + " x " + tilePoints.x); 

		// Return the tile points
		return tilePoints;
	}
}

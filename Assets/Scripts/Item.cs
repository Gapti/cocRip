using UnityEngine;
using System.Collections;

public enum Direction
{
	None,
	North,
	South,
	East,
	West
}

public class Item : MonoBehaviour  {

	public int id;
	public int CellWidth;
	public int CellHeight;
	public string ItemName;
	public string ItemDesc;
	public Direction direction = Direction.None;
	public GameObject HightLight;
}

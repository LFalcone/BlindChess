using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour {

	public List<List<GameObject>> tiles;
	public GameObject Tile;

	void Start()
	{
		MakeBoard (4,6);
	}
	void MakeBoard(int x, int y)
	{
		tiles = new List<List<GameObject>>();
		for (int i = 0; i < x; ++i) 
		{
			for (int j = 0; j < y; ++j) 
			{
				GameObject myTile = Instantiate(Tile);
				myTile.transform.parent = this.transform;
				myTile.transform.position = new Vector3 (i, 0, j);
				if ((i + j) % 2 == 0) 
				{
					Tile tileScript = myTile.GetComponent<Tile> ();
					tileScript.setBlack ();
				}
			}
		}
	}
}

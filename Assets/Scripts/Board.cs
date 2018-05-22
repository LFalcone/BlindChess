
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour {

	public GameObject[,] tiles;
	public GameObject Tile;
	public GameObject King;
	public int x=4;
	public int y=6;

	void Start()
	{
		tiles = new GameObject[x, y];
		MakeBoard();
		SetPieces ();
	}
	void MakeBoard()
	{		
		for (int i = 0; i < x; ++i) 
		{
			for (int j = 0; j < y; ++j) 
			{
				GameObject myTile = Instantiate(Tile);
				myTile.transform.parent = this.transform;
				myTile.transform.position = new Vector3 (i, 0, j);
				Tile tileScript = myTile.GetComponent<Tile> ();
				if ((i + j) % 2 == 0) 
				{
					tileScript.setBlack ();
				}
				tileScript.setPos (i, j);
				tiles [i,j] = myTile;
			}
		}
	}

	void SetPieces()
	{
		GameObject myTile = tiles [0,2];
		Tile tileScript = myTile.GetComponent<Tile> ();
		tileScript.setPiece (King, "white");
	}
}

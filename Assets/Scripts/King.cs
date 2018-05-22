using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class King : Piece {

	private Tile tileScript = tile.GetComponent<Tile> ();
	private ArrayList<ArrayList<int>> movement = { { -1, -1 }, { -1, 0 }, { -1, 1 }, { 0, -1 }, { 0, 0 }, { 0, 1 }, { 1, -1 }, { 1, 0 }, { 1, 1 } };

	ArrayList<ArrayList<int>> options()
	{
		ArrayList<ArrayList<int>> returnList = new List<List<int>>();
		int index = 0;
		ArrayList<int> currentPos = tileScript.pos;
		for (int i = 0; i < movement.Count; ++i) 
		{
			ArrayList<int> newPos = { currentPos [0] + movement [i] [0], currentPos [1] + movement [i] [1] };
			if (newPos [0] >= 0 && newPos [0] <= 3 && newPos [1] >= 0 && newPos [1] <= 5) 
			{
				returnList [index] = newPos;
				index++;
			}
		}
		return returnList;
	}
}


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour {

	public GameObject[,] tiles;
	public GameObject Tile;
	public GameObject King;
	public int x=4;
	public int y=6;
	public int[] selectedSpace;

	public string selectedPiece;

	private Vector2 mouseOver;
	private Vector2 startDrag;
	private Vector2 endDrag;

	void Start()
	{
		selectedPiece = "none";
		selectedSpace = new int[] {-1,-1};
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
				if ((i + j) % 2 == 0) {
					tileScript.setBlack ();
				} else 
				{
					tileScript.setWhite ();
				}
				tileScript.setPos (i, j);
				tiles [i,j] = myTile;
			}
		}
	}

	void SetPieces()
	{
		// Spawn Kings
		GameObject myTile = tiles [2,0];
		Tile tileScript = myTile.GetComponent<Tile> ();
		tileScript.setPiece ("whiteKing", "white");
		myTile = tiles [2,5];
		tileScript = myTile.GetComponent<Tile> ();
		tileScript.setPiece ("blackKing", "black");

		// Spawn Queens
		myTile = tiles [1,0];
		tileScript = myTile.GetComponent<Tile> ();
		tileScript.setPiece ("whiteQueen", "white");
		myTile = tiles [1,5];
		tileScript = myTile.GetComponent<Tile> ();
		tileScript.setPiece ("blackQueen", "black");

		// Spawn Pawns
		for (int j = 0; j < 4; ++j) {
			myTile = tiles [j,1];
			tileScript = myTile.GetComponent<Tile> ();
			tileScript.setPiece ("whitePawn", "white");
			myTile = tiles [j,4];
			tileScript = myTile.GetComponent<Tile> ();
			tileScript.setPiece ("blackPawn", "black");
		}

		// Get and Spawn Chosen Pieces
		List<int> choices = GetComponent<LoadOnClick> ().GetChoices ();
		for (int j = 0; j < 4; ++j) {
			string side = "";
			int y;
			if (j < 2) {
				side += "white";
				y = 0;
			} else {
				side += "black";
				y = 5;
			}
			string piece = side;
			if (choices [j] == 0)
				piece += "Bishop";
			else if (choices [j] == 1)
				piece += "Knight";
			else if (choices [j] == 2)
				piece += "Rook";
			Debug.Log (piece + "!");
			myTile = tiles [j%2*3,y];
			tileScript = myTile.GetComponent<Tile> ();
			tileScript.setPiece (piece, side);
		}
	}

	void UpdateMouseOver()
	{
		// If its my turn 
		if (!Camera.main)
		{
			Debug.Log("Unable to find Main Camera");
			return;
		}
		RaycastHit hit;
		if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 25.0f))
		{
			mouseOver.x = (int)(hit.point.x);
			mouseOver.y = (int)(hit.point.z);
		}
		else
		{
			mouseOver.x = -1;
			mouseOver.y = -1;
		}
		if (Input.GetMouseButtonDown (0))
		{
			//this will be moved later
			if (selectedSpace [0] != -1 && selectedSpace [1] != -1) {
				GameObject oldTile = tiles [selectedSpace [0], selectedSpace [1]];
				Tile oldScript = oldTile.GetComponent<Tile> ();
				oldScript.selectSpace (false);
			}
			//end of part to be moved later

			if (selectedPiece == "none") 
			{		
				selectedSpace [0] = (int)mouseOver.x;
				selectedSpace [1] = (int)mouseOver.y;
				if (selectedSpace [0] != -1 && selectedSpace [1] != -1) {
					GameObject myTile = tiles [selectedSpace [0], selectedSpace [1]];
					Tile tileScript = myTile.GetComponent<Tile> ();
					tileScript.selectSpace (true);
					selectedPiece = tileScript.piece;
					//Kings
					if (tileScript.piece == "whiteKing") {
						//check if adjacent spaces are on the board
						bool left = false;
						bool right = false;
						bool down = false;
						bool up = false;
						if (selectedSpace [0] > 0) { //not on the left wall
							left = true; //cam move left
							Debug.Log ("left");
						}
						if (selectedSpace [0] < 5) { //not on the right wall
							right = true; //cam move left
							Debug.Log ("right");
						}
						if (selectedSpace [1] > 0) { //not on the upper wall
							down = true; //cam move up
							Debug.Log ("down");
						}
						if (selectedSpace [1] < 5) { //not on the lower wall
							up = true; //cam move down
							Debug.Log ("up");
						}

						if (left) {
							GameObject checkTile = tiles [selectedSpace [0] - 1, selectedSpace [1]];
							Tile checkScript = checkTile.GetComponent<Tile> ();
							if (checkScript.state == 0) {
								checkScript.setMove ();
							} else if (checkScript.state == 2) {
								checkScript.setKill ();
							}
							if (up) {
								checkTile = tiles [selectedSpace [0] - 1, selectedSpace [1] + 1];
								checkScript = checkTile.GetComponent<Tile> ();
								if (checkScript.state == 0) {
									checkScript.setMove ();
								} else if (checkScript.state == 2) {
									checkScript.setKill ();
								}
							}
							if (down) {
								checkTile = tiles [selectedSpace [0] - 1, selectedSpace [1] - 1];
								checkScript = checkTile.GetComponent<Tile> ();
								if (checkScript.state == 0) {
									checkScript.setMove ();
								} else if (checkScript.state == 2) {
									checkScript.setKill ();
								}
							}
						}
						if (right) {
							GameObject checkTile = tiles [selectedSpace [0] + 1, selectedSpace [1]];
							Tile checkScript = checkTile.GetComponent<Tile> ();
							if (checkScript.state == 0) {
								checkScript.setMove ();
							} else if (checkScript.state == 2) {
								checkScript.setKill ();
							}
							if (up) {
								checkTile = tiles [selectedSpace [0] + 1, selectedSpace [1] + 1];
								checkScript = checkTile.GetComponent<Tile> ();
								if (checkScript.state == 0) {
									checkScript.setMove ();
								} else if (checkScript.state == 2) {
									checkScript.setKill ();
								}
							}
							if (down) {
								checkTile = tiles [selectedSpace [0] + 1, selectedSpace [1] - 1];
								checkScript = checkTile.GetComponent<Tile> ();
								if (checkScript.state == 0) {
									checkScript.setMove ();
								} else if (checkScript.state == 2) {
									checkScript.setKill ();
								}
							}
						}
						if (up) {
							GameObject checkTile = tiles [selectedSpace [0], selectedSpace [1] + 1];
							Tile checkScript = checkTile.GetComponent<Tile> ();
							if (checkScript.state == 0) {
								checkScript.setMove ();
							} else if (checkScript.state == 2) {
								checkScript.setKill ();
							}
						}
						if (down) {
							GameObject checkTile = tiles [selectedSpace [0], selectedSpace [1] - 1];
							Tile checkScript = checkTile.GetComponent<Tile> ();
							if (checkScript.state == 0) {
								checkScript.setMove ();
							} else if (checkScript.state == 2) {
								checkScript.setKill ();
							}
						}
					}
				
				} 
				else 
				{
					selectedPiece = "none";
				}
				
	//old stuff that i am reworking but would like to keep for now to reference
	/*		selectedSpace [0] = (int)mouseOver.x;
			selectedSpace [1] = (int)mouseOver.y;

			if (selectedSpace [0] != -1 && selectedSpace [1] != -1) 
			{
				GameObject newTile = tiles [selectedSpace [0], selectedSpace [1]];
				Tile tileScript = newTile.GetComponent<Tile> ();
				tileScript.selectSpace (true);

				if (oldString == "whiteKing") {
					tileScript.setPiece (King, "white");
				} 
				else if (oldString == "blackKing") 
				{
					tileScript.setPiece (King, "black");
				}

				Debug.Log (tileScript.piece);
				Debug.Log ("x=" + mouseOver.x);
				Debug.Log ("y=" + mouseOver.y);
			}*/
		}
	}
	}

	void clearSelections()
	{
		for (int i = 0; i < x; ++i) {
			for (int j = 0; j < y; ++j) {
				GameObject myTile = tiles [i, j];
				Tile tileScript = myTile.GetComponent<Tile> ();
			}
		}

	}

	void Update()
	{
		UpdateMouseOver();
	}

}

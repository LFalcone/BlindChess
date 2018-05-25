
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour {
	public int victory = 0;

	public GameObject[,] tiles;
	public GameObject Tile;
	public int x=4;
	public int y=6;
	public int[] selectedSpace;

	public string selectedPiece;

	public GameObject cam;
	public TurnHandler turnScript;

	private Vector2 mouseOver;
	private Vector2 startDrag;
	private Vector2 endDrag;

	void Start()
	{
		selectedPiece = "empty";
		selectedSpace = new int[] {-1,-1};
		tiles = new GameObject[x, y];
		MakeBoard();
		SetPieces ();
		turnScript = cam.GetComponent<TurnHandler> ();
		RevealByTeam (turnScript.playerNum());
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

	void RevealAll()
	{
		for (int i = 0; i < x; ++i) {
			for (int j = 0; j < y; ++j) {
				GameObject myTile = tiles [i, j];
				Tile tileScript = myTile.GetComponent<Tile> ();
				tileScript.Reveal (true);
			}
		}
	}

	void RevealByTeam(int s)
	{
		for (int i = 0; i < x; ++i) {
			for (int j = 0; j < y; ++j) {
				GameObject myTile = tiles [i, j];
				Tile tileScript = myTile.GetComponent<Tile> ();
				tileScript.Reveal (s == tileScript.state);
			}
		}
		for (int i = 0; i < x; ++i) {
			for (int j = 0; j < y; ++j) {
				GameObject myTile = tiles [i, j];
				Tile tileScript = myTile.GetComponent<Tile> ();
				int[] sSpace = {i,j};
				/////////
				//Kings//
				/////////
				if (tileScript.piece == "whiteKing") {
					//check if adjacent spaces are on the board
					bool left = false;
					bool right = false;
					bool down = false;
					bool up = false;
					if (sSpace [0] > 0) { //not on the left wall
						left = true; //can move left
					}
					if (sSpace [0] < 3) { //not on the right wall
						right = true; //can move left
					}
					if (sSpace [1] > 0) { //not on the upper wall
						down = true; //can move up
					}
					if (sSpace [1] < 5) { //not on the lower wall
						up = true; //can move down
					}

					if (left) {
						GameObject checkTile = tiles [sSpace [0] - 1, sSpace [1]];
						Tile checkScript = checkTile.GetComponent<Tile> ();
						if (checkScript.state == 2) {
							checkScript.Reveal (true); tileScript.Reveal (true);
						}
						if (up) {
							checkTile = tiles [sSpace [0] - 1, sSpace [1] + 1];
							checkScript = checkTile.GetComponent<Tile> ();
							if (checkScript.state == 2) {
								checkScript.Reveal (true); tileScript.Reveal (true);
							}
						}
						if (down) {
							checkTile = tiles [sSpace [0] - 1, sSpace [1] - 1];
							checkScript = checkTile.GetComponent<Tile> ();
							if (checkScript.state == 2) {
								checkScript.Reveal (true); tileScript.Reveal (true);
							}
						}
					}
					if (right) {
						GameObject checkTile = tiles [sSpace [0] + 1, sSpace [1]];
						Tile checkScript = checkTile.GetComponent<Tile> ();
							if (checkScript.state == 2) {
								checkScript.Reveal (true); tileScript.Reveal (true);
							}
						if (up) {
							checkTile = tiles [sSpace [0] + 1, sSpace [1] + 1];
							checkScript = checkTile.GetComponent<Tile> ();
							if (checkScript.state == 2) {
								checkScript.Reveal (true); tileScript.Reveal (true);
							}
						}
						if (down) {
							checkTile = tiles [sSpace [0] + 1, sSpace [1] - 1];
							checkScript = checkTile.GetComponent<Tile> ();
							if (checkScript.state == 2) {
								checkScript.Reveal (true); tileScript.Reveal (true);
							}
						}
					}
					if (up) {
						GameObject checkTile = tiles [sSpace [0], sSpace [1] + 1];
						Tile checkScript = checkTile.GetComponent<Tile> ();
						if (checkScript.state == 2) {
							checkScript.Reveal (true); tileScript.Reveal (true);
						}
					}
					if (down) {
						GameObject checkTile = tiles [sSpace [0], sSpace [1] - 1];
						Tile checkScript = checkTile.GetComponent<Tile> ();
						if (checkScript.state == 2) {
							checkScript.Reveal (true); tileScript.Reveal (true);
						}
					}
				}
				if (tileScript.piece == "blackKing") {
					//check if adjacent spaces are on the board
					bool left = false;
					bool right = false;
					bool down = false;
					bool up = false;
					if (sSpace [0] > 0) { //not on the left wall
						left = true; //can move left
					}
					if (sSpace [0] < 3) { //not on the right wall
						right = true; //can move left
					}
					if (sSpace [1] > 0) { //not on the upper wall
						down = true; //can move up
					}
					if (sSpace [1] < 5) { //not on the lower wall
						up = true; //can move down
					}

					if (left) {
						GameObject checkTile = tiles [sSpace [0] - 1, sSpace [1]];
						Tile checkScript = checkTile.GetComponent<Tile> ();
						if (checkScript.state == 1){
							checkScript.Reveal (true); tileScript.Reveal (true);
						}
						if (up) {
							checkTile = tiles [sSpace [0] - 1, sSpace [1] + 1];
							checkScript = checkTile.GetComponent<Tile> ();
							if (checkScript.state == 1){
								checkScript.Reveal (true); tileScript.Reveal (true);
							}
						}
						if (down) {
							checkTile = tiles [sSpace [0] - 1, sSpace [1] - 1];
							checkScript = checkTile.GetComponent<Tile> ();
							if (checkScript.state == 1){
								checkScript.Reveal (true); tileScript.Reveal (true);
							}
						}
					}
					if (right) {
						GameObject checkTile = tiles [sSpace [0] + 1, sSpace [1]];
						Tile checkScript = checkTile.GetComponent<Tile> ();
						if (checkScript.state == 1){
							checkScript.Reveal (true); tileScript.Reveal (true);
						}
						if (up) {
							checkTile = tiles [sSpace [0] + 1, sSpace [1] + 1];
							checkScript = checkTile.GetComponent<Tile> ();
							if (checkScript.state == 1){
								checkScript.Reveal (true); tileScript.Reveal (true);
							}
						}
						if (down) {
							checkTile = tiles [sSpace [0] + 1, sSpace [1] - 1];
							checkScript = checkTile.GetComponent<Tile> ();
							if (checkScript.state == 1){
								checkScript.Reveal (true); tileScript.Reveal (true);
							}
						}
					}
					if (up) {
						GameObject checkTile = tiles [sSpace [0], sSpace [1] + 1];
						Tile checkScript = checkTile.GetComponent<Tile> ();
						if (checkScript.state == 1){
							checkScript.Reveal (true); tileScript.Reveal (true);
						}
					}
					if (down) {
						GameObject checkTile = tiles [sSpace [0], sSpace [1] - 1];
						Tile checkScript = checkTile.GetComponent<Tile> ();
						if (checkScript.state == 1){
							checkScript.Reveal (true); tileScript.Reveal (true);
						}
					}
				}
				// END OF KINGS

				/////////
				//ROOKS//
				/////////
				if (tileScript.piece == "whiteRook") {
					int[] current = new int[2];
					current[0] = sSpace[0];
					current[1] = sSpace[1];
					while (current [0] > 0) {
						--current [0];
						GameObject checkTile = tiles [current [0], current [1]];
						Tile checkScript = checkTile.GetComponent<Tile> ();
						if (checkScript.state == 1) {
							break;
						} else if (checkScript.state == 2) {
							checkScript.Reveal (true); tileScript.Reveal (true);
							break;
						}
					}
					current[0] = sSpace[0];
					current[1] = sSpace[1];
					while (current [0] < 3) {
						++current [0];
						GameObject checkTile = tiles [current [0], current [1]];
						Tile checkScript = checkTile.GetComponent<Tile> ();
						if (checkScript.state == 1) {
							break;
						} else if (checkScript.state == 2) {
							checkScript.Reveal (true); tileScript.Reveal (true);
							break;
						}
					}
					current[0] = sSpace[0];
					current[1] = sSpace[1];
					while (current [1] > 0) {
						--current [1];
						GameObject checkTile = tiles [current [0], current [1]];
						Tile checkScript = checkTile.GetComponent<Tile> ();
						if (checkScript.state == 1) {
							break;
						} else if (checkScript.state == 2) {
							checkScript.Reveal (true); tileScript.Reveal (true);
							break;
						}
					}
					current[0] = sSpace[0];
					current[1] = sSpace[1];
					while (current [1] < 5) {
						++current [1];
						GameObject checkTile = tiles [current [0], current [1]];
						Tile checkScript = checkTile.GetComponent<Tile> ();
						if (checkScript.state == 1) {
							break;
						} else if (checkScript.state == 2) {
							checkScript.Reveal (true); tileScript.Reveal (true);
							break;
						}
					}
				}
				if (tileScript.piece == "blackRook") {
					int[] current = new int[2];
					current[0] = sSpace[0];
					current[1] = sSpace[1];
					while (current [0] > 0) {
						--current[0];
						GameObject checkTile = tiles [current [0], current [1]];
						Tile checkScript = checkTile.GetComponent<Tile> ();
						if (checkScript.state == 1) {
							checkScript.Reveal (true); tileScript.Reveal (true);
							break;
						} else if (checkScript.state == 2) {
							break;
						}
					}
					current[0] = sSpace[0];
					current[1] = sSpace[1];
					while (current [0] < 3) {
						++current[0];
						GameObject checkTile = tiles [current [0], current [1]];
						Tile checkScript = checkTile.GetComponent<Tile> ();
						if (checkScript.state == 1) {
							checkScript.Reveal (true); tileScript.Reveal (true);
							break;
						} else if (checkScript.state == 2) {
							break;
						}
					}
					current[0] = sSpace[0];
					current[1] = sSpace[1];
					while (current [1] > 0) {
						--current[1];
						GameObject checkTile = tiles [current [0], current [1]];
						Tile checkScript = checkTile.GetComponent<Tile> ();
						if (checkScript.state == 1) {
							checkScript.Reveal (true); tileScript.Reveal (true);
							break;
						} else if (checkScript.state == 2) {
							break;
						}
					}
					current[0] = sSpace[0];
					current[1] = sSpace[1];
					while (current [1] < 5) {
						++current[1];
						GameObject checkTile = tiles [current [0], current [1]];
						Tile checkScript = checkTile.GetComponent<Tile> ();
						if (checkScript.state == 1) {
							checkScript.Reveal (true); tileScript.Reveal (true);
							break;
						} else if (checkScript.state == 2) {
							break;
						}
					}
				}
				// END OF ROOKS

				///////////
				//BISHOPS//
				///////////
				if (tileScript.piece == "whiteBishop") {
					int[] current = new int[2];
					current[0] = sSpace[0];
					current[1] = sSpace[1];
					while (current [0] > 0 && current[1] > 0) {
						--current [0]; --current [1];
						GameObject checkTile = tiles [current [0], current [1]];
						Tile checkScript = checkTile.GetComponent<Tile> ();
						if (checkScript.state == 1) {
							break;
						} else if (checkScript.state == 2) {
							checkScript.Reveal (true); tileScript.Reveal (true);
							break;

						}
					}
					current[0] = sSpace[0];
					current[1] = sSpace[1];
					while (current [0] < 3 && current[1] < 5) {
						++current [0]; ++current [1];
						GameObject checkTile = tiles [current [0], current [1]];
						Tile checkScript = checkTile.GetComponent<Tile> ();
						if (checkScript.state == 1) {
							break;
						} else if (checkScript.state == 2) {
							checkScript.Reveal (true); tileScript.Reveal (true);
							break;
						}
					}
					current[0] = sSpace[0];
					current[1] = sSpace[1];
					while (current [0] < 3 && current [1] > 0) {
						++current [0]; --current [1];
						GameObject checkTile = tiles [current [0], current [1]];
						Tile checkScript = checkTile.GetComponent<Tile> ();
						if (checkScript.state == 1) {
							break;
						} else if (checkScript.state == 2) {
							checkScript.Reveal (true); tileScript.Reveal (true);
							break;
						}
					}
					current[0] = sSpace[0];
					current[1] = sSpace[1];
					while (current [0] > 0 && current [1] < 5) {
						--current [0]; ++current [1];
						GameObject checkTile = tiles [current [0], current [1]];
						Tile checkScript = checkTile.GetComponent<Tile> ();
						if (checkScript.state == 1) {
							break;
						} else if (checkScript.state == 2) {
							checkScript.Reveal (true); tileScript.Reveal (true);
							break;
						}
					}
				}
				if (tileScript.piece == "blackBishop") {
					int[] current = new int[2];
					current[0] = sSpace[0];
					current[1] = sSpace[1];
					while (current [0] > 0 && current[1] > 0) {
						--current [0]; --current [1];
						GameObject checkTile = tiles [current [0], current [1]];
						Tile checkScript = checkTile.GetComponent<Tile> ();
						if (checkScript.state == 1) {
							checkScript.Reveal (true); tileScript.Reveal (true);
							break;
						} else if (checkScript.state == 2) {
							break;

						}
					}
					current[0] = sSpace[0];
					current[1] = sSpace[1];
					while (current [0] < 3 && current[1] < 5) {
						++current [0]; ++current [1];
						GameObject checkTile = tiles [current [0], current [1]];
						Tile checkScript = checkTile.GetComponent<Tile> ();
						if (checkScript.state == 1) {
							checkScript.Reveal (true); tileScript.Reveal (true);
							break;
						} else if (checkScript.state == 2) {
							break;
						}
					}
					current[0] = sSpace[0];
					current[1] = sSpace[1];
					while (current [0] < 3 && current [1] > 0) {
						++current [0]; --current [1];
						GameObject checkTile = tiles [current [0], current [1]];
						Tile checkScript = checkTile.GetComponent<Tile> ();
						if (checkScript.state == 1) {
							checkScript.Reveal (true); tileScript.Reveal (true);
							break;
						} else if (checkScript.state == 2) {
							break;
						}
					}
					current[0] = sSpace[0];
					current[1] = sSpace[1];
					while (current [0] > 0 && current [1] < 5) {
						--current [0]; ++current [1];
						GameObject checkTile = tiles [current [0], current [1]];
						Tile checkScript = checkTile.GetComponent<Tile> ();
						if (checkScript.state == 1) {
							checkScript.Reveal (true); tileScript.Reveal (true);
							break;
						} else if (checkScript.state == 2) {
							break;
						}
					}
				}
				// END OF BISHOPS

				//////////
				//QUEENS//
				//////////
				if (tileScript.piece == "whiteQueen") {
					int[] current = new int[2];
					current[0] = sSpace[0];
					current[1] = sSpace[1];
					while (current [0] > 0) {
						--current [0];
						GameObject checkTile = tiles [current [0], current [1]];
						Tile checkScript = checkTile.GetComponent<Tile> ();
						if (checkScript.state == 1) {
							break;
						} else if (checkScript.state == 2) {
							checkScript.Reveal (true); tileScript.Reveal (true);
							break;

						}
					}
					current[0] = sSpace[0];
					current[1] = sSpace[1];
					while (current [0] < 3) {
						++current [0];
						GameObject checkTile = tiles [current [0], current [1]];
						Tile checkScript = checkTile.GetComponent<Tile> ();
						if (checkScript.state == 1) {
							break;
						} else if (checkScript.state == 2) {
							checkScript.Reveal (true); tileScript.Reveal (true);
							break;
						}
					}
					current[0] = sSpace[0];
					current[1] = sSpace[1];
					while (current [1] > 0) {
						--current [1];
						GameObject checkTile = tiles [current [0], current [1]];
						Tile checkScript = checkTile.GetComponent<Tile> ();
						if (checkScript.state == 1) {
							break;
						} else if (checkScript.state == 2) {
							checkScript.Reveal (true); tileScript.Reveal (true);
							break;
						}
					}
					current[0] = sSpace[0];
					current[1] = sSpace[1];
					while (current [1] < 5) {
						++current [1];
						GameObject checkTile = tiles [current [0], current [1]];
						Tile checkScript = checkTile.GetComponent<Tile> ();
						if (checkScript.state == 1) {
							break;
						} else if (checkScript.state == 2) {
							checkScript.Reveal (true); tileScript.Reveal (true);
							break;
						}
					}
					current[0] = sSpace[0];
					current[1] = sSpace[1];
					while (current [0] > 0 && current[1] > 0) {
						--current [0]; --current [1];
						GameObject checkTile = tiles [current [0], current [1]];
						Tile checkScript = checkTile.GetComponent<Tile> ();
						if (checkScript.state == 1) {
							break;
						} else if (checkScript.state == 2) {
							checkScript.Reveal (true); tileScript.Reveal (true);
							break;

						}
					}
					current[0] = sSpace[0];
					current[1] = sSpace[1];
					while (current [0] < 3 && current[1] < 5) {
						++current [0]; ++current [1];
						GameObject checkTile = tiles [current [0], current [1]];
						Tile checkScript = checkTile.GetComponent<Tile> ();
						if (checkScript.state == 1) {
							break;
						} else if (checkScript.state == 2) {
							checkScript.Reveal (true); tileScript.Reveal (true);
							break;
						}
					}
					current[0] = sSpace[0];
					current[1] = sSpace[1];
					while (current [0] < 3 && current [1] > 0) {
						++current [0]; --current [1];
						GameObject checkTile = tiles [current [0], current [1]];
						Tile checkScript = checkTile.GetComponent<Tile> ();
						if (checkScript.state == 1) {
							break;
						} else if (checkScript.state == 2) {
							checkScript.Reveal (true); tileScript.Reveal (true);
							break;
						}
					}
					current[0] = sSpace[0];
					current[1] = sSpace[1];
					while (current [0] > 0 && current [1] < 5) {
						--current [0]; ++current [1];
						GameObject checkTile = tiles [current [0], current [1]];
						Tile checkScript = checkTile.GetComponent<Tile> ();
						if (checkScript.state == 1) {
							break;
						} else if (checkScript.state == 2) {
							checkScript.Reveal (true); tileScript.Reveal (true);
							break;
						}
					}
				}
				if (tileScript.piece == "blackQueen") {
					int[] current = new int[2];
					current[0] = sSpace[0];
					current[1] = sSpace[1];
					while (current [0] > 0) {
						--current[0];
						GameObject checkTile = tiles [current [0], current [1]];
						Tile checkScript = checkTile.GetComponent<Tile> ();
						if (checkScript.state == 1) {
							checkScript.Reveal (true); tileScript.Reveal (true);
							break;
						} else if (checkScript.state == 2) {
							break;
						}
					}
					current[0] = sSpace[0];
					current[1] = sSpace[1];
					while (current [0] < 3) {
						++current[0];
						GameObject checkTile = tiles [current [0], current [1]];
						Tile checkScript = checkTile.GetComponent<Tile> ();
						if (checkScript.state == 1) {
							checkScript.Reveal (true); tileScript.Reveal (true);
							break;
						} else if (checkScript.state == 2) {
							break;
						}
					}
					current[0] = sSpace[0];
					current[1] = sSpace[1];
					while (current [1] > 0) {
						--current[1];
						GameObject checkTile = tiles [current [0], current [1]];
						Tile checkScript = checkTile.GetComponent<Tile> ();
						if (checkScript.state == 1) {
							checkScript.Reveal (true); tileScript.Reveal (true);
							break;
						} else if (checkScript.state == 2) {
							break;
						}
					}
					current[0] = sSpace[0];
					current[1] = sSpace[1];
					while (current [1] < 5) {
						++current[1];
						GameObject checkTile = tiles [current [0], current [1]];
						Tile checkScript = checkTile.GetComponent<Tile> ();
						if (checkScript.state == 1) {
							checkScript.Reveal (true); tileScript.Reveal (true);
							break;
						} else if (checkScript.state == 2) {
							break;
						}
					}
					current[0] = sSpace[0];
					current[1] = sSpace[1];
					while (current [0] > 0 && current[1] > 0) {
						--current [0]; --current [1];
						GameObject checkTile = tiles [current [0], current [1]];
						Tile checkScript = checkTile.GetComponent<Tile> ();
						if (checkScript.state == 1) {
							checkScript.Reveal (true); tileScript.Reveal (true);
							break;
						} else if (checkScript.state == 2) {
							break;

						}
					}
					current[0] = sSpace[0];
					current[1] = sSpace[1];
					while (current [0] < 3 && current[1] < 5) {
						++current [0]; ++current [1];
						GameObject checkTile = tiles [current [0], current [1]];
						Tile checkScript = checkTile.GetComponent<Tile> ();
						if (checkScript.state == 1) {
							checkScript.Reveal (true); tileScript.Reveal (true);
							break;
						} else if (checkScript.state == 2) {
							break;
						}
					}
					current[0] = sSpace[0];
					current[1] = sSpace[1];
					while (current [0] < 3 && current [1] > 0) {
						++current [0]; --current [1];
						GameObject checkTile = tiles [current [0], current [1]];
						Tile checkScript = checkTile.GetComponent<Tile> ();
						if (checkScript.state == 1) {
							checkScript.Reveal (true); tileScript.Reveal (true);
							break;
						} else if (checkScript.state == 2) {
							break;
						}
					}
					current[0] = sSpace[0];
					current[1] = sSpace[1];
					while (current [0] > 0 && current [1] < 5) {
						--current [0]; ++current [1];
						GameObject checkTile = tiles [current [0], current [1]];
						Tile checkScript = checkTile.GetComponent<Tile> ();
						if (checkScript.state == 1) {
							checkScript.Reveal (true); tileScript.Reveal (true);
							break;
						} else if (checkScript.state == 2) {
							break;
						}
					}
				}
				// END OF QUEENS

				///////////
				//KNIGHTS//
				///////////
				if (tileScript.piece == "whiteKnight") {
					if (sSpace [0] - 2 >= 0) {
						if (sSpace [1] - 1 >= 0) {
							GameObject checkTile = tiles [sSpace [0] - 2, sSpace [1] - 1];
							Tile checkScript = checkTile.GetComponent<Tile> ();
							if (checkScript.state == 2) {
								checkScript.Reveal (true); tileScript.Reveal (true);
							}
						}
						if (sSpace [1] + 1 <= 5) {
							GameObject checkTile = tiles [sSpace [0] - 2, sSpace [1] + 1];
							Tile checkScript = checkTile.GetComponent<Tile> ();
							if (checkScript.state == 2) {
								checkScript.Reveal (true); tileScript.Reveal (true); tileScript.Reveal (true);
							}
						}
					}
					if (sSpace [0] + 2 <= 3) {
						if (sSpace [1] - 1 >= 0) {
							GameObject checkTile = tiles [sSpace [0] + 2, sSpace [1] - 1];
							Tile checkScript = checkTile.GetComponent<Tile> ();
							if (checkScript.state == 2) {
								checkScript.Reveal (true); tileScript.Reveal (true);
							}
						}
						if (sSpace [1] + 1 <= 5) {
							GameObject checkTile = tiles [sSpace [0] + 2, sSpace [1] + 1];
							Tile checkScript = checkTile.GetComponent<Tile> ();
							if (checkScript.state == 2) {
								checkScript.Reveal (true); tileScript.Reveal (true);
							}
						}
					}
					if (sSpace [1] - 2 >= 0) {
						if (sSpace [0] - 1 >= 0) {
							GameObject checkTile = tiles [sSpace [0] - 1, sSpace [1] - 2];
							Tile checkScript = checkTile.GetComponent<Tile> ();
							if (checkScript.state == 2) {
								checkScript.Reveal (true); tileScript.Reveal (true);
							}
						}
						if (sSpace [0] + 1 <= 3) {
							GameObject checkTile = tiles [sSpace [0] + 1, sSpace [1] - 2];
							Tile checkScript = checkTile.GetComponent<Tile> ();
							if (checkScript.state == 2) {
								checkScript.Reveal (true); tileScript.Reveal (true);
							}
						}
					}
					if (sSpace [1] + 2 <= 5) {
						if (sSpace [0] - 1 >= 0) {
							GameObject checkTile = tiles [sSpace [0] - 1, sSpace [1] + 2];
							Tile checkScript = checkTile.GetComponent<Tile> ();
							if (checkScript.state == 2) {
								checkScript.Reveal (true); tileScript.Reveal (true);
							}
						}
						if (sSpace [0] + 1 <= 3) {
							GameObject checkTile = tiles [sSpace [0] + 1, sSpace [1] + 2];
							Tile checkScript = checkTile.GetComponent<Tile> ();
							if (checkScript.state == 2) {
								checkScript.Reveal (true); tileScript.Reveal (true);
							}
						}
					}
				}
				if (tileScript.piece == "blackKnight") {
					if (sSpace [0] - 2 >= 0) {
						if (sSpace [1] - 1 >= 0) {
							GameObject checkTile = tiles [sSpace [0] - 2, sSpace [1] - 1];
							Tile checkScript = checkTile.GetComponent<Tile> ();
							if (checkScript.state == 1) {
								checkScript.Reveal (true); tileScript.Reveal (true);
							}
						}
						if (sSpace [1] + 1 <= 5) {
							GameObject checkTile = tiles [sSpace [0] - 2, sSpace [1] + 1];
							Tile checkScript = checkTile.GetComponent<Tile> ();
							if (checkScript.state == 1) {
								checkScript.Reveal (true); tileScript.Reveal (true);
							}
						}
					}
					if (sSpace [0] + 2 <= 3) {
						if (sSpace [1] - 1 >= 0) {
							GameObject checkTile = tiles [sSpace [0] + 2, sSpace [1] - 1];
							Tile checkScript = checkTile.GetComponent<Tile> ();
							if (checkScript.state == 1) {
								checkScript.Reveal (true); tileScript.Reveal (true);
							}
						}
						if (sSpace [1] + 1 <= 5) {
							GameObject checkTile = tiles [sSpace [0] + 2, sSpace [1] + 1];
							Tile checkScript = checkTile.GetComponent<Tile> ();
							if (checkScript.state == 1) {
								checkScript.Reveal (true); tileScript.Reveal (true);
							}
						}
					}
					if (sSpace [1] - 2 >= 0) {
						if (sSpace [0] - 1 >= 0) {
							GameObject checkTile = tiles [sSpace [0] - 1, sSpace [1] - 2];
							Tile checkScript = checkTile.GetComponent<Tile> ();
							if (checkScript.state == 1) {
								checkScript.Reveal (true); tileScript.Reveal (true);
							}
						}
						if (sSpace [0] + 1 <= 3) {
							GameObject checkTile = tiles [sSpace [0] + 1, sSpace [1] - 2];
							Tile checkScript = checkTile.GetComponent<Tile> ();
							if (checkScript.state == 1) {
								checkScript.Reveal (true); tileScript.Reveal (true);
							}
						}
					}
					if (sSpace [1] + 2 <= 5) {
						if (sSpace [0] - 1 >= 0) {
							GameObject checkTile = tiles [sSpace [0] - 1, sSpace [1] + 2];
							Tile checkScript = checkTile.GetComponent<Tile> ();
							if (checkScript.state == 1) {
								checkScript.Reveal (true); tileScript.Reveal (true);
							}
						}
						if (sSpace [0] + 1 <= 3) {
							GameObject checkTile = tiles [sSpace [0] + 1, sSpace [1] + 2];
							Tile checkScript = checkTile.GetComponent<Tile> ();
							if (checkScript.state == 1) {
								checkScript.Reveal (true); tileScript.Reveal (true);
							}
						}
					}
				}
				// END OF KNIGHTS

				/////////
				//PAWNS//
				/////////
				if (tileScript.piece == "whitePawn") {
					//check if adjacent spaces are on the board
					bool left = false;
					bool right = false;
					//bool down = false;
					bool up = false;
					if (sSpace [0] > 0) { //not on the left wall
						left = true; //can move left
					}
					if (sSpace [0] < 3) { //not on the right wall
						right = true; //can move left
					}
					if (sSpace [1] < 5) { //not on the upper wall
						up = true; //can move down
					}

					if (up) {
						GameObject checkTile = tiles [sSpace [0], sSpace [1] + 1];
						Tile checkScript = checkTile.GetComponent<Tile> ();
						if (checkScript.state == 2) {
							checkScript.Reveal (true); tileScript.Reveal (true);
						}

						if (left) {
							checkTile = tiles [sSpace [0] - 1, sSpace [1] + 1];
							checkScript = checkTile.GetComponent<Tile> ();
							if (checkScript.state == 2) {
								checkScript.Reveal (true); tileScript.Reveal (true);
							}
						}
						if (right) {
							checkTile = tiles [sSpace [0] + 1, sSpace [1] + 1];
							checkScript = checkTile.GetComponent<Tile> ();
							if (checkScript.state == 2) {
								checkScript.Reveal (true); tileScript.Reveal (true);
							}
						}
					}

				}
				if (tileScript.piece == "blackPawn") {
					//check if adjacent spaces are on the board
					bool left = false;
					bool right = false;
					bool down = false;
					//bool up = false;
					if (sSpace [0] > 0) { //not on the left wall
						left = true; //can move left
					}
					if (sSpace [0] < 3) { //not on the right wall
						right = true; //can move left
					}
					if (sSpace [1] > 0) { //not on the lower wall
						down = true; //can move down
					}

					if (down) {
						GameObject checkTile = tiles [sSpace [0], sSpace [1] - 1];
						Tile checkScript = checkTile.GetComponent<Tile> ();
						if (checkScript.state == 1) {
							checkScript.Reveal (true); tileScript.Reveal (true);
						}

						if (left) {
							checkTile = tiles [sSpace [0] - 1, sSpace [1] - 1];
							checkScript = checkTile.GetComponent<Tile> ();
							if (checkScript.state == 1) {
								checkScript.Reveal (true); tileScript.Reveal (true);
							}
						}
						if (right) {
							checkTile = tiles [sSpace [0] + 1, sSpace [1] - 1];
							checkScript = checkTile.GetComponent<Tile> ();
							if (checkScript.state == 1) {
								checkScript.Reveal (true); tileScript.Reveal (true);
							}
						}
					}

				}
				// END OF PAWNS
			}
		}
	}

	void RevealByPosition(int i, int j)
	{
		GameObject myTile = tiles [i, j];
		Tile tileScript = myTile.GetComponent<Tile> ();
		tileScript.Reveal (true);
	}

	void UpdateMouseOver()
	{
		// If its my turn 
		if (!Camera.main)
		{
			Debug.Log("Unable to find Main Camera");
			return;
		}
		Ray ray;
		ray = Camera.main.ScreenPointToRay (Input.mousePosition);
		RaycastHit hit;
		if (Physics.Raycast(ray, out hit))
		{
			GameObject clickedObject = hit.collider.gameObject;
			if (clickedObject.name == "Tile(Clone)") {
				Tile clicked = clickedObject.GetComponent<Tile> ();
				mouseOver.x = clicked.pos[0];
				mouseOver.y = clicked.pos[1];
			}
		}
		else
		{
			mouseOver.x = -1;
			mouseOver.y = -1;
		}
		if (Input.GetMouseButtonDown (0) && !GetComponentInChildren<TurnHandler>().isWaiting() && !GetComponentInChildren<TurnHandler>().hasTakenTurn())
		{
			//this will be moved later
			if (selectedSpace [0] != -1 && selectedSpace [1] != -1) {
				GameObject oldTile = tiles [selectedSpace [0], selectedSpace [1]];
				Tile oldScript = oldTile.GetComponent<Tile> ();
				oldScript.selectSpace (false);
			}
			//end of part to be moved later

			if (selectedPiece == "empty") {
				clearSelections ();

				selectedSpace [0] = (int)mouseOver.x;
				selectedSpace [1] = (int)mouseOver.y;
				if (selectedSpace [0] != -1 && selectedSpace [1] != -1) {
					GameObject myTile = tiles [selectedSpace [0], selectedSpace [1]];
					Tile tileScript = myTile.GetComponent<Tile> ();
					TurnHandler.Player turnPlayer = GetComponentInChildren<TurnHandler> ().GetTurnPlayer ();
					if(tileScript.state == 1 && turnPlayer == TurnHandler.Player.WHITE || tileScript.state == 2 && turnPlayer == TurnHandler.Player.BLACK) {
						tileScript.selectSpace (true);
						selectedPiece = tileScript.piece;
	
						/////////
						//Kings//
						/////////
						if (tileScript.piece == "whiteKing") {
							//check if adjacent spaces are on the board
							bool left = false;
							bool right = false;
							bool down = false;
							bool up = false;
							if (selectedSpace [0] > 0) { //not on the left wall
								left = true; //can move left
								Debug.Log ("left");
							}
							if (selectedSpace [0] < 3) { //not on the right wall
								right = true; //can move left
								Debug.Log ("right");
							}
							if (selectedSpace [1] > 0) { //not on the upper wall
								down = true; //can move up
								Debug.Log ("down");
							}
							if (selectedSpace [1] < 5) { //not on the lower wall
								up = true; //can move down
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
						if (tileScript.piece == "blackKing") {
							//check if adjacent spaces are on the board
							bool left = false;
							bool right = false;
							bool down = false;
							bool up = false;
							if (selectedSpace [0] > 0) { //not on the left wall
								left = true; //can move left
								Debug.Log ("left");
							}
							if (selectedSpace [0] < 3) { //not on the right wall
								right = true; //can move left
								Debug.Log ("right");
							}
							if (selectedSpace [1] > 0) { //not on the upper wall
								down = true; //can move up
								Debug.Log ("down");
							}
							if (selectedSpace [1] < 5) { //not on the lower wall
								up = true; //can move down
								Debug.Log ("up");
							}
	
							if (left) {
								GameObject checkTile = tiles [selectedSpace [0] - 1, selectedSpace [1]];
								Tile checkScript = checkTile.GetComponent<Tile> ();
								if (checkScript.state == 0) {
									checkScript.setMove ();
								} else if (checkScript.state == 1) {
									checkScript.setKill ();
								}
								if (up) {
									checkTile = tiles [selectedSpace [0] - 1, selectedSpace [1] + 1];
									checkScript = checkTile.GetComponent<Tile> ();
									if (checkScript.state == 0) {
										checkScript.setMove ();
									} else if (checkScript.state == 1) {
										checkScript.setKill ();
									}
								}
								if (down) {
									checkTile = tiles [selectedSpace [0] - 1, selectedSpace [1] - 1];
									checkScript = checkTile.GetComponent<Tile> ();
									if (checkScript.state == 0) {
										checkScript.setMove ();
									} else if (checkScript.state == 1) {
										checkScript.setKill ();
									}
								}
							}
							if (right) {
								GameObject checkTile = tiles [selectedSpace [0] + 1, selectedSpace [1]];
								Tile checkScript = checkTile.GetComponent<Tile> ();
								if (checkScript.state == 0) {
									checkScript.setMove ();
								} else if (checkScript.state == 1) {
									checkScript.setKill ();
								}
								if (up) {
									checkTile = tiles [selectedSpace [0] + 1, selectedSpace [1] + 1];
									checkScript = checkTile.GetComponent<Tile> ();
									if (checkScript.state == 0) {
										checkScript.setMove ();
									} else if (checkScript.state == 1) {
										checkScript.setKill ();
									}
								}
								if (down) {
									checkTile = tiles [selectedSpace [0] + 1, selectedSpace [1] - 1];
									checkScript = checkTile.GetComponent<Tile> ();
									if (checkScript.state == 0) {
										checkScript.setMove ();
									} else if (checkScript.state == 1) {
										checkScript.setKill ();
									}
								}
							}
							if (up) {
								GameObject checkTile = tiles [selectedSpace [0], selectedSpace [1] + 1];
								Tile checkScript = checkTile.GetComponent<Tile> ();
								if (checkScript.state == 0) {
									checkScript.setMove ();
								} else if (checkScript.state == 1) {
									checkScript.setKill ();
								}
							}
							if (down) {
								GameObject checkTile = tiles [selectedSpace [0], selectedSpace [1] - 1];
								Tile checkScript = checkTile.GetComponent<Tile> ();
								if (checkScript.state == 0) {
									checkScript.setMove ();
								} else if (checkScript.state == 1) {
									checkScript.setKill ();
								}
							}
						}
						// END OF KINGS
	
						/////////
						//ROOKS//
						/////////
						if (tileScript.piece == "whiteRook") {
							int[] current = new int[2];
							current[0] = selectedSpace[0];
							current[1] = selectedSpace[1];
							while (current [0] > 0) {
								--current [0];
								GameObject checkTile = tiles [current [0], current [1]];
								Tile checkScript = checkTile.GetComponent<Tile> ();
								if (checkScript.state == 0) {
									checkScript.setMove ();
								} else if (checkScript.state == 1) {
									break;
								} else if (checkScript.state == 2) {
									checkScript.setKill ();
									break;
							
								}
							}
							current[0] = selectedSpace[0];
							current[1] = selectedSpace[1];
							while (current [0] < 3) {
								++current [0];
								GameObject checkTile = tiles [current [0], current [1]];
								Tile checkScript = checkTile.GetComponent<Tile> ();
								if (checkScript.state == 0) {
									checkScript.setMove ();
								} else if (checkScript.state == 1) {
									break;
								} else if (checkScript.state == 2) {
									checkScript.setKill ();
									break;
								}
							}
							current[0] = selectedSpace[0];
							current[1] = selectedSpace[1];
							while (current [1] > 0) {
								--current [1];
								GameObject checkTile = tiles [current [0], current [1]];
								Tile checkScript = checkTile.GetComponent<Tile> ();
								if (checkScript.state == 0) {
									checkScript.setMove ();
								} else if (checkScript.state == 1) {
									break;
								} else if (checkScript.state == 2) {
									checkScript.setKill ();
									break;
								}
							}
							current[0] = selectedSpace[0];
							current[1] = selectedSpace[1];
							while (current [1] < 5) {
								++current [1];
								GameObject checkTile = tiles [current [0], current [1]];
								Tile checkScript = checkTile.GetComponent<Tile> ();
								if (checkScript.state == 0) {
									checkScript.setMove ();
								} else if (checkScript.state == 1) {
									break;
								} else if (checkScript.state == 2) {
									checkScript.setKill ();
									break;
								}
							}
						}
						if (tileScript.piece == "blackRook") {
							int[] current = new int[2];
							current[0] = selectedSpace[0];
							current[1] = selectedSpace[1];
							while (current [0] > 0) {
								--current[0];
								GameObject checkTile = tiles [current [0], current [1]];
								Tile checkScript = checkTile.GetComponent<Tile> ();
								if (checkScript.state == 0) {
									checkScript.setMove ();
								} else if (checkScript.state == 1) {
									checkScript.setKill ();
									break;
								} else if (checkScript.state == 2) {
									break;
								}
							}
							current[0] = selectedSpace[0];
							current[1] = selectedSpace[1];
							while (current [0] < 3) {
								++current[0];
								GameObject checkTile = tiles [current [0], current [1]];
								Tile checkScript = checkTile.GetComponent<Tile> ();
								if (checkScript.state == 0) {
									checkScript.setMove ();
								} else if (checkScript.state == 1) {
									checkScript.setKill ();
									break;
								} else if (checkScript.state == 2) {
									break;
								}
							}
							current[0] = selectedSpace[0];
							current[1] = selectedSpace[1];
							while (current [1] > 0) {
								--current[1];
								GameObject checkTile = tiles [current [0], current [1]];
								Tile checkScript = checkTile.GetComponent<Tile> ();
								if (checkScript.state == 0) {
									checkScript.setMove ();
								} else if (checkScript.state == 1) {
									checkScript.setKill ();
									break;
								} else if (checkScript.state == 2) {
									break;
								}
							}
							current[0] = selectedSpace[0];
							current[1] = selectedSpace[1];
							while (current [1] < 5) {
								++current[1];
								GameObject checkTile = tiles [current [0], current [1]];
								Tile checkScript = checkTile.GetComponent<Tile> ();
								if (checkScript.state == 0) {
									checkScript.setMove ();
								} else if (checkScript.state == 1) {
									checkScript.setKill ();
									break;
								} else if (checkScript.state == 2) {
									break;
								}
							}
						}
						// END OF ROOKS
	
						///////////
						//BISHOPS//
						///////////
						if (tileScript.piece == "whiteBishop") {
							int[] current = new int[2];
							current[0] = selectedSpace[0];
							current[1] = selectedSpace[1];
							while (current [0] > 0 && current[1] > 0) {
								--current [0]; --current [1];
								GameObject checkTile = tiles [current [0], current [1]];
								Tile checkScript = checkTile.GetComponent<Tile> ();
								if (checkScript.state == 0) {
									checkScript.setMove ();
								} else if (checkScript.state == 1) {
									break;
								} else if (checkScript.state == 2) {
									checkScript.setKill ();
									break;
	
								}
							}
							current[0] = selectedSpace[0];
							current[1] = selectedSpace[1];
							while (current [0] < 3 && current[1] < 5) {
								++current [0]; ++current [1];
								GameObject checkTile = tiles [current [0], current [1]];
								Tile checkScript = checkTile.GetComponent<Tile> ();
								if (checkScript.state == 0) {
									checkScript.setMove ();
								} else if (checkScript.state == 1) {
									break;
								} else if (checkScript.state == 2) {
									checkScript.setKill ();
									break;
								}
							}
							current[0] = selectedSpace[0];
							current[1] = selectedSpace[1];
							while (current [0] < 3 && current [1] > 0) {
								++current [0]; --current [1];
								GameObject checkTile = tiles [current [0], current [1]];
								Tile checkScript = checkTile.GetComponent<Tile> ();
								if (checkScript.state == 0) {
									checkScript.setMove ();
								} else if (checkScript.state == 1) {
									break;
								} else if (checkScript.state == 2) {
									checkScript.setKill ();
									break;
								}
							}
							current[0] = selectedSpace[0];
							current[1] = selectedSpace[1];
							while (current [0] > 0 && current [1] < 5) {
								--current [0]; ++current [1];
								GameObject checkTile = tiles [current [0], current [1]];
								Tile checkScript = checkTile.GetComponent<Tile> ();
								if (checkScript.state == 0) {
									checkScript.setMove ();
								} else if (checkScript.state == 1) {
									break;
								} else if (checkScript.state == 2) {
									checkScript.setKill ();
									break;
								}
							}
						}
						if (tileScript.piece == "blackBishop") {
							int[] current = new int[2];
							current[0] = selectedSpace[0];
							current[1] = selectedSpace[1];
							while (current [0] > 0 && current[1] > 0) {
								--current [0]; --current [1];
								GameObject checkTile = tiles [current [0], current [1]];
								Tile checkScript = checkTile.GetComponent<Tile> ();
								if (checkScript.state == 0) {
									checkScript.setMove ();
								} else if (checkScript.state == 1) {
									checkScript.setKill ();
									break;
								} else if (checkScript.state == 2) {
									break;
											
								}
							}
							current[0] = selectedSpace[0];
							current[1] = selectedSpace[1];
							while (current [0] < 3 && current[1] < 5) {
								++current [0]; ++current [1];
								GameObject checkTile = tiles [current [0], current [1]];
								Tile checkScript = checkTile.GetComponent<Tile> ();
								if (checkScript.state == 0) {
									checkScript.setMove ();
								} else if (checkScript.state == 1) {
									checkScript.setKill ();
									break;
								} else if (checkScript.state == 2) {
									break;
								}
							}
							current[0] = selectedSpace[0];
							current[1] = selectedSpace[1];
							while (current [0] < 3 && current [1] > 0) {
								++current [0]; --current [1];
								GameObject checkTile = tiles [current [0], current [1]];
								Tile checkScript = checkTile.GetComponent<Tile> ();
								if (checkScript.state == 0) {
									checkScript.setMove ();
								} else if (checkScript.state == 1) {
									checkScript.setKill ();
									break;
								} else if (checkScript.state == 2) {
									break;
								}
							}
							current[0] = selectedSpace[0];
							current[1] = selectedSpace[1];
							while (current [0] > 0 && current [1] < 5) {
								--current [0]; ++current [1];
								GameObject checkTile = tiles [current [0], current [1]];
								Tile checkScript = checkTile.GetComponent<Tile> ();
								if (checkScript.state == 0) {
									checkScript.setMove ();
								} else if (checkScript.state == 1) {
									checkScript.setKill ();
									break;
								} else if (checkScript.state == 2) {
									break;
								}
							}
						}
						// END OF BISHOPS
	
						//////////
						//QUEENS//
						//////////
						if (tileScript.piece == "whiteQueen") {
							int[] current = new int[2];
							current[0] = selectedSpace[0];
							current[1] = selectedSpace[1];
							while (current [0] > 0) {
								--current [0];
								GameObject checkTile = tiles [current [0], current [1]];
								Tile checkScript = checkTile.GetComponent<Tile> ();
								if (checkScript.state == 0) {
									checkScript.setMove ();
								} else if (checkScript.state == 1) {
									break;
								} else if (checkScript.state == 2) {
									checkScript.setKill ();
									break;
	
								}
							}
							current[0] = selectedSpace[0];
							current[1] = selectedSpace[1];
							while (current [0] < 3) {
								++current [0];
								GameObject checkTile = tiles [current [0], current [1]];
								Tile checkScript = checkTile.GetComponent<Tile> ();
								if (checkScript.state == 0) {
									checkScript.setMove ();
								} else if (checkScript.state == 1) {
									break;
								} else if (checkScript.state == 2) {
									checkScript.setKill ();
									break;
								}
							}
							current[0] = selectedSpace[0];
							current[1] = selectedSpace[1];
							while (current [1] > 0) {
								--current [1];
								GameObject checkTile = tiles [current [0], current [1]];
								Tile checkScript = checkTile.GetComponent<Tile> ();
								if (checkScript.state == 0) {
									checkScript.setMove ();
								} else if (checkScript.state == 1) {
									break;
								} else if (checkScript.state == 2) {
									checkScript.setKill ();
									break;
								}
							}
							current[0] = selectedSpace[0];
							current[1] = selectedSpace[1];
							while (current [1] < 5) {
								++current [1];
								GameObject checkTile = tiles [current [0], current [1]];
								Tile checkScript = checkTile.GetComponent<Tile> ();
								if (checkScript.state == 0) {
									checkScript.setMove ();
								} else if (checkScript.state == 1) {
									break;
								} else if (checkScript.state == 2) {
									checkScript.setKill ();
									break;
								}
							}
							current[0] = selectedSpace[0];
							current[1] = selectedSpace[1];
							while (current [0] > 0 && current[1] > 0) {
								--current [0]; --current [1];
								GameObject checkTile = tiles [current [0], current [1]];
								Tile checkScript = checkTile.GetComponent<Tile> ();
								if (checkScript.state == 0) {
									checkScript.setMove ();
								} else if (checkScript.state == 1) {
									break;
								} else if (checkScript.state == 2) {
									checkScript.setKill ();
									break;
	
								}
							}
							current[0] = selectedSpace[0];
							current[1] = selectedSpace[1];
							while (current [0] < 3 && current[1] < 5) {
								++current [0]; ++current [1];
								GameObject checkTile = tiles [current [0], current [1]];
								Tile checkScript = checkTile.GetComponent<Tile> ();
								if (checkScript.state == 0) {
									checkScript.setMove ();
								} else if (checkScript.state == 1) {
									break;
								} else if (checkScript.state == 2) {
									checkScript.setKill ();
									break;
								}
							}
							current[0] = selectedSpace[0];
							current[1] = selectedSpace[1];
							while (current [0] < 3 && current [1] > 0) {
								++current [0]; --current [1];
								GameObject checkTile = tiles [current [0], current [1]];
								Tile checkScript = checkTile.GetComponent<Tile> ();
								if (checkScript.state == 0) {
									checkScript.setMove ();
								} else if (checkScript.state == 1) {
									break;
								} else if (checkScript.state == 2) {
									checkScript.setKill ();
									break;
								}
							}
							current[0] = selectedSpace[0];
							current[1] = selectedSpace[1];
							while (current [0] > 0 && current [1] < 5) {
								--current [0]; ++current [1];
								GameObject checkTile = tiles [current [0], current [1]];
								Tile checkScript = checkTile.GetComponent<Tile> ();
								if (checkScript.state == 0) {
									checkScript.setMove ();
								} else if (checkScript.state == 1) {
									break;
								} else if (checkScript.state == 2) {
									checkScript.setKill ();
									break;
								}
							}
						}
						if (tileScript.piece == "blackQueen") {
							int[] current = new int[2];
							current[0] = selectedSpace[0];
							current[1] = selectedSpace[1];
							while (current [0] > 0) {
								--current[0];
								GameObject checkTile = tiles [current [0], current [1]];
								Tile checkScript = checkTile.GetComponent<Tile> ();
								if (checkScript.state == 0) {
									checkScript.setMove ();
								} else if (checkScript.state == 1) {
									checkScript.setKill ();
									break;
								} else if (checkScript.state == 2) {
									break;
								}
							}
							current[0] = selectedSpace[0];
							current[1] = selectedSpace[1];
							while (current [0] < 3) {
								++current[0];
								GameObject checkTile = tiles [current [0], current [1]];
								Tile checkScript = checkTile.GetComponent<Tile> ();
								if (checkScript.state == 0) {
									checkScript.setMove ();
								} else if (checkScript.state == 1) {
									checkScript.setKill ();
									break;
								} else if (checkScript.state == 2) {
									break;
								}
							}
							current[0] = selectedSpace[0];
							current[1] = selectedSpace[1];
							while (current [1] > 0) {
								--current[1];
								GameObject checkTile = tiles [current [0], current [1]];
								Tile checkScript = checkTile.GetComponent<Tile> ();
								if (checkScript.state == 0) {
									checkScript.setMove ();
								} else if (checkScript.state == 1) {
									checkScript.setKill ();
									break;
								} else if (checkScript.state == 2) {
									break;
								}
							}
							current[0] = selectedSpace[0];
							current[1] = selectedSpace[1];
							while (current [1] < 5) {
								++current[1];
								GameObject checkTile = tiles [current [0], current [1]];
								Tile checkScript = checkTile.GetComponent<Tile> ();
								if (checkScript.state == 0) {
									checkScript.setMove ();
								} else if (checkScript.state == 1) {
									checkScript.setKill ();
									break;
								} else if (checkScript.state == 2) {
									break;
								}
							}
							current[0] = selectedSpace[0];
							current[1] = selectedSpace[1];
							while (current [0] > 0 && current[1] > 0) {
								--current [0]; --current [1];
								GameObject checkTile = tiles [current [0], current [1]];
								Tile checkScript = checkTile.GetComponent<Tile> ();
								if (checkScript.state == 0) {
									checkScript.setMove ();
								} else if (checkScript.state == 1) {
									checkScript.setKill ();
									break;
								} else if (checkScript.state == 2) {
									break;
	
								}
							}
							current[0] = selectedSpace[0];
							current[1] = selectedSpace[1];
							while (current [0] < 3 && current[1] < 5) {
								++current [0]; ++current [1];
								GameObject checkTile = tiles [current [0], current [1]];
								Tile checkScript = checkTile.GetComponent<Tile> ();
								if (checkScript.state == 0) {
									checkScript.setMove ();
								} else if (checkScript.state == 1) {
									checkScript.setKill ();
									break;
								} else if (checkScript.state == 2) {
									break;
								}
							}
							current[0] = selectedSpace[0];
							current[1] = selectedSpace[1];
							while (current [0] < 3 && current [1] > 0) {
								++current [0]; --current [1];
								GameObject checkTile = tiles [current [0], current [1]];
								Tile checkScript = checkTile.GetComponent<Tile> ();
								if (checkScript.state == 0) {
									checkScript.setMove ();
								} else if (checkScript.state == 1) {
									checkScript.setKill ();
									break;
								} else if (checkScript.state == 2) {
									break;
								}
							}
							current[0] = selectedSpace[0];
							current[1] = selectedSpace[1];
							while (current [0] > 0 && current [1] < 5) {
								--current [0]; ++current [1];
								GameObject checkTile = tiles [current [0], current [1]];
								Tile checkScript = checkTile.GetComponent<Tile> ();
								if (checkScript.state == 0) {
									checkScript.setMove ();
								} else if (checkScript.state == 1) {
									checkScript.setKill ();
									break;
								} else if (checkScript.state == 2) {
									break;
								}
							}
						}
						// END OF QUEENS
							
						///////////
						//KNIGHTS//
						///////////
						if (tileScript.piece == "whiteKnight") {
							if (selectedSpace [0] - 2 >= 0) {
								if (selectedSpace [1] - 1 >= 0) {
									GameObject checkTile = tiles [selectedSpace [0] - 2, selectedSpace [1] - 1];
									Tile checkScript = checkTile.GetComponent<Tile> ();
									if (checkScript.state == 0) {
										checkScript.setMove ();
									} else if (checkScript.state == 2) {
										checkScript.setKill ();
									}
								}
								if (selectedSpace [1] + 1 <= 5) {
									GameObject checkTile = tiles [selectedSpace [0] - 2, selectedSpace [1] + 1];
									Tile checkScript = checkTile.GetComponent<Tile> ();
									if (checkScript.state == 0) {
										checkScript.setMove ();
									} else if (checkScript.state == 2) {
										checkScript.setKill ();
									}
								}
							}
							if (selectedSpace [0] + 2 <= 3) {
								if (selectedSpace [1] - 1 >= 0) {
									GameObject checkTile = tiles [selectedSpace [0] + 2, selectedSpace [1] - 1];
									Tile checkScript = checkTile.GetComponent<Tile> ();
									if (checkScript.state == 0) {
										checkScript.setMove ();
									} else if (checkScript.state == 2) {
										checkScript.setKill ();
									}
								}
								if (selectedSpace [1] + 1 <= 5) {
									GameObject checkTile = tiles [selectedSpace [0] + 2, selectedSpace [1] + 1];
									Tile checkScript = checkTile.GetComponent<Tile> ();
									if (checkScript.state == 0) {
										checkScript.setMove ();
									} else if (checkScript.state == 2) {
										checkScript.setKill ();
									}
								}
							}
							if (selectedSpace [1] - 2 >= 0) {
								if (selectedSpace [0] - 1 >= 0) {
									GameObject checkTile = tiles [selectedSpace [0] - 1, selectedSpace [1] - 2];
									Tile checkScript = checkTile.GetComponent<Tile> ();
									if (checkScript.state == 0) {
										checkScript.setMove ();
									} else if (checkScript.state == 2) {
										checkScript.setKill ();
									}
								}
								if (selectedSpace [0] + 1 <= 3) {
									GameObject checkTile = tiles [selectedSpace [0] + 1, selectedSpace [1] - 2];
									Tile checkScript = checkTile.GetComponent<Tile> ();
									if (checkScript.state == 0) {
										checkScript.setMove ();
									} else if (checkScript.state == 2) {
										checkScript.setKill ();
									}
								}
							}
							if (selectedSpace [1] + 2 <= 5) {
								if (selectedSpace [0] - 1 >= 0) {
									GameObject checkTile = tiles [selectedSpace [0] - 1, selectedSpace [1] + 2];
									Tile checkScript = checkTile.GetComponent<Tile> ();
									if (checkScript.state == 0) {
										checkScript.setMove ();
									} else if (checkScript.state == 2) {
										checkScript.setKill ();
									}
								}
								if (selectedSpace [0] + 1 <= 3) {
									GameObject checkTile = tiles [selectedSpace [0] + 1, selectedSpace [1] + 2];
									Tile checkScript = checkTile.GetComponent<Tile> ();
									if (checkScript.state == 0) {
										checkScript.setMove ();
									} else if (checkScript.state == 2) {
										checkScript.setKill ();
									}
								}
							}
						}
						if (tileScript.piece == "blackKnight") {
							if (selectedSpace [0] - 2 >= 0) {
								if (selectedSpace [1] - 1 >= 0) {
									GameObject checkTile = tiles [selectedSpace [0] - 2, selectedSpace [1] - 1];
									Tile checkScript = checkTile.GetComponent<Tile> ();
									if (checkScript.state == 0) {
										checkScript.setMove ();
									} else if (checkScript.state == 1) {
										checkScript.setKill ();
									}
								}
								if (selectedSpace [1] + 1 <= 5) {
									GameObject checkTile = tiles [selectedSpace [0] - 2, selectedSpace [1] + 1];
									Tile checkScript = checkTile.GetComponent<Tile> ();
									if (checkScript.state == 0) {
										checkScript.setMove ();
									} else if (checkScript.state == 1) {
										checkScript.setKill ();
									}
								}
							}
							if (selectedSpace [0] + 2 <= 3) {
								if (selectedSpace [1] - 1 >= 0) {
									GameObject checkTile = tiles [selectedSpace [0] + 2, selectedSpace [1] - 1];
									Tile checkScript = checkTile.GetComponent<Tile> ();
									if (checkScript.state == 0) {
										checkScript.setMove ();
									} else if (checkScript.state == 1) {
										checkScript.setKill ();
									}
								}
								if (selectedSpace [1] + 1 <= 5) {
									GameObject checkTile = tiles [selectedSpace [0] + 2, selectedSpace [1] + 1];
									Tile checkScript = checkTile.GetComponent<Tile> ();
									if (checkScript.state == 0) {
										checkScript.setMove ();
									} else if (checkScript.state == 1) {
										checkScript.setKill ();
									}
								}
							}
							if (selectedSpace [1] - 2 >= 0) {
								if (selectedSpace [0] - 1 >= 0) {
									GameObject checkTile = tiles [selectedSpace [0] - 1, selectedSpace [1] - 2];
									Tile checkScript = checkTile.GetComponent<Tile> ();
									if (checkScript.state == 0) {
										checkScript.setMove ();
									} else if (checkScript.state == 1) {
										checkScript.setKill ();
									}
								}
								if (selectedSpace [0] + 1 <= 3) {
									GameObject checkTile = tiles [selectedSpace [0] + 1, selectedSpace [1] - 2];
									Tile checkScript = checkTile.GetComponent<Tile> ();
									if (checkScript.state == 0) {
										checkScript.setMove ();
									} else if (checkScript.state == 1) {
										checkScript.setKill ();
									}
								}
							}
							if (selectedSpace [1] + 2 <= 5) {
								if (selectedSpace [0] - 1 >= 0) {
									GameObject checkTile = tiles [selectedSpace [0] - 1, selectedSpace [1] + 2];
									Tile checkScript = checkTile.GetComponent<Tile> ();
									if (checkScript.state == 0) {
										checkScript.setMove ();
									} else if (checkScript.state == 1) {
										checkScript.setKill ();
									}
								}
								if (selectedSpace [0] + 1 <= 3) {
									GameObject checkTile = tiles [selectedSpace [0] + 1, selectedSpace [1] + 2];
									Tile checkScript = checkTile.GetComponent<Tile> ();
									if (checkScript.state == 0) {
										checkScript.setMove ();
									} else if (checkScript.state == 1) {
										checkScript.setKill ();
									}
								}
							}
						}
						// END OF KNIGHTS
	
						/////////
						//PAWNS//
						/////////
						if (tileScript.piece == "whitePawn") {
							//check if adjacent spaces are on the board
							bool left = false;
							bool right = false;
							//bool down = false;
							bool up = false;
							if (selectedSpace [0] > 0) { //not on the left wall
								left = true; //can move left
								Debug.Log ("left");
							}
							if (selectedSpace [0] < 3) { //not on the right wall
								right = true; //can move left
								Debug.Log ("right");
							}
							if (selectedSpace [1] < 5) { //not on the lower wall
								up = true; //can move down
								Debug.Log ("up");
							}
	
							if (up) {
								GameObject checkTile = tiles [selectedSpace [0], selectedSpace [1] + 1];
								Tile checkScript = checkTile.GetComponent<Tile> ();
								if (checkScript.state == 0) {
									checkScript.setMove ();
								}
	
								if (left) {
									checkTile = tiles [selectedSpace [0] - 1, selectedSpace [1] + 1];
									checkScript = checkTile.GetComponent<Tile> ();
									if (checkScript.state == 2) {
										checkScript.setKill ();
									}
								}
								if (right) {
									checkTile = tiles [selectedSpace [0] + 1, selectedSpace [1] + 1];
									checkScript = checkTile.GetComponent<Tile> ();
									if (checkScript.state == 2) {
										checkScript.setKill ();
									}
								}
							}
	
						}
						if (tileScript.piece == "blackPawn") {
							//check if adjacent spaces are on the board
							bool left = false;
							bool right = false;
							bool down = false;
							//bool up = false;
							if (selectedSpace [0] > 0) { //not on the left wall
								left = true; //can move left
								Debug.Log ("left");
							}
							if (selectedSpace [0] < 3) { //not on the right wall
								right = true; //can move left
								Debug.Log ("right");
							}
							if (selectedSpace [1] > 0) { //not on the lower wall
								down = true; //can move down
								Debug.Log ("down");
							}
	
							if (down) {
								GameObject checkTile = tiles [selectedSpace [0], selectedSpace [1] - 1];
								Tile checkScript = checkTile.GetComponent<Tile> ();
								if (checkScript.state == 0) {
									checkScript.setMove ();
								}
	
								if (left) {
									checkTile = tiles [selectedSpace [0] - 1, selectedSpace [1] - 1];
									checkScript = checkTile.GetComponent<Tile> ();
									if (checkScript.state == 1) {
										checkScript.setKill ();
									}
								}
								if (right) {
									checkTile = tiles [selectedSpace [0] + 1, selectedSpace [1] - 1];
									checkScript = checkTile.GetComponent<Tile> ();
									if (checkScript.state == 1) {
										checkScript.setKill ();
									}
								}
							}
	
						}
						// END OF PAWNS
					}

				} else {
					selectedPiece = "empty";
				}
			}

			/*************************************
			 if a piece has already been selected
			*************************************/
			else {
				int[] oldSpace = { selectedSpace [0], selectedSpace [1] };
				selectedSpace [0] = (int)mouseOver.x;
				selectedSpace [1] = (int)mouseOver.y;
				if (selectedSpace [0] != -1 && selectedSpace [1] != -1) {
					GameObject myTile = tiles [selectedSpace [0], selectedSpace [1]];
					Tile tileScript = myTile.GetComponent<Tile> ();
					GameObject oldTile = tiles [oldSpace [0], oldSpace [1]];
					Tile oldScript = oldTile.GetComponent<Tile> ();
					TurnHandler.Player turnPlayer = GetComponentInChildren<TurnHandler> ().GetTurnPlayer ();
					if (tileScript.softSelect) {
						if (oldScript.state == 1 && turnPlayer == TurnHandler.Player.WHITE) {	
							if (tileScript.piece == "blackKing") 
							{ 
								victory = 1;
							}
							tileScript.setPiece (oldScript.piece, "white");
							oldScript.setPiece ("empty", "empty");
							GetComponentInChildren<TurnHandler> ().TurnTaken ();
						} else if (oldScript.state == 2 && turnPlayer == TurnHandler.Player.BLACK) {
							if (tileScript.piece == "whiteKing") 
							{ 
								victory = 2;
							}
							tileScript.setPiece (oldScript.piece, "black");
							oldScript.setPiece ("empty", "empty");
							GetComponentInChildren<TurnHandler> ().TurnTaken ();
						}
					} 
					clearSelections ();
				}
		}
	}
	}

	void clearSelections()
	{
		selectedPiece = "empty";
		for (int i = 0; i < x; ++i) {
			for (int j = 0; j < y; ++j) {
				GameObject myTile = tiles [i, j];
				Tile tileScript = myTile.GetComponent<Tile> ();
				tileScript.selectSpace (false);
			}
		}
	}

	void Update()
	{
		if (victory==0) {
			RevealByTeam (turnScript.playerNum ());
			UpdateMouseOver ();
		}
		else if(victory==1)
		{
			RevealAll ();
		}
		else if(victory==2)
		{
			RevealAll ();
		}
	}

}

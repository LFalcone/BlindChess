
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
		GameObject myTile = tiles [2,0];
		Tile tileScript = myTile.GetComponent<Tile> ();
		tileScript.setPiece ("whiteKing", "white");
		myTile = tiles [1,5];
		tileScript = myTile.GetComponent<Tile> ();
		tileScript.setPiece ("blackKing", "black");

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
				if (selectedSpace [0] != -1 && selectedSpace [1] != -1) 
				{
					GameObject myTile = tiles [selectedSpace [0], selectedSpace [1]];
					Tile tileScript = myTile.GetComponent<Tile> ();
					tileScript.selectSpace (true);
					if (tileScript.piece == "whiteKing") 
					{
						
					}
				}
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

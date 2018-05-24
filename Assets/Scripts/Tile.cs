using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour {

	public int state = 0; 	//0=no piece, 1=white piece, 2=black piece
	public List<int> pos;	//(x,y) bottom left tile = (0,0)
	public string piece;	//will be empty if state=0
	public Material mat;
	public string color;

	public GameObject whiteKing;
	public GameObject whiteQueen;
	public GameObject whiteRook;
	public GameObject whiteBishop;
	public GameObject whiteKnight;
	public GameObject whitePawn;
	public GameObject blackKing;
	public GameObject blackQueen;
	public GameObject blackRook;
	public GameObject blackBishop;
	public GameObject blackKnight;
	public GameObject blackPawn;

	public bool softSelect= false;

	void Awake()
	{
		mat = GetComponent<Renderer>().material;
		piece = "empty";


		whiteKing = Instantiate (whiteKing);
		whiteKing.transform.parent = this.transform;
		whiteKing.transform.position = new Vector3 (pos[0], 0, pos[1]);
		whiteKing.SetActive (false);

		whiteQueen = Instantiate (whiteQueen);
		whiteQueen.transform.parent = this.transform;
		whiteQueen.transform.position = new Vector3 (pos[0], 0, pos[1]);
		whiteQueen.SetActive (false);

		whiteRook = Instantiate (whiteRook);
		whiteRook.transform.parent = this.transform;
		whiteRook.transform.position = new Vector3 (pos[0], 0, pos[1]);
		whiteRook.SetActive (false);

		whiteBishop = Instantiate (whiteBishop);
		whiteBishop.transform.parent = this.transform;
		whiteBishop.transform.position = new Vector3 (pos[0], 0, pos[1]);
		whiteBishop.SetActive (false);

		whiteKnight = Instantiate (whiteKnight);
		whiteKnight.transform.parent = this.transform;
		whiteKnight.transform.position = new Vector3 (pos[0], 0, pos[1]);
		whiteKnight.SetActive (false);

		whitePawn = Instantiate (whitePawn);
		whitePawn.transform.parent = this.transform;
		whitePawn.transform.position = new Vector3 (pos[0], 0, pos[1]);
		whitePawn.SetActive (false);

		blackKing = Instantiate (blackKing);
		blackKing.transform.parent = this.transform;
		blackKing.transform.position = new Vector3 (pos[0], 0, pos[1]);
		blackKing.SetActive (false);

		blackQueen = Instantiate (blackQueen);
		blackQueen.transform.parent = this.transform;
		blackQueen.transform.position = new Vector3 (pos[0], 0, pos[1]);
		blackQueen.SetActive (false);

		blackRook = Instantiate (blackRook);
		blackRook.transform.parent = this.transform;
		blackRook.transform.position = new Vector3 (pos[0], 0, pos[1]);
		blackRook.SetActive (false);

		blackBishop = Instantiate (blackBishop);
		blackBishop.transform.parent = this.transform;
		blackBishop.transform.position = new Vector3 (pos[0], 0, pos[1]);
		blackBishop.SetActive (false);

		blackKnight = Instantiate (blackKnight);
		blackKnight.transform.parent = this.transform;
		blackKnight.transform.position = new Vector3 (pos[0], 0, pos[1]);
		blackKnight.SetActive (false);

		blackPawn = Instantiate (blackPawn);
		blackPawn.transform.parent = this.transform;
		blackPawn.transform.position = new Vector3 (pos[0], 0, pos[1]);
		blackPawn.SetActive (false);
	}

	public void setBlack()
	{
		color = "black";
		mat.color = Color.black;
	}
	public void setWhite()
	{
		color = "white";
		mat.color = Color.white;
	}
	public void selectSpace(bool selected)
	{
		softSelect = false;
		if (selected) {
			mat.color = Color.red;
		} 
		else if (color == "black") 
		{
			mat.color = Color.black;
		}
		else
		{
			mat.color = Color.white;
		}
	}
	public void setMove()
	{
		mat.color = Color.yellow;
		softSelect = true;
	}
	public void setKill()
	{
		mat.color = Color.green;
		softSelect = true;
	}
	public void setPos(int x, int y)
	{
		pos [0] = x;
		pos [1] = y;
	}
	//maybe use later

	public void setPiece(string p, string color)
	{
		piece = p;
		if (color == "black") 
		{
			state = 2;
		} 
		else if (color == "white") 
		{
			state = 1;
		} 
		else 
		{
			state = 0;
		}
	}

	public void removePiece(int x, int y)
	{
		piece = "empty";
		state = 0;
	}
}

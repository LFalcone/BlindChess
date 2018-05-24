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
		whiteKing.transform.localPosition = new Vector3 (0f,4f, 0f);

		whiteQueen = Instantiate (whiteQueen);
		whiteQueen.transform.parent = this.transform;
		whiteQueen.transform.localPosition = new Vector3 (0f,0.5f, 0f);

		whiteRook = Instantiate (whiteRook);
		whiteRook.transform.parent = this.transform;
		whiteRook.transform.localPosition = new Vector3 (0f,0.1f, 0f);

		whiteBishop = Instantiate (whiteBishop);
		whiteBishop.transform.parent = this.transform;
		whiteBishop.transform.localPosition = new Vector3 (0f,0.1f, 0f);

		whiteKnight = Instantiate (whiteKnight);
		whiteKnight.transform.parent = this.transform;
		whiteKnight.transform.localPosition = new Vector3 (0f,0.1f, 0f);

		whitePawn = Instantiate (whitePawn);
		whitePawn.transform.parent = this.transform;
		whitePawn.transform.localPosition = new Vector3 (0f,4f, 0f);

		blackKing = Instantiate (blackKing);
		blackKing.transform.parent = this.transform;
		blackKing.transform.localPosition = new Vector3 (0f,4f, 0f);

		blackQueen = Instantiate (blackQueen);
		blackQueen.transform.parent = this.transform;
		blackQueen.transform.localPosition = new Vector3 (0f,0.5f, 0f);

		blackRook = Instantiate (blackRook);
		blackRook.transform.parent = this.transform;
		blackRook.transform.localPosition = new Vector3 (0f,0.1f, 0f);

		blackBishop = Instantiate (blackBishop);
		blackBishop.transform.parent = this.transform;
		blackBishop.transform.localPosition = new Vector3 (0f,0.1f, 0f);

		blackKnight = Instantiate (blackKnight);
		blackKnight.transform.parent = this.transform;
		blackKnight.transform.localPosition = new Vector3 (0f,0.1f, 0f);

		blackPawn = Instantiate (blackPawn);
		blackPawn.transform.parent = this.transform;
		blackPawn.transform.localPosition = new Vector3 (0f,4f, 0);

		deactivate ();
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

	public void deactivate()
	{
		
		whiteKing.SetActive (false);

		whiteQueen.SetActive (false);

		whiteRook.SetActive (false);

		whiteBishop.SetActive (false);

		whiteKnight.SetActive (false);

		whitePawn.SetActive (false);

		blackKing.SetActive (false);

		blackQueen.SetActive (false);

		blackRook.SetActive (false);

		blackBishop.SetActive (false);

		blackKnight.SetActive (false);

		blackPawn.SetActive (false);
	}

	public void deActivate(GameObject g)
	{
		whiteKing.SetActive (false);

		whiteQueen.SetActive (false);

		whiteRook.SetActive (false);

		whiteBishop.SetActive (false);

		whiteKnight.SetActive (false);

		whitePawn.SetActive (false);

		blackKing.SetActive (false);

		blackQueen.SetActive (false);

		blackRook.SetActive (false);

		blackBishop.SetActive (false);

		blackKnight.SetActive (false);

		blackPawn.SetActive (false);

		g.SetActive (true);
	}

	public void setPiece(string p, string color)
	{
		piece = p;
		deactivate ();
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
		if (piece == "whiteKing") {
			deActivate (whiteKing);
		} else if (piece == "whiteQueen") {
			deActivate (whiteQueen);
		} else if (piece == "whiteRook") {
			deActivate (whiteRook);
		} else if (piece == "whiteBishop") {
			deActivate (whiteBishop);
		} else if (piece == "whiteKnight") {
			deActivate (whiteKnight);
		} else if (piece == "whitePawn") {
			deActivate (whitePawn);
		} else if (piece == "blackKing") {
			deActivate (blackKing);
		} else if (piece == "blackQueen") {
			deActivate (blackQueen);
		} else if (piece == "blackRook") {
			deActivate (blackRook);
		} else if (piece == "blackBishop") {
			deActivate (blackBishop);
		} else if (piece == "blackKnight") {
			deActivate (blackKnight);
		} else if (piece == "blackPawn") {
			deActivate (blackPawn);
		} 
	}

	public void removePiece(int x, int y)
	{
		piece = "empty";
		state = 0;
	}
}

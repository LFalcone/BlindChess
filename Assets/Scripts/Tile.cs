using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour {

	public int state = 0; 	//0=no piece, 1=white piece, 2=black piece
	public List<int> pos;	//(x,y) bottom left tile = (0,0)
	public string piece;	//will be empty if state=0
	public Material mat;
	public string color;

	public bool softSelect= false;

	void Awake()
	{
		mat = GetComponent<Renderer>().material;
		piece = "empty";
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
	/*public void setPiece(GameObject p, string color)
	{
		King kingScript = p.GetComponent<King> ();
		if (kingScript != null) {
			Debug.Log ("it worked");

			Instantiate (p);
			if (color == "black") {
				kingScript.setBlack ();
				piece = "blackKing";
				state = 2;
			} 
			else 
			{
				kingScript.setWhite ();
				piece = "whiteKing";
				state = 1;
			}
			p.transform.position = this.transform.position;
			p.transform.Translate (0.0f, 0.4f, 0.0f);
		} else {
			Debug.Log ("unlucky");
		}
	}*/

	public void removePiece(int x, int y)
	{
		piece = "empty";
		state = 0;
	}
}

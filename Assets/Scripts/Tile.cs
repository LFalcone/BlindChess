using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour {

	public int state = 0; 	//0=no piece, 1=white piece, 2=black piece
	public List<int> pos;	//(x,y) bottom left tile = (0,0)
	public GameObject piece;	//will be empty if state=0
	public Material mat;
	public string color;

	void Awake()
	{
		mat = GetComponent<Renderer>().material;
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
	public void setPos(int x, int y)
	{
		pos [0] = x;
		pos [1] = y;
	}
	public void setPiece(GameObject p, string color)
	{
		King kingScript = p.GetComponent<King> ();
		if (kingScript != null) 
		{
			piece = p;
			Instantiate (p);
			if (color == "black")
				kingScript.setBlack ();
			else
				kingScript.setWhite ();
		}

	}
}

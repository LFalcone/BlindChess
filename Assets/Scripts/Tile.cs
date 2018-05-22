using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour {

	public int state = 0; 	//0=no piece, 1=white piece, 2=black piece
	public List<int> pos;	//(x,y) bottom left tile = (0,0)
	public GameObject piece;	//will be empty if state=0
	public Material mat;

	void Awake()
	{
		mat = GetComponent<Renderer>().material;
	}

	public void setBlack()
	{
		mat.color = Color.black;
	}
	public void setWhite()
	{
		mat.color = Color.white;
	}
}

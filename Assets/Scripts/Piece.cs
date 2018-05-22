using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Piece : MonoBehaviour {

	public string color;
	public GameObject board;
	public bool visible;
	public GameObject tile;
	public List<int> space;
	public List<List<int>> movement;
	public Material mat;

	void Awake()
	{
		mat = GetComponent<Renderer>().material;
	}
	public void setBlack()
	{
		mat.color = Color.black;
		color = "black";
	}
	public void setWhite()
	{
		mat.color = Color.white;
		color = "white";
	}
	void move()
	{
		
	}
}

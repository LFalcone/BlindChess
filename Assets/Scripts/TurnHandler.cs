using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turn : MonoBehaviour {
	
	public enum Player { WHITE, BLACK }

	public bool startingPlayer = Player.WHITE;

	private bool turnPlayer;
	private bool waitForNextPlayer;
	private bool gameOver;

	// Use this for initialization
	void Start () {
		gameOver = false;
		turnPlayer = startingPlayer;
		waitForNextPlayer = true;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void NextTurn () {
		waitForNextPlayer = true;
		turnPlayer = !turnPlayer;
	}

	void RevealTurnPlayer () {
		waitForNextPlayer = false;
	}

	void RevealBoard () {
		gameOver = true;
	}
}

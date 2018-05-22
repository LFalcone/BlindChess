using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnHandler : MonoBehaviour {
	
	public enum Player { WHITE, BLACK }
	public bool whiteTurn = true;

	public Player startingPlayer = Player.WHITE;

	private Player turnPlayer;
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
		if (Input.GetKeyDown (KeyCode.Return) && !waitForNextPlayer) {
			NextTurn ();
		}
		if (Input.GetKeyDown (KeyCode.Space) && waitForNextPlayer) {
			RevealTurnPlayer ();
		}
	}

	void NextTurn () {
		waitForNextPlayer = true;
		if(turnPlayer == Player.WHITE) turnPlayer = Player.BLACK;
		else if(turnPlayer == Player.BLACK) turnPlayer = Player.WHITE;
	}

	void RevealTurnPlayer () {
		waitForNextPlayer = false;
	}

	void RevealBoard () {
		gameOver = true;
	}
}

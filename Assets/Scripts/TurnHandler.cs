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
		SetCameraPosition ();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.Return) && !waitForNextPlayer) { // Eventually this should be changed from a GetKeyDown to a button press or just after the turn player makes their move
			NextTurn ();
		}
		if (Input.GetKeyDown (KeyCode.Space) && waitForNextPlayer) { // Eventually this should be changed from a GetKeyDown to a button press. It actually could stay as a Spacebar press as long
			RevealTurnPlayer ();									 // as there is some way to notify the player that they need to press Spacebar
		}
	}

	// NextTurn ends the current player's turn and should cause the entire board to be empty so that neither player reveals their sides of the board
	void NextTurn () {
		waitForNextPlayer = true;
		if(turnPlayer == Player.WHITE) turnPlayer = Player.BLACK;
		else if(turnPlayer == Player.BLACK) turnPlayer = Player.WHITE;
		SetCameraPosition ();
	}

	// RevealTurnPlayer reveals the turn player's side of the board after the other player is already looking away
	void RevealTurnPlayer () {
		waitForNextPlayer = false;
	}

	// RevealBoard reveals the entire board, which will likely only have use when the game is complete
	void RevealBoard () {
		gameOver = true;
	}

	// WHITE Camera:
	// 				Position: 1.5, 5, -1.5
	//				Rotation: 0.4472136, 0, 0, 0.8944272
	// BLACK Camera:
	//				Position: 1.5, 5, 6.5
	//				Rotation: 0, 0.8944263, -0.4472136, 0.001235376
	private Vector3 whitePosition = new Vector3(1.5f, 5, -1.5f);
	private Quaternion whiteRotation = new Quaternion(0.4472136f, 0, 0, 0.8944272f);
	private Vector3 blackPosition = new Vector3(1.5f, 5, 6.5f);
	private Quaternion blackRotation = new Quaternion(0, 0.8944263f, -0.4472136f, 0.001235376f);

	void SetCameraPosition () {
		if (turnPlayer == Player.WHITE) {
			transform.localPosition = whitePosition;
			transform.localRotation = whiteRotation;
		} else if (turnPlayer == Player.BLACK) {
			transform.localPosition = blackPosition;
			transform.localRotation = blackRotation;
		}
	}

	public Player GetTurnPlayer () {
		return turnPlayer;
	}

	public bool isWaiting () {
		return waitForNextPlayer;
	}
}

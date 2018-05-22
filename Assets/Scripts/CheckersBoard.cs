﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckersBoard : MonoBehaviour
{
    public Pieces[,] pieces = new Pieces[8, 8];
    public GameObject whitePiecePrefab;
    public GameObject blackPiecePrefab;

    private Vector3 boardOffset = new Vector3(-4.0f, 0.2f, -4.0f);
    private Vector3 pieceOffset = new Vector3(0.5f, 0, 0.5f);

    private Pieces selectedPiece;


    private Vector2 mouseOver;
    private Vector2 startDrag;
    private Vector2 endDrag;


    private void Start()
    {
        GenerateBoard();
    }

    private void Update()
    {
        UpdateMouseOver();

        //If my turn
        {
            int x = (int)mouseOver.x;
            int y = (int)mouseOver.y;

           

            if (Input.GetMouseButton(0))
                SelectPiece(x, y);

            if (Input.GetMouseButtonUp(0))
            {
                TryMove((int)startDrag.x, (int)startDrag.y, x, y);
            }
        }
    }

    private void UpdateMouseOver()
    {
        // If its my turn 
        if (!Camera.main)
        {
            Debug.Log("Unable to find Main Camera");
            return;
        }
        RaycastHit hit;
        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 25.0f, LayerMask.GetMask("Board")))
        {
            mouseOver.x = (int)(hit.point.x - boardOffset.x);
            mouseOver.y = (int)(hit.point.z - boardOffset.z);
        }
        else
        {
            mouseOver.x = -1;
            mouseOver.y = -1;
        }
    }

    


    private void SelectPiece(int x, int y)
    {
        // If out of bounds
        if (x < 0 || x >= pieces.Length || y < 0 || y >= pieces.Length)
            return;
        Pieces p = pieces[x, y];
        if (p != null)
        {
            selectedPiece = p;
            startDrag = mouseOver;
            Debug.Log(selectedPiece.name);
        }
    }

    private void TryMove(int x1, int y1, int x2, int y2)
    {
        // Multiplayer support
        startDrag = new Vector2(x1, y1);
        endDrag = new Vector2(x2, y2);
        selectedPiece = pieces[x1, y1];

        MovePiece(selectedPiece, x2, y2);
    }

    private void GenerateBoard()
    {
        //Generate White Team
        for (int y = 0; y < 3; y++)
        {
            bool oddRow = (y % 2 == 0);
            for (int x = 0; x < 8; x += 2)
            {
                //Generate our Piece
                //checks if odd row, if it isn't just do x, if not do x+1
                GeneratePiece((oddRow)?x:x+1, y);
            }
        }

        //Generate Black Team
        for (int y = 7; y > 4; y--)
        {
            bool oddRow = (y % 2 == 0);
            for (int x = 0; x < 8; x += 2)
            {
                //Generate our Piece
                //checks if odd row, if it isn't just do x, if not do x+1
                GeneratePiece((oddRow) ? x : x + 1, y);
            }
        }
    }

    private void GeneratePiece(int x, int y)
    {
        bool isPieceWhite = (y > 3) ? false : true;
        GameObject go = Instantiate((isPieceWhite)?whitePiecePrefab:blackPiecePrefab) as GameObject;
        go.transform.SetParent(transform);
        Pieces p = go.GetComponent<Pieces>();
        pieces[x, y] = p;
        MovePiece(p, x, y);
    }

    private void MovePiece(Pieces p, int x, int y)
    {
        // Moves piece when generated to right spot
        p.transform.position = (Vector3.right * x) + (Vector3.forward * y) + boardOffset + pieceOffset;

    }

}


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnightPieces
{
    public static List<Vector3> listOfPossibleMoves = new List<Vector3>
    {
        new Vector3(-1 , 0 , 2),
        new Vector3(1 , 0 , 2),
        new Vector3(-1 , 0 , -2),
        new Vector3(1 , 0 , -2),
        new Vector3(-2 , 0 , -1),
        new Vector3(-2 , 0 , 1),
        new Vector3(2 , 0 , -1),
        new Vector3(2 , 0 , 1)
    };

    private Vector3 postion;

    public Vector3 Postion { get => postion; set => postion = value; }

    public KnightPieces(Vector3 position)
    {
        this.Postion = position;
    }

    
}

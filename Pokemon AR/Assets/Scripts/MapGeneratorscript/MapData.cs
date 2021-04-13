using System.Collections.Generic;
using UnityEngine;


public partial class MapVisualiser
{
    public struct MapData
    {
        public bool[] obstacleArray;
        public List<KnightPieces> knightplaceslist;
        public Vector3 startPosition;
        public Vector3 exitPosition;
    }
}

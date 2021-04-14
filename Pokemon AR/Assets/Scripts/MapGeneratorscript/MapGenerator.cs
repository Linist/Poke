using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{

    public GridVisualizer gridVisualizer;
    public MapVisualiser mapVisualiser;

    public Direction startEdge, exitEdge;
    public bool randomPlacement;

    [Range(1,10)]
    public int numberOfPieces;

    private Vector3 startPosition, exitPosition;

    //[Range(3, 20)]
    //public int width, length = 11;
    private MapGrid grid;

    public void GenerateMap(Vector3 midpoint, int width, int length)
    {
        grid = new MapGrid(width, length);
        grid.CheckCoordiantes();
        gridVisualizer.VisualizeGrid(midpoint, width, length); // midpoint, width, length
        MapHelper.RandomlyChooseAndSetStartAndExit(grid, ref startPosition, ref exitPosition, randomPlacement, startEdge, exitEdge);
        Debug.Log("start" + startPosition);
        Debug.Log("Exit" + exitPosition);
        CandidateMap map = new CandidateMap(grid, numberOfPieces);
        map.CreateMap(startPosition, exitPosition);
        mapVisualiser.VisualizeMap(grid, map.ReturnMapData(), false);
        
    }


}

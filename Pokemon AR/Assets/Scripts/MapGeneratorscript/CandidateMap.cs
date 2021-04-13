using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;


public class CandidateMap
{
    private MapGrid grid;
    private int numberOfPieces = 0;
    private bool[] obstaclesArray = null;
    private Vector3 startPoint, exitPoint;
    private List<KnightPieces> knightPiecesList;

    public MapGrid Grid { get => grid; }
    public bool[] ObstcalesArray { get => obstaclesArray; }


    public CandidateMap(MapGrid grid, int numberOfPieces)
    {
        this.numberOfPieces = numberOfPieces;
        
        this.grid = grid;

    }

    public void CreateMap(Vector3 startPosition, Vector3 exitPosition, bool autoRepair = false)
    {
        this.startPoint = startPosition;
        this.exitPoint = exitPosition;
        obstaclesArray = new bool[grid.Width * grid.Length];
        this.knightPiecesList = new List<KnightPieces>();
        RandomlyPlaceKnightPieces(this.numberOfPieces);
        PlaceObstacles();
    }
    private bool CheckItPositionCanBeObstacle(Vector3 position)
    {
        if (position == startPoint || position == exitPoint)
        {
            return false;
        }
        int index = grid.CalculateIndexFromCoordinates(position.x, position.z);

        return obstaclesArray[index] == false;
    }

    private void RandomlyPlaceKnightPieces(int numberOfPieces)
    {
        var count = numberOfPieces;
        var knightPlacementTryLimit = 100;
        while (count > 0 && knightPlacementTryLimit > 0)
        {
            var randomIndex = Random.Range(0, obstaclesArray.Length);
            if (obstaclesArray[randomIndex] == false)
            {
                var coordinates = grid.CalculateCoordinatesFromIndex(randomIndex);
                if (coordinates == startPoint || coordinates == exitPoint)
                {
                    continue;
                }
                obstaclesArray[randomIndex] = true;
                knightPiecesList.Add(new KnightPieces(coordinates));
                count--;
            }
            knightPlacementTryLimit--;
        }

        

    }

    private void PlaceObstaclesForThisKnight(KnightPieces knight)
    {
        foreach( var position in KnightPieces.listOfPossibleMoves)
        {
            var newPosition = knight.Postion + position;
            if(grid.isCellValid(newPosition.x, newPosition.z) && CheckItPositionCanBeObstacle(newPosition))
            {
                obstaclesArray[grid.CalculateIndexFromCoordinates(newPosition.x, newPosition.z)] = true;
            }
        }
    }

    private void PlaceObstacles()
    {
        foreach(var knight in knightPiecesList)
        {
            PlaceObstaclesForThisKnight(knight);
        }
        
    }

    public MapVisualiser.MapData ReturnMapData()
    {
        return new MapVisualiser.MapData
        {
            obstacleArray = this.obstaclesArray,
            knightplaceslist = knightPiecesList,
            startPosition = startPoint,
            exitPosition = exitPoint
        };
    } 
   
}

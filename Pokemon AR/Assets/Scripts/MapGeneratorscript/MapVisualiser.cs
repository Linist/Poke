using System;
using System.Collections;
using UnityEngine;
using System.Collections.Generic;

public partial class MapVisualiser : MonoBehaviour
{
    private Transform parent;
    public Color startColor, exitColor;
    public GameObject Grass;

    Dictionary<Vector3, GameObject> dictionaryOfObstacles = new Dictionary<Vector3, GameObject>();

    private void Awake()
    {
        parent = this.transform;
        
    }

    public void VisualizeMap(MapGrid grid, MapData data, bool visualizePrefabs)
    {
        if (visualizePrefabs)
        {

        }
        else
        {
            VisualizeUsingPrimitives(grid, data);
        }
    }

    private void VisualizeUsingPrimitives(MapGrid grid, MapData data)
    {
        PlaceStartAndExitPosint(data);
        for (int i = 0; i < data.obstacleArray.Length; i++)
        {
            if (data.obstacleArray[i])
            {
                var positionOnGrid = grid.CalculateCoordinatesFromIndex(i);
                if(positionOnGrid == data.startPosition || positionOnGrid == data.exitPosition)
                {
                    continue;
                }
                grid.SetCell(positionOnGrid.x, positionOnGrid.z, CellObjectType.Obstacle);
                if(PlaceKnightObstacle(data, positionOnGrid))
                {
                    continue;
                }
                if(dictionaryOfObstacles.ContainsKey(positionOnGrid) == false)
                {
                    CreateIndicator(positionOnGrid, Color.white, Grass);
                }
            }
        }
    }

    private bool PlaceKnightObstacle(MapData data, Vector3 positionOnGrid)
    {
        foreach(var knight in data.knightplaceslist)
        {
            if(knight.Postion== positionOnGrid)
            {
                CreateIndicator(positionOnGrid, Color.red, Grass);
                return true;
            }
        }
        return false;
    }

    private void PlaceStartAndExitPosint(MapData data)
    {
       // CreateIndicator(data.startPosition, startColor, PrimitiveType.Sphere);
        //CreateIndicator(data.exitPosition, exitColor, PrimitiveType.Sphere);
    }

    private void CreateIndicator(Vector3 position, Color color, GameObject sphere)
    {
        var element = Instantiate(sphere);
        dictionaryOfObstacles.Add(position, element);
        element.transform.position = position+ new Vector3( 0.5f,0f,0f);
        element.transform.parent = parent;
        var renderer = element.GetComponent<Renderer>();
        renderer.material.SetColor("_Color", color);
    }

    public void ClearMap()
    {
        foreach (var obstacle in dictionaryOfObstacles.Values)
        {
            Destroy(obstacle);
        }
        dictionaryOfObstacles.Clear();
    }
}

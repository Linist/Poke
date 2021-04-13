﻿using System;
using System.Collections;
using UnityEngine;
using System.Collections.Generic;

public partial class MapVisualiser : MonoBehaviour
{
    private Transform parent;
    public Color startColor, exitColor;

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
                CreateIndicator(positionOnGrid, Color.white, PrimitiveType.Cube);
            }
        }
    }

    private bool PlaceKnightObstacle(MapData data, Vector3 positionOnGrid)
    {
        foreach(var knight in data.knightplaceslist)
        {
            if(knight.Postion== positionOnGrid)
            {
                CreateIndicator(positionOnGrid, Color.red, PrimitiveType.Cube);
                return true;
            }
        }
        return false;
    }

    private void PlaceStartAndExitPosint(MapData data)
    {
        CreateIndicator(data.startPosition, startColor, PrimitiveType.Sphere);
        CreateIndicator(data.exitPosition, exitColor, PrimitiveType.Sphere);
    }

    private void CreateIndicator(Vector3 position, Color color, PrimitiveType sphere)
    {
        var element = GameObject.CreatePrimitive(sphere);
        element.transform.position = position+ new Vector3( .5f,.5f,.5f);
        element.transform.parent = parent;
        var renderer = element.GetComponent<Renderer>();
        renderer.material.SetColor("_Color", color);
    }
}

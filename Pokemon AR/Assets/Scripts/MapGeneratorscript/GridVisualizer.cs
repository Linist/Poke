using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridVisualizer : MonoBehaviour
{
    public GameObject groundPrefab;
    public Transform groundPlane;

    public void VisualizeGrid(int width, int length)
    {
        //Vector3 position = midpoint;
        Vector3 position = new Vector3(width/2f, 0, length/2f);
        Quaternion rotation = Quaternion.Euler(90, 0, 0);
        
        var board = Instantiate(groundPrefab, position, rotation);
        board.transform.SetParent(groundPlane);
        board.transform.localScale = new Vector3(width, length, 1);
        
        /*
        groundPrefab.transform.position = position;
        groundPrefab.transform.rotation = rotation;
        groundPrefab.transform.localScale = new Vector3(width, length, 1);
        */
    }
}

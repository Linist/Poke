using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridVisualizer : MonoBehaviour
{
    public GameObject groundPrefab;

    public void VisualizeGrid(Vector3 midpoint, int width, int length)
    {
        Vector3 position = midpoint;
        Quaternion rotation = Quaternion.Euler(90, 0, 0);
        var board = Instantiate(groundPrefab, position, rotation);
        board.transform.localScale = new Vector3(width, length, 1);

    }
}

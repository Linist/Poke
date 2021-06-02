using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class PlaneGeneration : MonoBehaviour
{
    public List<Transform> trackedPoints;
    public Transform[] points;
    float posY;
    public Vector3 midpoint;
    public GameObject plane;

    public float highestX, lowestX, highestZ, lowestZ;

    public float distanceX, distanceZ;
    public int width, height;

    public Transform playerSpawnpoint, enemySpawnpoint;

    public GameObject mapGenerator;
    public GameObject mapVisualiser;

    public Button calibrationButton;
    public Button removeButton;

    public void FindBounds()
    {
        removeButton.gameObject.SetActive(false);
        /*
        // Using List to calculate works bad
        midpoint = new Vector3 (0f, 0f, 0f);

        foreach (var pos in trackedPoints)
        {
            posY += pos.position.y;

            if (highestX != 0 && lowestX != 0 && highestZ != 0 && lowestZ != 0)
            {
                if (pos.position.x > highestX)
                    highestX = pos.position.x;
                else if (pos.position.x < lowestX)
                    lowestX = pos.position.x;

                if (pos.position.z > highestZ)
                    highestZ = pos.position.z;
                else if (pos.position.z < lowestZ)
                    lowestZ = pos.position.z;

            }
            else
            {
                highestX = pos.position.x;
                lowestX = pos.position.x;
                highestZ = pos.position.z;
                lowestZ = pos.position.z;
            }
            
        }
        midpoint.x = (highestX + lowestX) / 2;
        midpoint.y = posY / 4;
        midpoint.z = (highestZ + lowestZ) / 2;
        plane.transform.position = midpoint;
        */

        // Using Array to calculate works better
        for (int i = 0; i < points.Length; i++)
        {
            if (highestX != 0 && lowestX != 0 && highestZ != 0 && lowestZ != 0)
            {
                if (points[i].position.x > highestX)
                    highestX = points[i].position.x;
                else if (points[i].position.x < lowestX)
                    lowestX = points[i].position.x;

                if (points[i].position.z > highestZ)
                    highestZ = points[i].position.z;
                else if (points[i].position.z < lowestZ)
                    lowestZ = points[i].position.z;

            }
            else
            {
                highestX = points[i].position.x;
                lowestX = points[i].position.x;
                highestZ = points[i].position.z;
                lowestZ = points[i].position.z;
            }

        }
        midpoint.x = ((highestX + lowestX) / 2);
        midpoint.y = (points[0].position.y + points[1].position.y + points[2].position.y + points[3].position.y) / 4;
        midpoint.z = ((highestZ + lowestZ) / 2);
        plane.transform.position = midpoint;
        
        distanceX = highestX - lowestX;
        distanceZ = highestZ - lowestZ;

        if (distanceX != 0 || distanceZ != 0)
        {
            newScale(plane, distanceX, distanceZ);
        }
        plane.SetActive(true);

        highestX = 0;
        lowestX = 0;
        highestZ = 0;
        lowestZ = 0;
        GameObject.FindGameObjectWithTag("MainCamera").GetComponent<PlaneSizeGet>().StartScript();

        width = Mathf.CeilToInt(distanceX);
        height = Mathf.CeilToInt(distanceZ);
        /*
        mapGenerator.GetComponent<MapGenerator>().GenerateMap(midpoint, width, height);
        mapVisualiser.transform.position = new Vector3(midpoint.x - (width / 2), midpoint.y, midpoint.z - (height / 2));
        */
    }

    public void newScale(GameObject resizeObject, float newSizeX, float newSizeZ)
    {
        float sizeX = resizeObject.GetComponent<Renderer>().bounds.size.x;
        float sizeZ = resizeObject.GetComponent<Renderer>().bounds.size.z;

        Vector3 rescale = resizeObject.transform.localScale;

        rescale.x = newSizeX * rescale.x / sizeX;
        rescale.z = newSizeZ * rescale.z / sizeZ;

        resizeObject.transform.localScale = rescale;
    }

    public Vector3 GetPlayerSpawnpoint()
    {
        playerSpawnpoint.position = new Vector3(width / 2, height * .2f);
        return playerSpawnpoint.position;
    }
    public Vector3 GetEnemySpawnpoint()
    {
        enemySpawnpoint.position = new Vector3(width / 2, height * .8f);
        return enemySpawnpoint.position;
    }

    public void CubeDetected(Transform trackedPosition)
    {
        if (!trackedPoints.Contains(trackedPosition))
            trackedPoints.Add(trackedPosition);
        else return;

        if (trackedPoints.Count >= 4)
        {
            //points = trackedPoints.ToArray();
            removeButton.gameObject.SetActive(true);
            // Calls FindBounds(); in the unity editor UI-click-function.
            //removeButton.gameObject.SetActive(true);
        }

    }
}

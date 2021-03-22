using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlaneGenerationCalibration : MonoBehaviour
{
    public Transform[] points;
    //public Transform point1, point2, point3, point4;
    public Vector3 midpoint;
    public GameObject plane;

    Vector3 startScale;

    public float highestX, lowestX, highestZ, lowestZ;

    //public float[] distancesX, distancesZ;
    public float distanceX, distanceZ;

    private int cubesTracked = 0;
    private bool tracked = false;

    public Button calibrationButton;

    // Start is called before the first frame update
    void Start()
    {
        startScale = new Vector3(1.0f, 1.0f, 1.0f);
        plane.transform.localScale = startScale;
    }

    // Update is called once per frame
    void Update()
    {
        if (tracked == true)
        {
            calibrationButton.gameObject.SetActive(true);
            // Calls FindBounds(); in the unity editor UI-click-function.
        }
    }

    public void FindBounds()
    {
        plane.SetActive(true);

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


        highestX = 0;
        lowestX = 0;
        highestZ = 0;
        lowestZ = 0;
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

    public void CubeDetected()
    {
        cubesTracked++;
        if (cubesTracked >= 4)
        {
            cubesTracked = 4;
            tracked = true;
        }
        Debug.Log("cubes tracked = " + cubesTracked);
    }
    public void CubeLost()
    {
        cubesTracked--;
        if (cubesTracked <= 0)
            cubesTracked = 0;
    }
}

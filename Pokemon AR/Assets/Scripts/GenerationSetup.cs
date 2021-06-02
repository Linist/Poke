using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerationSetup : MonoBehaviour
{
    public float planeSizeX = 0;
    public float planeSizeZ = 0;
    public int scale = 10;

    public GameObject plane;

    public Vector3 midpoint;
    int width, height;

    public Transform playerSpawnpoint, enemySpawnpoint;

    public GameObject mapGenerator;
    public GameObject mapVisualiser;

    // Start is called before the first frame update
    public void Setup()
    {
        //multiplies the scale of the plane (localscale) with 10, so we have the units in unity units.
        planeSizeX = plane.transform.localScale.x * scale;
        planeSizeZ = plane.transform.localScale.z * scale;

        midpoint = new Vector3(planeSizeX / 2, plane.transform.position.y, planeSizeZ / 2);

        // Makes sure that the planes size is in integers
        width = Mathf.CeilToInt(planeSizeX);
        height = Mathf.CeilToInt(planeSizeZ);
        
        // Calls the map generator script
        /*
        mapGenerator.GetComponent<MapGenerator>().GenerateMap(midpoint, width, height);
        mapVisualiser.transform.position = new Vector3(midpoint.x - (width / 2), midpoint.y, midpoint.z - (height / 2));
        */
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
}

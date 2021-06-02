using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartGeneration : MonoBehaviour
{
    public Transform refPlane;
    public Transform parent;

    public GameObject mapGenerator;
    public GameObject mapVisualiser;

    public Transform playerSpawnpoint, enemySpawnpoint;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(GenerateMap());
    }

    // Update is called once per frame
    public IEnumerator GenerateMap()
    {
        

        yield return new WaitForSeconds(2);
        mapGenerator.GetComponent<MapGenerator>().GenerateMap();
        this.transform.localScale = new Vector3(.1f, .1f, .1f);
        //this.transform.position = refPlane.position;
        this.transform.position = new Vector3(refPlane.position.x - .25f, refPlane.position.y, refPlane.position.z - .25f);
        //mapVisualiser.transform.position = refPlane.position;
        mapVisualiser.transform.position = new Vector3(this.transform.position.x - (refPlane.localScale.x / 2), this.transform.position.y, this.transform.position.z - (refPlane.localScale.z / 2));
        this.transform.SetParent(parent);


    }
    public Vector3 GetPlayerSpawnpoint()
    {
        playerSpawnpoint.position = new Vector3(refPlane.localScale.x / 2, refPlane.localScale.z * .2f);
        return playerSpawnpoint.position;
    }
    public Vector3 GetEnemySpawnpoint()
    {
        enemySpawnpoint.position = new Vector3(refPlane.localScale.x / 2, refPlane.localScale.z * .8f);
        return enemySpawnpoint.position;
    }
}

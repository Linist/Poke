using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneSizeGet : MonoBehaviour
{
    //this should be implemented in PlaneGenerationCalibration script, but isn't yet, just to make sure it runs, when it gets implemented (probably with a coroutine, delete this script)
    public float planeSizeX = 0;
    public float planeSizeZ = 0;
    public GameObject plane;


    // Start is called before the first frame update
    void Start()
    {
        //starts a coroutine, this should be done in PlanegenerationCalibrationscript when the plane size is set  (doesn't need a coroutine this is to make a wait time).
        StartCoroutine(planeSet());
    }

    public IEnumerator planeSet()
    {
        Debug.Log("Running planeset IEnumerator");
        yield return new WaitForSeconds(5);
        getPlaneSize();
    }

    public void getPlaneSize()
    {
        Debug.Log("running GetPlaneSize");
        planeSizeX = plane.transform.localScale.x * 10;
        planeSizeZ = plane.transform.localScale.z * 10;
    }
}

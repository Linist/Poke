using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneSizeGet : MonoBehaviour
{
    public float planeSizeX = 0;
    public float planeSizeZ = 0;
    public int scale = 10;
    public GameObject plane;
    
    // Start is called before the first frame update
    public void StartScript()
    {
            //starts a coroutine, this should be done in PlanegenerationCalibrationscript when the plane size is set  (doesn't need a coroutine this is to make a wait time).
            StartCoroutine(planeSet());   
    }

    public IEnumerator planeSet()
    {
        Debug.Log("Running planeSet IEnumerator");
        yield return new WaitForSeconds(1);
        StartCoroutine(getPlaneSize());
    }

    public IEnumerator getPlaneSize()
    {
        Debug.Log("running GetPlaneSize");
        //multiplies the scale of the plane (localscale) with 10, so we have the units in unity units.
        planeSizeX = plane.transform.localScale.x * scale;
        planeSizeZ = plane.transform.localScale.z * scale;

        yield return new WaitForEndOfFrame();
        //GameObject.FindGameObjectWithTag("MainCamera").GetComponent<PlaneSizeGet>().enabled = false;
    }
}

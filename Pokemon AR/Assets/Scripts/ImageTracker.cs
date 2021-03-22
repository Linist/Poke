using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImageTracker : MonoBehaviour
{
    public GameObject ARCamera;
    public bool found = false;

    public void Found()
    {
        Debug.Log("Found");
        found = true;
        //ARCamera.GetComponent<DistanseTest>().CubeDetected();
    }

    public void Lost()
    {
        Debug.Log("Lost");
        found = false;
        //ARCamera.GetComponent<DistanseTest>().CubeLost();
    }
}

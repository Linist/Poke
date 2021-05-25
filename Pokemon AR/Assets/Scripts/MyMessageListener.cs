using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyMessageListener : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Invoked on recieved data from the serial device
    void onMessageArrived(string msg)
    {
        Debug.Log("Arrived: " + msg);
    }

    /* Invoked when a connect/disconnect even occurs. The parameter "success"
      will be "true" upon connection, and "false" upon Disconecction or
      failure to connect
      */ 
        
    void OnConnectionEvent(bool success)
    {
        Debug.Log(success ? "Device Connected" : "Device Disconnected");
    }
}

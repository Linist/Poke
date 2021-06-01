using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackPlayer : MonoBehaviour
{
    public GameObject player;
    public Vector3 pos;

    public void PlayerTracking()
    {
        player = this.gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        pos = player.transform.position;
    }
}

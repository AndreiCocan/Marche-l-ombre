using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointBehavior : MonoBehaviour
{

    // Update is called once per frame
    void Update()
    {
        transform.RotateAround(transform.position, Vector3.up, 20 * Time.deltaTime);
    }
}

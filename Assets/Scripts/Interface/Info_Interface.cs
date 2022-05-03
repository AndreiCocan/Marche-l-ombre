using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

public class Info_Interface : MonoBehaviour
{
    /* TODO */

    // Variables for the vision of the camera
    [SerializeField] GameObject camera;
    private Vector3 direction;
    private Vector3 coordinate;
    private Vector3 difference;
    private float vu;
    private double distance;
    
    //Variables for the text zones on the interface
    [SerializeField] TextMeshProUGUI namePoint;
    [SerializeField] TextMeshProUGUI geographicInfos;
    [SerializeField] TextMeshProUGUI detailledInfos;

    // Detect if the user is looking at the point of interest
    private bool IsFacing()
    {
        coordinate = /* Take the exact coordinate of the point of interest (API script) */
        direction = (coordinate - camera.transform.position).normalized;
        vu = Vector3.Dot(direction, transform.forward);

        if (vu <= 1 && vu >= 0.7)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private bool IsNear()
    {
        coordinate = /* Take the exact coordinate of the point of interest (API script) */
        difference = new Vector3(camera.transform.position.x - coordinate.x, camera.transform.position.y - coordinate.y, camera.transform.position.z - coordinate.z);
        distance = Math.Sqrt(Math.Pow(difference.x, 2f) + Math.Pow(difference.y, 2f) + Math.Pow(difference.z, 2f));
        if (distance < 40)
        {
            return true;
        } else
        {
            return false;
        }
    }

    // Change the informations on the canva depending on the point of interest location
    void UpdateInfos()
    {
        /* Select the right infos on the json file */
    }

    // Update is called once per frame
    void Update()
    {
        if (IsFacing() && IsNear())
        {
            UpdateInfos();
        }
    }
}

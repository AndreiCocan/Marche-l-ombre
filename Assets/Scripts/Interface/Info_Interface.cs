using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

public class Info_Interface : MonoBehaviour
{
    // Data
    [SerializeField]
    private GeoGetter reference;

    // Variables for the vision of the camera
    [SerializeField]
    private GameObject camera;
    private Vector3 direction;
    private Vector3 coordinate;
    private Vector3 difference;
    private float vu;
    private double distance;
    
    //Variables for the text zones on the interface
    [SerializeField]
    private TextMeshProUGUI namePoint;
    [SerializeField]
    private TextMeshProUGUI geographicInfos;
    [SerializeField]
    private TextMeshProUGUI detailledInfos;

    // Detect if the user is looking at the point of interest
    private bool IsFacing(Vector3 coordinate)
    {
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

    private bool IsNear(Vector3 coordinate)
    {
        difference = new Vector3(camera.transform.position.x - coordinate.x, camera.transform.position.y - coordinate.y, camera.transform.position.z - coordinate.z);
        distance = Math.Sqrt(Math.Pow(difference.x, 2f) + Math.Pow(difference.y, 2f) + Math.Pow(difference.z, 2f));
        if (distance < 40)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    // Change the informations on the canva depending on the point of interest location
    void UpdateInfos()
    {
        // Select the right infos on the json file
        if(reference.WikipediaAPI.data.found == true)
        {
            namePoint.text = reference.WikipediaAPI.data.pages.title;
            detailledInfos.text = reference.WikipediaAPI.data.pages.extract;
        }
    }

    // Update is called once per frame
    void Update()
    {
        coordinate = reference.transform.position;
        if (IsFacing(coordinate) && IsNear(coordinate))
        {
            UpdateInfos();
        }
    }
}

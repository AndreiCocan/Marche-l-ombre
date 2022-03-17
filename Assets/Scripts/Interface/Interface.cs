using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;
using UnityEngine.XR.Interaction.Toolkit.Inputs;

public class Interface : MonoBehaviour
{
    public GameObject main;
    public Vector3 offSet;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void lateUpdate()
    {
        transform.position = main.transform.position + offSet;
    }
}
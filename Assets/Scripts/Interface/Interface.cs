using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;
using UnityEngine.XR.Interaction.Toolkit.Inputs;

public class Interface : MonoBehaviour
{
    public GameObject rightArm;
    public Vector3 offSetInterface;

    // Update is called once per frame
    void LateUpdate()
    {
        transform.position = rightArm.transform.position + offSetInterface;
    }
}
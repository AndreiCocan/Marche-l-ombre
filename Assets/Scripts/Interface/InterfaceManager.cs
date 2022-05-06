using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;
using UnityEngine.XR.Interaction.Toolkit.Inputs;

public class InterfaceManager : MonoBehaviour
{

    private GameObject Interface;
    private bool isPressed = false;

    private void Start()
    {
       Interface = GameObject.FindWithTag("Interface");
    }


    // Show the interface when the trigger is pressed
    private void Affichage_Interface_On_Trigger()
    {
        var LeftHandDevices = new List<UnityEngine.XR.InputDevice>();
        UnityEngine.XR.InputDevices.GetDevicesAtXRNode(UnityEngine.XR.XRNode.LeftHand, LeftHandDevices);
        
        if (LeftHandDevices.Count > 0)
        {
            UnityEngine.XR.InputDevice device = LeftHandDevices[0];
            bool triggerValue;
            if (device.TryGetFeatureValue(UnityEngine.XR.CommonUsages.triggerButton, out triggerValue) && triggerValue && !isPressed)
            {
                isPressed = true;
                Debug.Log("Trigger button is pressed.");
                if (Interface.activeSelf == true)
                {
                    Interface.SetActive(false);
                }
                else
                {
                    Interface.SetActive(true);
                }
                
            }
            if(device.TryGetFeatureValue(UnityEngine.XR.CommonUsages.triggerButton, out triggerValue) && !triggerValue)
            {
                isPressed = false;
            }
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Affichage_Interface_On_Trigger();
    }

}
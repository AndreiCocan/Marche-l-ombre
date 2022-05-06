using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;
using UnityEngine.InputSystem.XR;
using UnityEngine.XR.Interaction.Toolkit.Inputs;

public class InterfaceManager : MonoBehaviour
{

    private GameObject Interface;
    private bool isPressed = false;

    private UnityEngine.XR.InputDevice device;

    private void Start()
    {
       Interface = GameObject.FindWithTag("Interface");

    }


    // Show the interface when the trigger is pressed
    private void Affichage_Interface_On_Trigger()
    {

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
                Interface.GetComponent<Info_Interface>().UpdateInfos(null);
          }
                
        }
       if(device.TryGetFeatureValue(UnityEngine.XR.CommonUsages.triggerButton, out triggerValue) && !triggerValue)
       {
             isPressed = false;
       }
        
    }

    public bool SendHaptic(float amplitude, float duration)
    {
        if (device.TryGetHapticCapabilities(out var capabilities) &&
            capabilities.supportsImpulse)
        {
            return device.SendHapticImpulse(0u, amplitude, duration);
        }
        return false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        var LeftHandDevices = new List<UnityEngine.XR.InputDevice>();
        UnityEngine.XR.InputDevices.GetDevicesAtXRNode(UnityEngine.XR.XRNode.LeftHand, LeftHandDevices);
        if (LeftHandDevices.Count > 0)
        {
            device = LeftHandDevices[0];
        }
        Affichage_Interface_On_Trigger();
    }

}
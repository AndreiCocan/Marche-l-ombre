using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;
using UnityEngine.XR.Interaction.Toolkit.Inputs;

public class Interface : MonoBehaviour
{
    [SerializeField] private GameObject rightArm;
    [SerializeField] private Vector3 offSetInterface;

    // Follow the movements of the user's right arm
    private void Interface_Follow_Arm_Movements()
    {
        transform.position = rightArm.transform.position + offSetInterface;
    }

    // Show the interface when the trigger is pressed
    private void Affichage_Interface_On_Trigger()
    {
        var RightHandDevices = new List<UnityEngine.XR.InputDevice>();
        UnityEngine.XR.InputDevices.GetDevicesAtXRNode(UnityEngine.XR.XRNode.RightHand, RightHandDevices);
        
        if (RightHandDevices.Count > 0)
        {
            UnityEngine.XR.InputDevice device = RightHandDevices[0];
            bool triggerValue;
            if (device.TryGetFeatureValue(UnityEngine.XR.CommonUsages.triggerButton, out triggerValue) && triggerValue)
            {
                Debug.Log("Trigger button is pressed.");
            }
        }
    }

    // Update is called once per frame
    void LateUpdate()
    {
        Interface_Follow_Arm_Movements();
        Affichage_Interface_On_Trigger();
    }

}
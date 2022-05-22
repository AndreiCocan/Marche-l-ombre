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
    private bool open = true;

    [SerializeField]
    GameObject InfoCanvas;
    [SerializeField]
    GameObject ScrollerCanvas;


    //To know wich canvas to open when all canvas are closed
    private bool _isInfoCanvasActive;

    private UnityEngine.XR.InputDevice device;

    private void Start()
    {
       Interface = GameObject.FindWithTag("Interface");
        
        InfoCanvas.GetComponent<Canvas>().enabled = false;
        ScrollerCanvas.GetComponent<Canvas>().enabled = true;
        _isInfoCanvasActive = false;
    }


    public void InfoCanvasActive()
    {
        InfoCanvas.GetComponent<Canvas>().enabled = true;
        ScrollerCanvas.GetComponent<Canvas>().enabled = false;

        InfoCanvas.GetComponent<Animator>().SetTrigger("Open");
        ScrollerCanvas.GetComponent<Animator>().SetTrigger("Open");
        
        _isInfoCanvasActive = true;
    }
    public void ScrollerCanvasActive()
    {
        InfoCanvas.GetComponent<Canvas>().enabled = false;
        ScrollerCanvas.GetComponent<Canvas>().enabled = true;

        InfoCanvas.GetComponent<Animator>().SetTrigger("Close");        
        ScrollerCanvas.GetComponent<Animator>().SetTrigger("Close");
        
        _isInfoCanvasActive = false;
    }

    public void AllCanvasEnable()
    {
        if (_isInfoCanvasActive == true)
        {
            InfoCanvas.GetComponent<Canvas>().enabled = true;
        }
        else
        {
            ScrollerCanvas.GetComponent<Canvas>().enabled = true;
        }
    }

    public void AllCanvasDisable()
    {
        InfoCanvas.GetComponent<Canvas>().enabled = false;
        ScrollerCanvas.GetComponent<Canvas>().enabled = false;
    }


    public void ConfigureInfoCanvas(pages _page)
    {
        InfoCanvas.GetComponent<InfoCanvas>().ConfigureCanvas(_page);
    } 

    // Show the interface when the trigger is pressed
    private void Affichage_Interface_On_Trigger()
    {

        bool triggerValue;
        if (((device.TryGetFeatureValue(UnityEngine.XR.CommonUsages.triggerButton, out triggerValue) && triggerValue)|| Input.GetKeyDown(KeyCode.T)) && !isPressed)
        {
           isPressed = true;
           Debug.Log("Trigger button is pressed.");
            
            if (open == true)
            {
                if (Interface.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("InterfaceOpened"))
                {
                    open = false;
                }                
                Interface.GetComponent<Animator>().SetTrigger("Close");
                

                //AllCanvasDisable();
                
            }
            else
            {
                if (Interface.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("InterfaceClosed"))
                {

                    open = true;
                }                
                Interface.GetComponent<Animator>().SetTrigger("Open");
                

                //AllCanvasEnable();
                Interface.GetComponent<Info_Interface>().UpdateInfos(null);
                
            }
                
        }
       if((device.TryGetFeatureValue(UnityEngine.XR.CommonUsages.triggerButton, out triggerValue) && !triggerValue) || !Input.GetKeyDown(KeyCode.T))
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
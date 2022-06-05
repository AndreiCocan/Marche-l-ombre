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
    private GameObject Minimap;

    private bool isPressedInterface = false;
    private bool openInterface = true;

    private bool isPressedMinimap = false;
    private bool openMinimap = false;

    [SerializeField]
    GameObject InfoCanvas;
    [SerializeField]
    GameObject ScrollerCanvas;


    //To know wich canvas to open when all canvas are closed
    private bool _isInfoCanvasActive;

    //VR controllers
    private UnityEngine.XR.InputDevice left;
    private UnityEngine.XR.InputDevice right;

    private void Start()
    {
        Interface = GameObject.FindWithTag("Interface");
        Minimap = GameObject.FindWithTag("Minimap");

        InfoCanvas.GetComponent<Canvas>().enabled = false;
        ScrollerCanvas.GetComponent<Canvas>().enabled = true;
        _isInfoCanvasActive = false;
    }
     
    //Show InfoCanvas and hide ScrollerCanvas
    public void InfoCanvasActive()
    {
        InfoCanvas.GetComponent<Canvas>().enabled = true;
        ScrollerCanvas.GetComponent<Canvas>().enabled = false;

        InfoCanvas.GetComponent<Animator>().SetTrigger("Open");
        ScrollerCanvas.GetComponent<Animator>().SetTrigger("Open");
        
        _isInfoCanvasActive = true;
    }

    //Show InfoCanvas and hide ScrollerCanvas
    public void ScrollerCanvasActive()
    {
        InfoCanvas.GetComponent<Canvas>().enabled = false;
        ScrollerCanvas.GetComponent<Canvas>().enabled = true;

        InfoCanvas.GetComponent<Animator>().SetTrigger("Close");        
        ScrollerCanvas.GetComponent<Animator>().SetTrigger("Close");
        
        _isInfoCanvasActive = false;
    }

    //Show the GUI (show the last canvas opened before it was hidden)
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

    //Hide all GUI
    public void AllCanvasDisable()
    {
        InfoCanvas.GetComponent<Canvas>().enabled = false;
        ScrollerCanvas.GetComponent<Canvas>().enabled = false;
    }

    //Load the page data to the InfoCanvas
    public void ConfigureInfoCanvas(pages _page)
    {
        InfoCanvas.GetComponent<InfoCanvas>().ConfigureCanvas(_page);
    } 

    // Show/Hide the interface when the trigger is pressed
    private void Affichage_Interface_On_Trigger()
    {

        bool triggerValue;
        if ((left.TryGetFeatureValue(UnityEngine.XR.CommonUsages.triggerButton, out triggerValue) && triggerValue) && !isPressedInterface)
        {
            isPressedInterface = true;
           Debug.Log("Trigger button is pressed.");
            
            if (openInterface == true)
            {
                if (Interface.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("InterfaceOpened"))
                {
                    openInterface = false;
                }                
                Interface.GetComponent<Animator>().SetTrigger("Close");
                

                //AllCanvasDisable();
                
            }
            else
            {
                if (Interface.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("InterfaceClosed"))
                {

                    openInterface = true;
                }                
                Interface.GetComponent<Animator>().SetTrigger("Open");
                

                //AllCanvasEnable();
                Interface.GetComponent<Info_Interface>().UpdateInfos(null);
                
            }
                
        }
       if(left.TryGetFeatureValue(UnityEngine.XR.CommonUsages.triggerButton, out triggerValue) && !triggerValue)
       {
            isPressedInterface = false;
       }
        
    }
    //Show/Hide the minimap when the trigger is pressed
    void Affichage_Minimap_On_Trigger()
    {
        bool triggerValue;
        if ((right.TryGetFeatureValue(UnityEngine.XR.CommonUsages.gripButton, out triggerValue) && triggerValue) && !isPressedMinimap)
        {
            isPressedMinimap = true;
            Debug.Log("Map button is pressed.");

            if (openMinimap == true)
            {
                if (Minimap.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("MinimapOpened"))
                {
                    openMinimap = false;
                }
                Minimap.GetComponent<Animator>().SetTrigger("Close");


            }
            else
            {
                if (Minimap.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("MinimapClosed"))
                {

                    openMinimap = true;
                }
                Minimap.GetComponent<Animator>().SetTrigger("Open");

            }

        }
        
        if (right.TryGetFeatureValue(UnityEngine.XR.CommonUsages.gripButton, out triggerValue) && !triggerValue )
        {
            isPressedMinimap = false;
        }

    }

    //Haptic impulsion  in the left controller
    public bool SendHaptic(float amplitude, float duration)
    {
        if (left.TryGetHapticCapabilities(out var capabilities) &&
            capabilities.supportsImpulse)
        {
            return left.SendHapticImpulse(0u, amplitude, duration);
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
            left = LeftHandDevices[0];
        }
        var RightHandDevices = new List<UnityEngine.XR.InputDevice>();
        UnityEngine.XR.InputDevices.GetDevicesAtXRNode(UnityEngine.XR.XRNode.RightHand, RightHandDevices);
        if (RightHandDevices.Count > 0)
        {
            right = RightHandDevices[0];
        }

        Affichage_Interface_On_Trigger();
        Affichage_Minimap_On_Trigger();
    }

}
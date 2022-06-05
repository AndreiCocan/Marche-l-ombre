using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackButton : MonoBehaviour
{
    private InterfaceManager InterfaceManag;
    private void Start()
    {
        //Can also be done in the inspector
        GetComponent<Button>().onClick.AddListener(ButtonListener);

        if (InterfaceManag == null)
        {
            InterfaceManag = FindObjectOfType<InterfaceManager>();
        }
    }


    private void ButtonListener()
    {
        //Get back to the Scroller Canvas
        InterfaceManag.ScrollerCanvasActive();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackButton : MonoBehaviour
{
    private GameObject Interface;
    private void Start()
    {
        //Can also be done in the inspector
        GetComponent<Button>().onClick.AddListener(ButtonListener);
        Interface = GameObject.FindWithTag("Interface");
    }


    private void ButtonListener()
    {
        foreach (Transform child in Interface.transform)
        {
            if (string.Equals(child.tag, "InfoCanvas"))
            {
                child.gameObject.SetActive(false);
            }
            else if (string.Equals(child.tag, "ScrollerCanvas"))
            {
                child.gameObject.SetActive(true);
                Interface.gameObject.GetComponent<Info_Interface>().UpdateInfos(null);
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class pages
{
    [HideInInspector]
    public string pageid, ns;
    public string title, extract;
}


[System.Serializable]
public class Data
{
    public bool found;
    public pages pages;
}


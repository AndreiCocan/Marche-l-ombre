using Mapbox.Utils;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class pages
{
    [HideInInspector]
    public string pageid, ns;
    public string title, extract, lat, lon;

}


[System.Serializable]
public class Data
{
    public bool found;
    public List<pages> pages;
    public Vector2d latlon;

    public Data()
    {
        found = false;
        pages = new List<pages>();
    }

}


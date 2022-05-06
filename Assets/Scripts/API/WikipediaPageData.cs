﻿using Mapbox.Utils;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class pages
{
    [HideInInspector]
    public string pageid, ns;
    public string title, extract, lat, lon;

    public bool equals(object o)
    {
        if (o.GetType() == typeof(pages))
        {
            return pageid==((pages)o).pageid;

        }
        return false;
    }
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


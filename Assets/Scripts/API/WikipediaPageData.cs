using Mapbox.Utils;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//Wikipedia article

[System.Serializable]
public class pages
{
    [HideInInspector]
    public string pageid, ns;
    public string title, extract, lat, lon;

}

public class PageComparer : IEqualityComparer<pages>
{
    public bool Equals(pages x, pages y)
    {
        return x.pageid == y.pageid;
    }
    public int GetHashCode(pages obj)
    {
        return obj.pageid.GetHashCode();
    }
}

//Wikipedia search
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


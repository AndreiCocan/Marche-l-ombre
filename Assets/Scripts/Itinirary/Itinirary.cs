using Mapbox.Utils;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Itinirary
{
    public Vector2d start;
    public List<Vector2d> waypoints;

    public Itinirary(Vector2d start, List<Vector2d> waypoints)
    {
        this.start = start;
        this.waypoints = waypoints;
    }
}

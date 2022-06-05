using Mapbox.Utils;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Itinirary
{
    //Starting positon of the player at the start of the game
    public Vector2d start;
    
    //Waypoint list
    public List<Vector2d> waypoints;

    public Itinirary(Vector2d start, List<Vector2d> waypoints)
    {
        this.start = start;
        this.waypoints = waypoints;
    }
}

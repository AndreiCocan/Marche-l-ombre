using Mapbox.Unity.Map;
using Mapbox.Unity.MeshGeneration.Interfaces;
using Mapbox.Utils;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.UI;

public class GeoGetter : MonoBehaviour, IFeaturePropertySettable
{

	public string _name;
	public Vector2d _pos;
	AbstractMap _map;
	GameObject parent;

	[SerializeField]
	private CharacterController player;

    
    private WikipediaAPI WikipediaAPI;
    
    [SerializeField]
	float visitedDistance = 20;

	public float dis;

    public bool isVisited = false;
    
    protected virtual void Awake()
	{
		if (_map == null)
		{
			_map = FindObjectOfType<AbstractMap>();
		}
		if (WikipediaAPI == null)
		{
			WikipediaAPI = FindObjectOfType<WikipediaAPI>();
		}
		if (player == null)
		{
			player = FindObjectOfType<CharacterController>();
		}

	}
    
	private void Start()
	{
        parent = this.transform.parent.gameObject;
	}

	public void Set(Dictionary<string, object> props)
	{
		parent = this.transform.parent.gameObject;
		_name = "";

		if (props.ContainsKey("name"))
		{
			_name = props["name"].ToString();
			_pos = _map.WorldToGeoPosition(parent.transform.position);
		}
	}

    private void LateUpdate()
    {
		//Distance between ther user and the next waypoint
		dis = (float)Vector3.Distance(player.transform.position, parent.transform.position);
        if (dis <= visitedDistance && isVisited==false)
        {
			Debug.Log("Start Search");
			WikipediaAPI.Search(_pos,_name);
			isVisited = true;
        }
        if (dis > visitedDistance)
        {
            isVisited = false;
        }
    }
}

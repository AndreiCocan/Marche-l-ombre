using Mapbox.Unity.Map;
using Mapbox.Unity.MeshGeneration.Interfaces;
using Mapbox.Utils;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.UI;

public class GeoGetter : MonoBehaviour, IFeaturePropertySettable
{
	
	public string _name;
	public Vector2d _pos;
	AbstractMap _map;
	GameObject parent;

	protected virtual void Awake()
	{
		if (_map == null)
		{
			_map = FindObjectOfType<AbstractMap>();
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
            _name =props["name"].ToString();
			_pos = _map.WorldToGeoPosition(parent.transform.position);
			//Debug.Log("Name: " + _name + " " + parent.name + " " + _pos);
		}
	}
}

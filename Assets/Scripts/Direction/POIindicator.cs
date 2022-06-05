using Mapbox.Unity.Map;
using Mapbox.Utils;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class POIindicator : MonoBehaviour
{
    [SerializeField]
    AbstractMap _map;

    [SerializeField]
    GameObject _PoiIcon;

    [SerializeField]
    GameObject _PoiTracker;

    [SerializeField]
    float _spawnScaleIcon = 30f;

    [SerializeField]
    float _spawnScaleTracker = 30f;


    protected void Awake()
    {
        if (_map == null)
        {
            _map = FindObjectOfType<AbstractMap>();
        }
    }
    
    //Spawn indicators on the POI found with the Wikipedia search
    public void spawnPOI(Vector2d location,string name)
    {
        //Spawn the POI icon for the minimap
        GameObject PoiIcon= Instantiate(_PoiIcon, transform);
        PoiIcon.GetComponentInChildren<TMPro.TextMeshProUGUI>().text = name;
        PoiIcon.transform.localPosition = _map.GeoToWorldPosition(location, true);
	    PoiIcon.transform.localPosition += new Vector3(0, 600, 0);
        PoiIcon.transform.localScale = new Vector3(_spawnScaleIcon, _spawnScaleIcon,1);

        //Spawn POI tracker
        GameObject PoiTracker = Instantiate(_PoiTracker, transform);
        PoiTracker.transform.localPosition = _map.GeoToWorldPosition(location, true);
        PoiTracker.transform.localPosition += new Vector3(0, 1, 0);
        PoiTracker.transform.localScale = new Vector3(_spawnScaleTracker, _spawnScaleTracker, 1);
    }

}

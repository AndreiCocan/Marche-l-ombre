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

    // Start is called before the first frame update
    void Start()
    {
        
    }

    protected void Awake()
    {
        if (_map == null)
        {
            _map = FindObjectOfType<AbstractMap>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void spawnPOI(Vector2d location)
    {
        GameObject spawnedObject= Instantiate(_PoiIcon, transform);
        spawnedObject.transform.localPosition = _map.GeoToWorldPosition(location, true);
	    spawnedObject.transform.localPosition += new Vector3(0, 600, 0);
        spawnedObject.transform.localScale = new Vector3(_spawnScaleIcon*0.5f, _spawnScaleIcon,1);

        GameObject PoiTracker = Instantiate(_PoiTracker, transform);
        PoiTracker.transform.localPosition = _map.GeoToWorldPosition(location, true);
        PoiTracker.transform.localPosition += new Vector3(0, 1, 0);
        PoiTracker.transform.localScale = new Vector3(_spawnScaleTracker, _spawnScaleTracker, 1);
    }

}

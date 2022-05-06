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
    GameObject _PoiPrefab;

    [SerializeField]
    float _spawnScale = 100f;

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
        GameObject spawnedObject= Instantiate(_PoiPrefab, transform);
        spawnedObject.transform.localPosition = _map.GeoToWorldPosition(location, true);
	    spawnedObject.transform.localPosition += new Vector3(0, 20, 0);
        spawnedObject.transform.localScale = new Vector3(_spawnScale, _spawnScale, _spawnScale);
    }

}

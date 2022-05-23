using Mapbox.Unity.Map;
using Mapbox.Unity.Utilities;
using Mapbox.Utils;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class ItineraryProvider: MonoBehaviour
{
    [SerializeField]
    public AbstractMap map;

    public SpawnOnMap spawnOnMap;

    Itinirary itinirary;
    
   
    string path;
    // Start is called before the first frame update
    void Awake()
    {
        path = Path.Combine(Application.persistentDataPath, "ItinararynData.json");
        if (!File.Exists(path))
        {
            itinirary = new Itinirary(new Vector2d(48.85877975, 2.294786), new List<Vector2d>() { new Vector2d(48.863301 , 2.287061), new Vector2d(48.8561, 2.2825) });
            string itiJSON = JsonUtility.ToJson(itinirary);
            File.WriteAllText(path, itiJSON);
            Debug.Log("Standard Itinirary loaded");
            //Debug.Log(itiJSON);
            //Debug.Log(Application.persistentDataPath);
        }
        else
        {
            string itiJSON=File.ReadAllText(path);
            itinirary = JsonUtility.FromJson<Itinirary>(itiJSON);
            Debug.Log(itinirary.start);
        }
        
        if (spawnOnMap == null)
        {
            spawnOnMap= FindObjectOfType<SpawnOnMap>();
        }

        spawnOnMap.Initialize(itinirary.waypoints);

    }

    private void Start()
    {
        if (map == null)
        {
            map = FindObjectOfType<AbstractMap>();
        }

        map.Initialize(itinirary.start,15);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

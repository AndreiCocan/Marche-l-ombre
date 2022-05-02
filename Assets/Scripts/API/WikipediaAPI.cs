using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using Mapbox.Utils;

public class WikipediaAPI : MonoBehaviour
{
    public Vector2d latlong= new Vector2d(0,0);
    public Vector2d Lastlatlong = new Vector2d(0, 0);
    public  string Name = "";
    public string LastName = "";
    
    
    string urlByName= @"https://fr.wikipedia.org/w/api.php?format=json&action=query&prop=extracts&exintro&explaintext&redirects=1&titles=";
    string urlByGeoSearch = @"https://fr.wikipedia.org/w/api.php?format=json&action=query&prop=extracts&exintro&explaintext&redirects=1&list=geosearch&gsradius=10&gscoord=";
    
    [SerializeField]
    public Data data;

    
    public async void Search(Vector2d latlon, string name)
    {
        this.latlong = latlon;
        Name = name;
        if(latlong.x == 0 && latlong.y == 0)
        {
            data.pages.extract = "";
            data.pages.title = "";
            data.found = false;
            return;
        }

        if (!string.Equals(Name,LastName))
        {
            StartCoroutine(LoadData());

        }
    }

    IEnumerator  LoadData()
    {
        WWW wwwGeosearch = new WWW(urlByGeoSearch+ latlong.x + "|"+ latlong.y);
        yield return wwwGeosearch;
        if(wwwGeosearch.error==null)
        {
            string source = wwwGeosearch.text;
            string[] stringSeparators = {"pageid"};
            string[] result = source.Split(stringSeparators, StringSplitOptions.None);
            if(result.Length>1)
            {
                data.found = true;
                Lastlatlong = latlong;
                string newJson = "{\"pageid" + result[1];
                newJson = newJson.Substring(0, newJson.Length - 3);
                data.pages = JsonUtility.FromJson<pages>(newJson);
            }
            else
            {
                data.pages.title = Name;
                data.found = false;
            }
        }

        string[] nameRes = data.pages.title.Split('—');

        foreach(string name in nameRes)
        {
            WWW wwwExtract = new WWW(urlByName + name);
            yield return wwwExtract;
            if (wwwExtract.error == null)
            {
                string source = wwwExtract.text;
                string[] stringSeparators = { "pageid" };
                string[] result = source.Split(stringSeparators, StringSplitOptions.None);
                if (result.Length > 1)
                {
                    data.found = true;
                    string newJson = "{\"pageid" + result[1];
                    newJson = newJson.Substring(0, newJson.Length - 3);
                    data.pages = JsonUtility.FromJson<pages>(newJson);
                    LastName = Name;

                }
                else
                {
                    data.pages.extract = "No result found";
                    data.found = false;
                }
                
            }

        }

        Debug.Log(data.pages.extract);
        yield return MicrosoftTTS.Speech(data.pages.extract);

    }
}



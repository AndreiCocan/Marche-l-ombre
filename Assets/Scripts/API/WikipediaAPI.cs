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
    public  string name = "";
    public string LastName = "";
    
    
    string urlByName= @"https://fr.wikipedia.org/w/api.php?format=json&action=query&prop=extracts&exintro&explaintext&redirects=1&titles=";
    string urlByGeoSearch = @"https://fr.wikipedia.org/w/api.php?format=json&action=query&prop=extracts&exintro&explaintext&redirects=1&list=geosearch&gsradius=50&gscoord=";
    
    private Data data;

    
    public async void Search(Vector2d latlon, string name)
    {
        data = new Data();
        
        this.latlong = latlon;
        this.name = name;

        if (!string.Equals(this.name, LastName) && latlon.x!=Lastlatlong.x && latlon.y!=Lastlatlong.y)
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
                for (int i = 1; i < result.Length; i++) {
                    string newJson = "{\"pageid" + result[i];
                    newJson = newJson.Substring(0, newJson.Length - 3);
                    data.pages.Add(JsonUtility.FromJson<pages>(newJson));
                }
           
            }
            else
            {
                data.found = false;
            }
        }

        foreach(pages pages in data.pages)
        {
            string[] nameRes = pages.title.Split('—');

            foreach (string name in nameRes)
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
                        pages.title = JsonUtility.FromJson<pages>(newJson).title;
                        pages.extract = JsonUtility.FromJson<pages>(newJson).extract;
                        LastName = this.name;

                    }
                    else
                    {
                        pages.extract = "No result found";
                        data.found = false;
                    }

                }

            }
            Debug.Log(pages.title+":"+pages.extract);
        }
        
        //yield return MicrosoftTTS.Speech(data.pages.extract);

    }
}



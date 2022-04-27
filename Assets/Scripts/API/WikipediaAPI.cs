using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;


public class WikipediaAPI : MonoBehaviour
{
    public string Lat= "";
    public string Lon = "";
    public string Name = "";
    private string finalUrl;
    string urlByName= @"https://fr.wikipedia.org/w/api.php?format=json&action=query&prop=extracts&exintro&explaintext&redirects=1&titles=";
    string urlByGeoSearch = @"https://fr.wikipedia.org/w/api.php?format=json&action=query&prop=extracts&exintro&explaintext&redirects=1&list=geosearch&gsradius=10&gscoord=";
    [SerializeField]
    private Data data;
    private string lastLat;
    private string lastLon;    
    private string title;
    private string extract;
    


    void Start()
    {
        Search();
    }
 
    public void Search()
    {
        if(Lat=="" || Lon ==" ")
        {
            data.pages.extract = "";
            data.pages.title = "";
            data.found = false;
            return;
        }

        if(!(Lat== lastLat && Lon==lastLon))
        {
            StartCoroutine(LoadData());
        } else
        {
            //loadingPanel.SetActive(false);
        }
    }

    IEnumerator LoadData()
    {
        WWW wwwGeosearch = new WWW(urlByGeoSearch+Lat+"|"+Lon);
        yield return wwwGeosearch;
        if(wwwGeosearch.error==null)
        {
            string source = wwwGeosearch.text;
            string[] stringSeparators = {"pageid"};
            string[] result = source.Split(stringSeparators, StringSplitOptions.None);
            if(result.Length>1)
            {
                data.found = true;
                lastLat = Lat;
                lastLon = Lon;
                string newJson = "{\"pageid" + result[1];
                newJson = newJson.Substring(0, newJson.Length - 3);
                data.pages = JsonUtility.FromJson<pages>(newJson);
                

            }
            else
            {
                data.pages.title = Name;
                data.found = false;
            }

            Debug.Log(data.pages.title);

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

                }
                else
                {
                    data.pages.extract = "No result found";
                    data.found = false;
                }
                
            }
            Debug.Log(data.pages.extract);
        }

    }
}



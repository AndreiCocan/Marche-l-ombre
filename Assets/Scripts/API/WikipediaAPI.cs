using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using Mapbox.Utils;

public class WikipediaAPI : MonoBehaviour
{
    //Name of the current reasearch
    public  string name = "";
    
    //Attributes  of the last reasearch
    public string LastName = "";
    public Vector2d Lastlatlong = new Vector2d(0, 0);

    //Url to rearch by article title (to get article)
    string urlByName= @"https://fr.wikipedia.org/w/api.php?format=json&action=query&prop=extracts&exintro&explaintext&redirects=1&titles=";
    //Url to rearch by geographical position (to get articles title)
    string urlByGeoSearch = @"https://fr.wikipedia.org/w/api.php?format=json&action=query&prop=extracts&exintro&explaintext&redirects=1&list=geosearch&gsradius=50&gscoord=";
    
    //Result of the reasearch
    private Data data;
    
    //Interface GUI du bras gauche
    public Info_Interface ifi;

    private void Start()
    {
        if (ifi == null)
        {
            ifi = FindObjectOfType<Info_Interface>();
        }
    }
    
    //Init a search from a position and a name
    public async void Search(Vector2d latlong, string name)
    {

        this.name = name;

        if (!string.Equals(this.name, LastName) && latlong.x!=Lastlatlong.x && latlong.y!=Lastlatlong.y)
        {
            data = new Data();

            data.latlon.x = latlong.x;
            data.latlon.y = latlong.y;
            StartCoroutine(LoadData());
        }
    }

    //Api request
    IEnumerator  LoadData()
    {
        //Search by geographical position to get a liste of the titles of nearby articles
        WWW wwwGeosearch = new WWW(urlByGeoSearch+data.latlon.x.ToString().Replace(',','.')+"|"+ data.latlon.y.ToString().Replace(',', '.'));
        yield return wwwGeosearch;
        if(wwwGeosearch.error==null)
        {
            string source = wwwGeosearch.text;
            string[] stringSeparators = {"pageid"};
            string[] result = source.Split(stringSeparators, StringSplitOptions.None);

            if(result.Length>1)
            {
                data.found = true;
                Lastlatlong = data.latlon;
                for (int i = 1; i < result.Length; i++) {
                    string newJson = "{\"pageid" + result[i];
                    newJson = newJson.Substring(0, newJson.Length - 3);
                    data.pages.Add(JsonUtility.FromJson<pages>(newJson));
                }

           
            }
            else
            {
                data.found = false;
                Debug.Log("datanoutfoud");
            }
        }
        //If no article found, create an artical (pages) with the name from the Search function 
        if (data.found == false)
        {
            data = new Data();
            data.pages.Add(new pages());
            data.pages[0].title = name;
        }

        //Search by article title to get the article extract
        foreach (pages pages in data.pages)
        {
                WWW wwwExtract = new WWW(urlByName + pages.title);
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
            Debug.Log(pages.title+":"+pages.extract);
        }
        //Add the result to the interface list
        ifi.UpdateInfos(data);
    }
}



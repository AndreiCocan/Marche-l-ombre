using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;


public class WikipediaAPI : MonoBehaviour
{
    public string searchName= "Tour Eiffel";
    private string finalUrl;
    string url= @"https://fr.wikipedia.org/w/api.php?format=json&action=query&prop=extracts&exintro&explaintext&redirects=1&titles=";
    public Data data;
    private string lastWord;
    private string title;
    private string extract;


    void Start()
    {
        Search();
    }
 
    public void Search()
    {
        if(searchName=="" || searchName ==" ")
        {
            data.pages.extract = "";
            data.pages.title = "";
            data.found = false;
            return;
        }

        if(searchName != lastWord)
        {
            StartCoroutine(LoadData());
        } else
        {
            //loadingPanel.SetActive(false);
        }
    }

    IEnumerator LoadData()
    {
        WWW www = new WWW(url+ searchName);
        yield return www;
        if(www.error==null)
        {
            string source = www.text;
            string[] stringSeparators = {"pageid"};
            string[] result = source.Split(stringSeparators, StringSplitOptions.None);
            if(result.Length>1)
            {
                data.found = true;
                lastWord = searchName;
                string newJson = "{\"pageid" + result[1];
                newJson = newJson.Substring(0, newJson.Length - 3);
                data.pages = JsonUtility.FromJson<pages>(newJson);

                title=data.pages.title;
                extract = data.pages.extract;
                Debug.Log(extract);
            }
            else
            {
                data.pages.extract = "";
                data.pages.title = "";
                data.found = false;
            }
            
        }
        
    }
}



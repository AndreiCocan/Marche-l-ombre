using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text;

public class NewBehaviourScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Search();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    async static void Search()
    {
    //...
    HttpClient client = new HttpClient();
    //client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", key);

    string uri = "https://diffuseur.datatourisme.gouv.fr/webservice/664e31e7e27612ae7318527e65624e32/5c7a46f5-a0f4-4a7a-9a6b-8507c17af852";

    HttpResponseMessage response = await client.GetAsync(uri);

    string contentString = await response.Content.ReadAsStringAsync();
    dynamic parsedJson = JsonConvert.DeserializeObject(contentString);
    Console.WriteLine("Hello");
    Console.WriteLine(parsedJson);
    }
}

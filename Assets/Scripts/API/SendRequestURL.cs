using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text;
using System.IO.Compression;

public class SendRequestURL : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        DownloadFile();
    }

    // Update is called once per frame
    void Update()
    {
        
    }



    public void DownloadFile()
    {
        string remoteUri = "http://www.contoso.com/library/homepage/images/";
    }
}

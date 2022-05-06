using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Interface_Info : MonoBehaviour
{
    //UI
    public Text title;
    public Text extract;
    public Text latlon;


    //Model
    private pages _page;

    //This is called from the SetCell method in DataSource
    public void ConfigureInterface(pages page)
    {
        _page = page;
        title.text = page.title;
        extract.text = page.extract;
        latlon.text = page.lat + " , " + page.lon;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InfoCanvas : MonoBehaviour
{
    //UI
    public TMPro.TextMeshProUGUI title;
    public TMPro.TextMeshProUGUI extract;
    public TMPro.TextMeshProUGUI latlon;

    [SerializeField]
    private GameObject Audioguid_button;

    //Model
    private pages _page;

    //This is called from the SetCell method in DataSource
    public void ConfigureCanvas(pages page)
    {
        _page = page;
        title.text = page.title;
        extract.text = page.extract;
        latlon.text = page.lat + " , " + page.lon;
        Audioguid_button.GetComponent<AudioguideButton>().ConfigureCell(page);
    }
}

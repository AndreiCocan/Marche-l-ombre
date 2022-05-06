using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioguideButton : MonoBehaviour
{

    //Model
    private pages _page;
    private int _cellIndex;

    private void Start()
    {
        //Can also be done in the inspector
        GetComponent<Button>().onClick.AddListener(ButtonListener);
    }

    //This is called from the SetCell method in DataSource
    public void ConfigureCell(pages page)
    {
        _page = page;
    }


    private async void ButtonListener()
    {
        await MicrosoftTTS.Speech(_page.extract);
    }
}

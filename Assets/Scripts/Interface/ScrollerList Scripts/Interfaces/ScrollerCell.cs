using UnityEngine;
using UnityEngine.UI;
using ScrollerList.UI;


public class ScrollerCell : MonoBehaviour, ICell
{
    //UI
    public Text title;
    

    private GameObject Interface;
    private InterfaceManager InterfaceManag;

    //Model
    private pages _page;
    private int _cellIndex;

    private void Start()
    {
        //Can also be done in the inspector
        GetComponent<Button>().onClick.AddListener(ButtonListener);
        
        if (InterfaceManag == null)
        {
            InterfaceManag = FindObjectOfType<InterfaceManager>();
        }
    }

    //This is called from the SetCell method in DataSource
    public void ConfigureCell(pages page, int cellIndex)
    {
        _cellIndex = cellIndex;
        _page = page;
        title.text = page.title+page.pageid;
    }
    

    private void ButtonListener()
    {
        Debug.Log("Index : " + _cellIndex + ", Name : " + _page.title );
        InterfaceManag.ConfigureInfoCanvas(_page);
        InterfaceManag.InfoCanvasActive();
    }
}


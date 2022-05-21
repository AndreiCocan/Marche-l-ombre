using UnityEngine;
using UnityEngine.UI;
using ScrollerList.UI;


public class ScrollerCell : MonoBehaviour, ICell
{
    //UI
    public Text title;
    

    private GameObject Interface;


    //Model
    private pages _page;
    private int _cellIndex;

    private void Start()
    {
        //Can also be done in the inspector
        GetComponent<Button>().onClick.AddListener(ButtonListener);
        Interface = GameObject.FindWithTag("Interface");
    }

    //This is called from the SetCell method in DataSource
    public void ConfigureCell(pages page, int cellIndex)
    {
        _cellIndex = cellIndex;
        _page = page;
        title.text = page.title;
    }


    private void ButtonListener()
    {
        Debug.Log("Index : " + _cellIndex + ", Name : " + _page.title );
        foreach (Transform child in Interface.transform)
        {
            if (string.Equals(child.tag, "InfoCanvas"))
            {
                child.gameObject.SetActive(true);
                child.gameObject.GetComponent<InfoCanvas>().ConfigureCanvas(_page);

            }
            else if (string.Equals(child.tag, "ScrollerCanvas"))
            {
                child.gameObject.SetActive(false);
            }
        }
    }
}


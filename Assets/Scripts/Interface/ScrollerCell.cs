using UnityEngine;
using UnityEngine.UI;
using PolyAndCode.UI;

//Cell class for demo. A cell in Recyclable Scroll Rect must have a cell class inheriting from ICell.
//The class is required to configure the cell(updating UI elements etc) according to the data during recycling of cells.
//The configuration of a cell is done through the DataSource SetCellData method.
//Check RecyclableScrollerDemo class
public class ScrollerCell : MonoBehaviour, ICell
{
    //UI
    public Text title;
    
    [SerializeField]
    private GameObject Infointerface;
    [SerializeField]
    private GameObject Scrollerinterface;

    //Model
    private pages _page;
    private int _cellIndex;

    private void Start()
    {
        //Can also be done in the inspector
        GetComponent<Button>().onClick.AddListener(ButtonListener);
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
        Infointerface.GetComponent<Interface_Info>().ConfigureInterface(_page);
        Scrollerinterface.SetActive(false);
        Infointerface.SetActive(true);
        
    }
}


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;
using ScrollerList.UI;
using Mapbox.Utils;
using System.Globalization;
using System.Linq;

public class Info_Interface : MonoBehaviour , IRecyclableScrollRectDataSource
{
    // Variables for the vision of the camera
    private Camera _camera;
    private Vector3 direction;
    private Vector3 coordinate;
    private Vector3 difference;
    private float vu;
    private double distance;
    

    [SerializeField]
    RecyclableScrollRect _recyclableScrollRect;

    InterfaceManager InterfaceManag;
    POIindicator poiindc;
    private static List<pages> pages;

    //Recyclable scroll rect's data source must be assigned in Awake.
    private void Awake()
    {
        _recyclableScrollRect.DataSource = this;
    }
    
    private void Start()
    {
        pages = new List<pages>();
        
        _camera = FindObjectOfType<Camera>();
        
        if (InterfaceManag == null)
        {
            InterfaceManag = FindObjectOfType<InterfaceManager>();
        }
        if (poiindc == null)
        {
            poiindc = FindObjectOfType<POIindicator>();
        }
        
    }



    // Add the pages to the list if not allready present
    public int UpdateInfos(Data data)
    {
        PageComparer pagec = new PageComparer();
        if (data != null && data.found==true)
        {
            foreach (pages page in data.pages)
            {
                if (!pages.Contains(page, pagec))
                {
                    pages.Add(page);
                    poiindc.spawnPOI(new Vector2d(Convert.ToDouble(page.lat, CultureInfo.InvariantCulture), Convert.ToDouble(page.lon, CultureInfo.InvariantCulture)),page.title);
                }
            }
            //Make the haptic feedback when new page added
            InterfaceManag.SendHaptic(0.3f, 0.3f);
        }
        //Update the scroll rect
        _recyclableScrollRect.ReloadData(this);

        return 0;
    }

    

    #region DATA-SOURCE


    // Data source method. return the list length.
    public int GetItemCount()
    {
        return pages.Count;
    }


    // Data source method. Called for a cell every time it is recycled. Implement this method to do the necessary cell configuration.

    public void SetCell(ICell cell, int index)
    {
        //Casting to the implemented Cell
        var item = cell as ScrollerCell;
        item.ConfigureCell(pages[index], index);
    }

    #endregion
}

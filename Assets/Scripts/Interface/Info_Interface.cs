using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

public class Info_Interface : MonoBehaviour
{
    // Variables for the vision of the camera
    private Camera _camera;
    private Vector3 direction;
    private Vector3 coordinate;
    private Vector3 difference;
    private float vu;
    private double distance;
    
    //Variables for the interface
    private TextMeshProUGUI namePoint;
    private TextMeshProUGUI geographicInfos;
    private TextMeshProUGUI detailledInfos;
    public Button info_button;
    public Canvas choice_buttons;
    public Canvas info_interface;
    private Vector2 cornerTopRight;
    private Vector2 cornerBottomLeft;

    private static Dictionary<string, pages> pages;

    private void Start()
    {
        pages = new Dictionary<string, pages>();
        _camera = FindObjectOfType<Camera>();
        namePoint = (TextMeshProUGUI)FindObjectsOfType<TextMeshProUGUI>().GetValue(0);
        geographicInfos = (TextMeshProUGUI)FindObjectsOfType<TextMeshProUGUI>().GetValue(1);
        detailledInfos = (TextMeshProUGUI)FindObjectsOfType<TextMeshProUGUI>().GetValue(2);

        cornerTopRight = new Vector2(-265, 94);
        cornerBottomLeft = new Vector2(265, -97);
    }

    // Detect if the user is looking at the point of interest
    private bool IsFacing()
    {
        coordinate = /* Take the exact coordinate of the point of interest (API script) */
        direction = (coordinate - _camera.transform.position).normalized;
        vu = Vector3.Dot(direction, transform.forward);

        if (vu <= 1 && vu >= 0.7)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private bool IsNear()
    {
        coordinate = /* Take the exact coordinate of the point of interest (API script) */
        difference = new Vector3(_camera.transform.position.x - coordinate.x, _camera.transform.position.y - coordinate.y, _camera.transform.position.z - coordinate.z);
        distance = Math.Sqrt(Math.Pow(difference.x, 2f) + Math.Pow(difference.y, 2f) + Math.Pow(difference.z, 2f));
        if (distance < 40)
        {
            return true;
        } else
        {
            return false;
        }
    }

    // Change the informations on the canva depending on the point of interest location
    public int UpdateInfos(Data data)
    {
        foreach (pages page in data.pages)
        {
            if(!pages.ContainsKey(page.pageid)) pages.Add(page.pageid, page);
        }

        foreach (string id in pages.Keys)
        {
            Button b = CreateButton(info_button, choice_buttons, cornerTopRight, cornerBottomLeft);
            b.GetComponent<TextMeshProUGUI>().text = pages[id].title;
            b.onClick.AddListener(delegate { ShowInfosOnClick(id); });
        }
        return 0;
    }

    public void ShowInfosOnClick(string id)
    {
        namePoint.text = pages[id].title;
        geographicInfos.text = "latitude : " + pages[id].lat + " | longitude : " + pages[id].lon;
        detailledInfos.text = pages[id].extract;
        info_interface.gameObject.SetActive(true);
    }

    private static Button CreateButton(Button buttonPrefab, Canvas canvas, Vector2 cornerTopRight, Vector2 cornerBottomLeft)
    {
        var button = Instantiate(buttonPrefab, Vector3.zero, Quaternion.identity);
        var rectTransform = button.GetComponent<RectTransform>();
        rectTransform.SetParent(canvas.transform);
        rectTransform.anchorMax = cornerTopRight;
        rectTransform.anchorMin = cornerBottomLeft;
        rectTransform.offsetMax = Vector2.zero;
        rectTransform.offsetMin = Vector2.zero;
        return button;
    }

    // Update is called once per frame
    void Update()
    {
        if (IsFacing() && IsNear())
        {
           // UpdateInfos();
        }
    }
}

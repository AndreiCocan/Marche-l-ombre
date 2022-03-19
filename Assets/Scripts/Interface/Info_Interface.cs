using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Info_Interface : MonoBehaviour
{
    //The differents buildings
    /* [SerializeField] private int nombreDeBatiments = 0;
     [SerializeField] private GameObject[] buildings;
     private GameObject bat;
     private float distanceDuBatiments = 0;*/

    //Variables for the vision of the camera
    private Vector3 direction;
    private float vu;

    //Variables to show the interface
    private GameObject RightHand;
    private GameObject vRInterface;
    private float armRotationX;


    // Start is called before the first frame update
    void Start()
    {
        RightHand = GameObject.Find("RightHand Controller");
        vRInterface = RightHand.transform.GetChild(0).GetChild(0).gameObject;
    }

    private bool IsFacing(GameObject batiment)
    {
        direction = (batiment.transform.position - transform.position).normalized;
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

    //If the user is near an interessting building the text corresponding to this building appear, if not reset guide;
    void UpdateInfos()
    {
        /* for (int i = 0; i < nombreDeBatiments; i++)
         {
             bat = GameObject.Find("bat" + i);
             distanceDuBatiments = Mathf.Sqrt(Mathf.Pow(bat.transform.position.x - transform.position.x, 2) + Mathf.Pow(bat.transform.position.y - transform.position.y, 2) + Mathf.Pow(bat.transform.position.z - transform.position.z, 2));
             if (distanceDuBatiments < 40)
             {
                 infos[i].text = "Guide :";
                 bat.transform.GetChild(0).gameObject.SetActive(true);

                 if (IsFacing(bat))
                 {
                     infosText = "This building is really interesting !";
                     infos[i].text = "Guide :" + infosText;
                 }
             }
             else
             {
                 infos[i].text = "Guide :";
                 bat.transform.GetChild(0).gameObject.SetActive(false);
             }

         }*/


    }

    // Update is called once per frame
    void Update()
    {
        UpdateInfos();
    }
}

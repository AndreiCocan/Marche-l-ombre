using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinimapPlayerIcon : MonoBehaviour
{
    Transform player;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("MainCamera").transform;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        //Rotate the player Icon to face the player forward
        transform.rotation = Quaternion.Euler(90,-90 , -100*player.rotation.y);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinimapPOIAllwaysFacingPlayer : MonoBehaviour
{
    Transform player;
    // Start is called before the first frame update
    void Start()
    {
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("PlayerIcon").GetComponent<Transform>();
        }
        Debug.Log(player.forward);
        Debug.Log(transform.forward);
    }
    

    // Update is called once per frame
    void LateUpdate()
    {
        transform.rotation = Quaternion.Euler(player.rotation.eulerAngles.x, player.rotation.eulerAngles.y+90, 0);


    }
}

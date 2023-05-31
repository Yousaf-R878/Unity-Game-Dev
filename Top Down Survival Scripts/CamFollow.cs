using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Overview:
//This script makes the camera follow the player
public class CamFollow : MonoBehaviour
{
    public GameObject player;

    // Update is called once per frame
    void Update()
    {
        //makes the camera follow the player around  
        Vector3 playerPos = player.transform.position;
        transform.position = new Vector3(playerPos.x, playerPos.y - 2.5f, -15f);
    }
}

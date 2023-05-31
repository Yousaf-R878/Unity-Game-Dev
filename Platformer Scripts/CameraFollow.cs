using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Overview:
//this makes the camera follow the player
public class CameraFollow : MonoBehaviour
{
    public GameObject player;

    // Update is called once per frame
    void Update()
    {
        //this is used to make the camera follow the player
        transform.position = new Vector3(player.transform.position.x, player.transform.position.y + 1, -10);
        
    }
}

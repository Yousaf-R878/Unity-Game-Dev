using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Overview:
//This makes the camera follow the player
public class CameraFollow : MonoBehaviour
{
    public GameObject player;

    // Update is called once per frame
    void Update()
    {
        //this sets the position of the camera behind and slightly above the player at all times
        //which allows the camera to follow the player
        float xPos = 0f;
        float yPos = 7.0f;
        float zPos = player.transform.position.z - 10;
        transform.position = new Vector3(xPos, yPos, zPos);
    }
}

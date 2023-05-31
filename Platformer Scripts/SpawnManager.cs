using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Overview:
//this script allows the player to shoot
public class SpawnManager : MonoBehaviour
{
    public GameObject pewpew;
    public GameObject gun;

    // Update is called once per frame
    void Update()
    {

        Vector3 gunPos = gun.transform.position;

        //if the player clicks the mouse button then the player will shoot
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Instantiate(pewpew, gunPos, gun.transform.rotation);
        }
    }
}
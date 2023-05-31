using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Overview:
//this script makes wall running possible
public class WallRunning : MonoBehaviour
{
    private Rigidbody playerRb;
    private Vector3 collisionPos;
    // Start is called before the first frame update
    void Start()
    {
        //gets the rigidbody component of the player
        playerRb = GetComponent<Rigidbody>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("WallRunL") || collision.gameObject.CompareTag("WallRunR"))
        {
            //this sets the position of the player when it makes contact with a wall running wall to some variable and 
            //then uses that variable in OnCollisionStay to keep the player at that position
            collisionPos = transform.position;
            
        }

    }
    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("WallRunL"))
        {
            transform.position = new Vector3(transform.position.x, collisionPos.y, transform.position.z);
        }
        if (collision.gameObject.CompareTag("WallRunR"))
        {
            transform.position = new Vector3(transform.position.x, collisionPos.y, transform.position.z);
        }
    }
}

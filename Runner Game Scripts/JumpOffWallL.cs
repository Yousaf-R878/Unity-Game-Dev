using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Overview: 
//this allows a player to jump off of walls that are on the left-hand side of the player 
//that the player can wall run on
public class JumpOffWallL : MonoBehaviour
{
    public GameObject player;
    private Rigidbody rigidB;
    public float wallForce = 15.0f;
    // Start is called before the first frame update
    void Start()
    {
        //dont need it - note to self
        rigidB = GetComponent<Rigidbody>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void OnTriggerStay(Collider other)
    {
        float rightForce = 12.5f;
        if (other.gameObject.CompareTag("Player"))
        {
            if (Input.GetKey(KeyCode.Space))
            {
                rigidB.AddForceAtPosition(Vector3.right * rightForce, player.transform.position, ForceMode.Impulse);
                rigidB.AddForceAtPosition(Vector3.up * wallForce, player.transform.position, ForceMode.Impulse);
            }
        }
    }
    //this is the same thing as the JumpOffWallR script howevever the only difference is the direction of the force which is added
    //to the right instead of the left
} 

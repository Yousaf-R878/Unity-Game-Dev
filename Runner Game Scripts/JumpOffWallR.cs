using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Overview: 
//this allows a player to jump off of walls that are on the right-hand side of the player 
//that the player can wall run on
public class JumpOffWallR : MonoBehaviour
{
    public GameObject player;
    private Rigidbody rigidB;
    public float wallForce = 15f;
    // Start is called before the first frame update
    void Start()
    {
        //dont need this - note to self
        //this gets a reference to physics on another object
        rigidB = GetComponent<Rigidbody>();
        //this finds an object with tag "Player" and then sets that object to the variable player
        //this allows you to access information about the player through a script
        player = GameObject.FindGameObjectWithTag("Player");

    }

    //This function is run whenever the player stays in collision with a wall running wall
    private void OnTriggerStay(Collider other)
    {
        float leftForce = 12.5f;
        if (other.gameObject.CompareTag("Player"))
        {
            if (Input.GetKey(KeyCode.Space))
            {
                // if the player presses the space key while in collision with a wall running wall
                // then the wall will add a force upwards and to the left at the players position
                rigidB.AddForceAtPosition(Vector3.left * leftForce, player.transform.position, ForceMode.Impulse);
                rigidB.AddForceAtPosition(Vector3.up * wallForce, player.transform.position, ForceMode.Impulse);
            }
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Overview: 
//This allows obstacles to kill the player
public class Obstacle : MonoBehaviour
{
    public GameObject Dead;
    public ParticleSystem dead;
    //This detects when the player collides with another object
    private void Start()
    {
        //storing information into variables
        Dead = GameObject.FindGameObjectWithTag("Dead");
        dead = Dead.GetComponent<ParticleSystem>();
    }

    //This detects when the player collides with another object
    private void OnTriggerEnter(Collider other)
    {
        //This if statement sees if the player collided with the obstacle
        if(other.gameObject.tag == "Player")
        {
            //When the player collides with the obstacle, an explosion effect is played and then the player dies
            dead.transform.position = other.transform.position;
            dead.Play();
            Destroy(other.gameObject);
        }
    }
}
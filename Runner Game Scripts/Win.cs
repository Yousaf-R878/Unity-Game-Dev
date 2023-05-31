using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Overview: 
//this script activates when the player beats a level
public class Win : MonoBehaviour
{

    //This will destroy the player if it reaches the end of the level and will output "You've beaten the lvl"
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Destroy(collision.gameObject);
            Debug.Log("You've beaten the lvl");
        }
    }
}

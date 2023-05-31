using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Overview:
//This script makes it so that the shooters are able to repeatedly fire at the player
public class ShootProjectile : MonoBehaviour
{
    public GameObject projectile;
    public float repeatRate = .5f;
    public float delay = .5f;
    // Start is called before the first frame update
    void Start()
    {
        /*this repeats a function x seconds after the start of the game and repeats the function after every y seconds
         * ie. it will call the function DuplicateObject .5 seconds after the start of the game and will then
         * call that function after every .5 seconds */
        InvokeRepeating("DuplicateObject", delay, repeatRate);
    }

    //when this function is called by the InvokeRepeating function, a projectile is spawned at the position of the shooter and then
    //rotated to fire in the direction the shooter is facing
    void DuplicateObject()
    {
        Vector3 projectilePos = transform.position;
        Instantiate(projectile, projectilePos, transform.rotation);
        
    }
}

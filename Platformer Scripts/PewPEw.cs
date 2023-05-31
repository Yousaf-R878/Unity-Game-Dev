using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Overview:
//this script makes bullets move when fired
public class PewPEw : MonoBehaviour
{
    public float speed = 10;

    // Update is called once per frame
    void Update()
    {
        //this is what makes the bullets move 
        transform.Translate(Vector3.forward * speed * Time.deltaTime);

        //this will destroy any player bullet that is outside of game world
        if(transform.position.x > 32.5 || transform.position.x < -32.5)
        {
            Destroy(gameObject);
        }
    }
    //if the bullets collide with something then they will destroy themselves
    private void OnCollisionEnter(Collision collision)
    {
        Destroy(gameObject);
    }
}

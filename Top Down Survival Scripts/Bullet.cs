using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Overview:
//this script allows bullets to move and kill enemies
public class Bullet : MonoBehaviour
{
    private float speed = 15f;
    public int killCount;

    // Whatever is in update will be executed once per frame
    void Update()
    {
        //this propels the object forward
        transform.Translate(Vector3.up * speed * Time.deltaTime);
    }
    //Sees if the bullet collides with an object of tag "Enemy"
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            //If the bullet collides with an enemy it will destroy the enemy
            Destroy(collision.gameObject);

        }
        //The bullet will then destroy itself after it destroys the enemy
        Destroy(gameObject);
    }
}


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Overview:
//this script makes boss bullets move
public class BossBullet : MonoBehaviour
{
    public float speed = 7.5f;

    // Update is called once per frame
    void Update()
    {
        //this is what makes the bullets move 
        transform.Translate(Vector3.right * speed * Time.deltaTime);
        //this will destroy any boss bullet that is outside of game world
        if (transform.position.x > 32.5 || transform.position.x < -32.5)
        {
            Destroy(gameObject);
        }
    }
  
}

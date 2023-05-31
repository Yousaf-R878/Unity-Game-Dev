using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Overview: 
//this sees if a projectile collides with something and if it does then it destroys that projectile
public class DestroyProjectile : MonoBehaviour
{

    private void OnCollisionEnter(Collision collision)
    {
        Destroy(gameObject);
    }
}

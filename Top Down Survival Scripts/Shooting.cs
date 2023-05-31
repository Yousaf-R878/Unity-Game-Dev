using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Overview:
//this allows the player to shoot
public class Shooting : MonoBehaviour
{
    public GameObject bulletPref;
    public GameObject firePoint;
    public bool canShoot = true;


    // Update is called once per frame
    void Update()
    {   
        //if the player clicks the mouse and canShoot is true then a bullet will be fired
        if (Input.GetKey(KeyCode.Mouse0) && canShoot)
        {
            //a bullet will the fired and then canShoot will be set to false so that the player can't shoot again
            Shoot();
            canShoot = false;
            //this is another coroutine which will add a cooldown for when the player can shoot a bullet again
            StartCoroutine(ShotCoolDown());
        }
    }
    //when this function is called, a bullet will be created at the position of the gun and will be rotated to fire in the direction
    //the player is facing
    void Shoot()
    {
        Instantiate(bulletPref, firePoint.transform.position, firePoint.transform.rotation);
    }
    //using ienumerator to add a shooting cool down
    IEnumerator ShotCoolDown()
    {
        yield return new WaitForSeconds(.35f);
        canShoot = true;
    }

    //**Fixes**
    //The bullets dont go to the mouse they just fire straight from the players rotation so make the gun also have a roationt so it shoots to the mouse
    //-note to self
}

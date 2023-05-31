using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Overview:
//This script makes the enemies follow the pl
public class Enemy : MonoBehaviour
{

    public GameObject player;
    public Rigidbody enemyRb;
    public GameObject bullet;
    public GameObject shotPoint;
    public float speed = 5.0f;
    public float angle;
    

    // Start is called before the first frame update
    void Start()
    {

        enemyRb = GetComponent<Rigidbody>();
        player = GameObject.FindGameObjectWithTag("Player");
        //InvokeRepeating("shoot", 2f, 2f); - if this was implemented then the enemies would shoot
        //however i decided not to implement this as it would've make the game too difficult 
    }

    // Update is called once per frame
    void Update()
    {
        //transform.Translate((player.transform.position - transform.position).normalized * speed * Time.deltaTime); - note to self/something that didnt work
        
        //this makes the enemies rotate to look at the player
        Vector3 lookDir = player.transform.position - transform.position;
        angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle);

        //this will move the enemy towards the direction it is looking
        enemyRb.MovePosition(transform.position + lookDir.normalized * speed * Time.deltaTime);

        //these are more things that i tried but didnt work
        //enemyRb.AddForce(lookDir.normalized * 1f, ForceMode.Force);

        //shoot();
        //StartCoroutine(Delay());
    }
    //This makes it so that the enemy destroys the player on collision
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Destroy(collision.gameObject);
        }
    }
    //this would be used to make the enemy shoot at the player
    /*
    void shoot()
    {
        bullet.transform.rotation = transform.rotation;        
        Instantiate(bullet, shotPoint.transform.position, Quaternion.Euler(0, 0, angle - 90f));

    }  */
    /*
     * this was something i tried to add delay for shooting but it didnt work 
     IEnumerator Delay()
     {
          yield return new WaitForSeconds(2f);
         Debug.Log("Coroutine");
     }
     */

}

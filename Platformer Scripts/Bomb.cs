using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Overview:
//this makes bombs damage the player
public class Bomb : MonoBehaviour
{
    public Rigidbody bombRB;
    public Transform player;
    public float force = 20f;
    public float speed;
    public float bombRadius = 1.25f;
    public LayerMask playerLayer;
    public int bombDamage = 45;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        //this will calculate how fast the bomb will need to move so that the bomb will reach the player when it hits the ground
        speed = (player.position.x - transform.position.x)/1.5f;
        //when a bomb spawns it is launched upwards
        bombRB.AddForce(Vector3.up * force, ForceMode.Impulse);

    }

    // Update is called once per frame
    void Update()
    {
        //this moves the bomb to the player
        transform.Translate(Vector3.right * speed * Time.deltaTime);

    }
    //this will see if the bomb collides with the platform
    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.CompareTag("Platform"))
        {
            //once the bomb hits the ground, if the player was within the radius of the bomb, it will damage the player by reducing 
            //the player's health by 45. Also, it will destroy itself when it hits the ground
            Collider[] bombHit = Physics.OverlapSphere(transform.position, bombRadius, playerLayer);
            Destroy(gameObject);
            foreach (Collider player in bombHit)
            {
                player.GetComponent<Playercontroler>().TakeDamageFromEnemy(bombDamage);
                Debug.Log(bombHit);
            }
        }
        
    }
    //this does the same thing as what happens in OnTriggerEnter, however the bomb is destroyed when it hits the player instead of 
    //when it hits the ground
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject);
            Collider[] bombHit = Physics.OverlapSphere(transform.position, bombRadius, playerLayer);
            foreach (Collider player in bombHit)
            {
                player.GetComponent<Playercontroler>().TakeDamageFromEnemy(bombDamage);
                Debug.Log(bombHit);
            }
        }
    }
    //this is a function that can be used to draw different shapes
    //here it is used to visualize the bomb's damage range
    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, bombRadius);
    }
}

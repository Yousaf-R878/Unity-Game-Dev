using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Overview: 
//this script allows the player to be controlled with the A and D keys or left and right arrow keys and the Spacebar
public class PlayerController : MonoBehaviour
{
    public float speed = 5.0f;
    public Rigidbody playerRb;
    public float force;
    public bool onGround;
    public Vector3 collisionPos;
    public ParticleSystem dead;
    public Vector3 vel;
    // Start is called before the first frame update
    void Start()
    {
        //this gets the players rigidbody component (holds information about physics for a particular object)
        playerRb = GetComponent<Rigidbody>();
        //stores the velocity of the player
        vel = playerRb.velocity;
    }

    // Update is called once per frame
    void Update()
    {

        //this gets input from the keyboard mainly the A, D, and left and right arrow keys and returns a -1 when
        // the A/ left arrow key is pressed and a 1 when the D/right arrow key is pressed
        float horizontalInput = Input.GetAxis("Horizontal");

        //this takes that horizontal input and applies it to move the player left and right depending on if the 
        //horizontalInput is -1 or 1
        transform.Translate(Vector3.right * horizontalInput * Time.deltaTime * speed);

        //this moves the player forward at a speed of "speed"
        transform.Translate(Vector3.forward * Time.deltaTime * speed);

        //this sees if the player is on the ground and if the player presses space, then an upward force will be added to the player
        //allowing the player to jump. This will also prevent the player from jumping while in the air
        if (onGround && Input.GetKey(KeyCode.Space))
        {
            onGround = false;
            playerRb.AddForce(Vector3.up * force, ForceMode.Impulse);

        }

    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Platform"))
        {
            //lets you not jump while in mid-air
            onGround = true;
            //makes sure the height of the jump is the same
            vel.y = 0;
            playerRb.velocity = vel;
        }
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            //allows you to jump on obstacles
            onGround = true;
            //makes sure the height of the jump is the same
            vel.y = 0;
            playerRb.velocity = vel;
        }
        //if the player collides with either a projectile or a spike, then the player will die and a death effect will play
        if (collision.gameObject.CompareTag("Projectile"))
        {
            dead.transform.position = transform.position;
            dead.Play();
            Destroy(gameObject);
        }
        if (collision.gameObject.CompareTag("Spike"))
        {
            dead.transform.position = transform.position;
            dead.Play();
            Destroy(gameObject);  
        }
    }
}    

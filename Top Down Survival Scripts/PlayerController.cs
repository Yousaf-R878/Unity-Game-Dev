using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Overview:
//this allows the player to be controlled with the W,S,A, and D keys
public class PlayerController : MonoBehaviour
{
    public float speed = 2.5f;
    public Rigidbody playerRb;
    Vector3 movement;
    public Vector3 mousePos;
    public Camera cam;
    public bool canDash = false;
    public bool isMoving;



    // Start is called before the first frame update
    void Start()
    {
        canDash = true;
    }

    // Update is called once per frame
    void Update()
    {
        //this gets input from the keyboard and stores it in variables
        movement.y = Input.GetAxisRaw("Vertical");
        movement.x = Input.GetAxisRaw("Horizontal");
        float zDepth = Vector3.Distance(cam.transform.position, transform.position);


        //only make depth the z axis bc if not this won't work - note to self
        //sets a variable, mousePos, to the position of the mouse in the x,y, and z. 
        //if i were to not include the ScreenToWorldPoint method, mousePos would contain coordinates based off the pixels in your screen
        //instead of coordinates that are relative to the game world. Without this method, getting the mouse's position wouldnt be possible
        mousePos = cam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, zDepth));

        //if the player is moving then the isMoving variable will be set to true 
        //if the player isnt moving then the variable will be set to false
        if (movement.x != 0 || movement.y != 0)
        {
            isMoving = true;
        }
        else
        {
            isMoving = false;
        }

        //this says that if the left shift key is pressed, the player is moving, and the dash ability cooldown is over,
        //then the player dashes
        if (Input.GetKeyDown(KeyCode.LeftShift) && isMoving && canDash)
        {
            //this will set canDash to false so the player can't dash again until the cooldown is over and will change the speed of the player
            //to a faster speed temporarily
            canDash = false;
            speed = 15f;
            //the way that I'm using coroutines is for adding delay
            //they take in an IEnumerator (see IEnumerator DashCoolDown and DashDuration)
            StartCoroutine(DashDuration());
            StartCoroutine(DashCoolDown());
        }

    }
    //this will be called several times per frame (similar to update)
    private void FixedUpdate()
    {
        //this is a different way of moving the player instead of using transform.translate
        playerRb.MovePosition(transform.position + movement.normalized * speed * Time.fixedDeltaTime);

        //this will rotate the player to the position of the mouse
        Vector3 lookDir = mousePos - transform.position;
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg;
        playerRb.rotation = Quaternion.Euler(0, 0, angle);
        

    }
    //once the player dashes and canDash is set to false there will be a 2 second cool down until canDash is set to true again
    //ie. this is the dash cooldown
    IEnumerator DashCoolDown()
    {
        yield return new WaitForSeconds(2f);
        canDash = true;  
    }
    //once the player dashes and the speed increases, there will be .2 seconds until the speed is set back to normal.
    //this determines how long the player's dash lasts
    IEnumerator DashDuration()
    {
        yield return new WaitForSeconds(.2f);
        speed = 5f;
    }


}
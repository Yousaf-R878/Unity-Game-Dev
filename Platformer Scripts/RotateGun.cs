using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Overview:
//this makes the player's gun rotate to point at the position of the player's mouse
public class RotateGun : MonoBehaviour
{
    Vector3 mousePos;
    public Camera cam;
    public GameObject player;
    public Rigidbody2D gunRb;
    // Start is called before the first frame update
    void Start()
    {
        gunRb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        //need to set this because if you keep this at 0 the world point youre trying to get is 0 distance from the cam -note to self
        float depth = Vector3.Distance(cam.transform.position, transform.position);
        Quaternion rotation = transform.rotation;
        
        //this is storing the current position of the mouse inside of a variable
        mousePos = cam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, depth));

        //this is making it so that the gun stays at the player's current position
        transform.position = new Vector3(player.transform.position.x, player.transform.position.y, -.7f);

        //this is something I tried out that didn't work

        //need to change the rotation as it goes greater than 90(note to self)
        /*if(transform.eulerAngles.z < 90)
        {
           
        }else
        {
            transform.rotation = Quaternion.Euler(transform.eulerAngles.x, transform.eulerAngles.y, 180f);
        }*/
        
    }
    private void FixedUpdate()
    {
        //this is used to rotate the gun so that it points at the mouse's position
        Vector2 lookDir = mousePos - player.transform.position;
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg;
        gunRb.rotation = angle;
    }
}

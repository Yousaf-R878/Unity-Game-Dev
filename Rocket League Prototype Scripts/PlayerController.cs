using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Rigidbody playerRb;
    public float speed = 5f;
    public float forwardInput;
    public float horizontalInput;
    public float jumpForce = 10f;
    public bool onGround;
    public bool hasSecondJump;
    float time;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        forwardInput = Input.GetAxis("Vertical");
        horizontalInput = Input.GetAxis("Horizontal");
        if (onGround)
            playerRb.AddRelativeForce(Vector3.forward * forwardInput * speed, ForceMode.Impulse);
            //transform.Translate(Vector3.forward * forwardInput * speed * Time.deltaTime);
        //if (onGround)
            //playerRb.useGravity = false;
        //else
            //playerRb.useGravity = true;
        if(playerRb.velocity.magnitude > 5)
        {
            Debug.Log("hello");
            Mathf.Clamp(playerRb.velocity.x, -5f, 5f);
            Mathf.Clamp(playerRb.velocity.y, -5, 5f);
        }
        if (!onGround)
        {
            //rotate in air
            //playerRb.AddRelativeTorque(Vector3.right, ForceMode.VelocityChange);
            playerRb.AddRelativeTorque(forwardInput * 2, horizontalInput * 1.5f, 0f, ForceMode.Acceleration);
            //playerRb.MoveRotation(new Quaternion(forwardInput, horizontalInput, transform.rotation.z, transform.rotation.w));
            if (Input.GetKey(KeyCode.E))
            {
                playerRb.AddRelativeTorque(0, 0 , -1.5f, ForceMode.Acceleration);
            }
            if (Input.GetKey(KeyCode.Q))
            {
                playerRb.AddRelativeTorque(0, 0, 1.5f, ForceMode.Acceleration);
            }
        }
        if (playerRb.velocity.magnitude > 0f && onGround)
            transform.Rotate(Vector3.up * horizontalInput * 35 * Time.deltaTime);
        else if (playerRb.velocity.magnitude < 0f && onGround)
            transform.Rotate(Vector3.up * horizontalInput * -35f * Time.deltaTime);

        if (Input.GetMouseButtonDown(1) && onGround)
        {
            playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            onGround = false;
            hasSecondJump = true;
            time = Time.time;           
        }
        if(Input.GetMouseButtonDown(1) && hasSecondJump && Input.GetKey(KeyCode.D) && !onGround && Time.time > time + .01)
        {
            playerRb.AddRelativeForce(Vector3.right * 12, ForceMode.Impulse);
            playerRb.AddRelativeTorque(Vector3.forward * -20, ForceMode.Impulse);
        }
        if (Input.GetMouseButtonDown(1) && hasSecondJump && Input.GetKey(KeyCode.A) && !onGround && Time.time > time + .01)
        {
            playerRb.AddRelativeForce(Vector3.left * 12, ForceMode.Impulse);
            playerRb.AddRelativeTorque(Vector3.forward * 20, ForceMode.Impulse);
        }
        if (Input.GetMouseButtonDown(1) && hasSecondJump && Input.GetKey(KeyCode.W) && !onGround && Time.time > time + .01)
        {
            playerRb.AddRelativeForce(Vector3.forward * 10, ForceMode.Impulse);
            playerRb.AddRelativeTorque(Vector3.right * 10, ForceMode.Impulse);
        }
        if (Input.GetMouseButtonDown(1) && hasSecondJump && Input.GetKey(KeyCode.S) && !onGround && Time.time > time + .01)
        {
            playerRb.AddRelativeForce(Vector3.back * 10, ForceMode.Impulse);
            playerRb.AddRelativeTorque(Vector3.right * -10, ForceMode.Impulse);
        }
        if (Input.GetMouseButtonDown(1) && hasSecondJump && !onGround && Time.time > time + .01 && !Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.S) && !Input.GetKey(KeyCode.D) && !Input.GetKey(KeyCode.W))
        {
            playerRb.AddForce(Vector3.up * .8f * jumpForce, ForceMode.Impulse);
            hasSecondJump = false;
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Platform"))
        {
            onGround = true;
        }
        playerRb.freezeRotation = false;
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Platform"))
        {
            onGround = false;
        }
    }
}

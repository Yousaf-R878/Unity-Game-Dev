using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Overview:
//this script makes the boss move to the player, shoot at the player, and launch its bomb attack
public class Boss : MonoBehaviour
{
    //References:
    public Rigidbody bossRb;
    public GameObject player;
    public GameObject bossBullet;
    public GameObject shotPoint;
    public Rigidbody2D shotPointRb;
    public GameObject bomb;

    //Movement:
    public float speed = 5.0f;

    //Stats:
    public int maxHealth = 300;
    public int currentHealth;


    // Start is called before the first frame update
    void Start()
    {
        //this will find the player and store it within a variable
        player = GameObject.FindGameObjectWithTag("Player");

        //these will store information about physics on certain objects
        bossRb = GetComponent<Rigidbody>();
        shotPointRb = shotPoint.GetComponent<Rigidbody2D>();
        
        //this sets the current health of the boss to the max health of the boss at the start of the game
        currentHealth = maxHealth;

        //this will make the boss shoot 1 second after the boss spawns and then every .8 seconds after that
        InvokeRepeating("DuplicateBullet", 1f, .8f);
        //this will spawn the bomb 2.5 seconds after a boss spawns and then every 3 seconds after that
        InvokeRepeating("DuplicateBombs", 2.5f, 3f);
    }

    // Update is called once per frame
    void Update()
    {
        //these 2 are what allow the boss to move to the player
        Vector3 moveVector = player.transform.position - transform.position;
        bossRb.MovePosition(transform.position + moveVector.normalized * speed * Time.deltaTime);

        //this will make the boss's bullet fire in the direction of the player
        Vector2 shotDir = player.transform.position - transform.position;
        float angle = Mathf.Atan2(shotDir.y, shotDir.x) * Mathf.Rad2Deg;
        shotPointRb.rotation = angle;

    }
    //this is the function that creates a bullet inside the boss and rotated towards the player 
    void DuplicateBullet()
    {
        Instantiate(bossBullet, shotPoint.transform.position, shotPoint.transform.rotation);
    }
    /*this is a function that when called, will damage the boss for x amout of damage
     * ie. TakeDamage(20); will reduce the boss's health by 20
     *also if the boss's health is less than or equal to 0, then the Die function will be called which destroys the boss
     */
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            Die();
        }
    }
    //this function destroys the boss when called 
    void Die()
    {
        Destroy(gameObject);
    }
    //whenever the player's bullet collides with the boss, the boss will lose 10 health.
    //if the bosses health is less than or equal to 0 then the boss will die, which destroys the boss
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            currentHealth -= 10;
            if(currentHealth <= 0)
            {
                Die();
            }
        }
    }
    //when this function is called, a bomb is created at the boss's position 
    void DuplicateBombs()
    {
        Instantiate(bomb, new Vector3(transform.position.x, -.2f, transform.position.z), Quaternion.identity);
    } 
}

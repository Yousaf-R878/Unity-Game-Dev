using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Overview:
//this allows the player to be controller with the A and D keys to move left and right respectively and jump with the space bar and
//allows the player to initiate a melee attack when the player presses E
public class Playercontroler : MonoBehaviour
{
    //references:
    public Rigidbody playerRb;
    public GameObject projSpawner;
    public GameObject Bullet;
    public Transform attackPoint;
    public LayerMask enemyLayer;
    public GameObject gun;
    //movement:
    public float speed = 5.0f;
    public float force = 100f;
    public bool onGround = true;
    //stats:

    public int maxHealthP = 100;
    public int currentHealthP;
    public float attackRangeP = 1f;
    int attackDamageP = 50;
    //vvv amount i can attack/second
    public float attackRateP = 1f;
    //vvv time when i can attack next
    float nextAttackTimeP = 0f;
    public bool isAlive = true;
    public bool canAttack;

    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        gun = GameObject.FindGameObjectWithTag("Gun");
        currentHealthP = maxHealthP;
    }

    // Update is called once per frame
    void Update()
    {
        //this will get input from the keyboard and store that input in a variable (used for movement)
        float xInput = Input.GetAxisRaw("Horizontal");

        //this is a vector which holds the player's velocities in the x,y and z axes
        Vector3 vel = playerRb.velocity;

        //this is what makes the player move with the a and d keys/ left and right arrow keys
        transform.Translate(Vector3.right * xInput * speed * Time.deltaTime);

        //this makes the player jump if the player presses the space key and the player is on the ground 
        //(so that the player can't jump while in mid-air)
        if (Input.GetKey(KeyCode.Space) && onGround)
        {
            Debug.Log("working");
            playerRb.AddForce(Vector3.up * force, ForceMode.Impulse);
            onGround = false;

        }
        //this makes the player accelerate downward while in the air
        if (!onGround)
        {
            vel.y -= 15 * Time.deltaTime;
            playerRb.velocity = vel;
        }
        //these 2 if-statements will rotate the player based on if the player is moving left or right
        //ie. if the player is moving to the left then the player will face the left direction
        // if the player is moving to the right then the player will face the right direction
        // there is an easier way of doing this which isnt as complicated, however i had found out about it after i had done this
        if (xInput == -1)
        {
            transform.rotation = Quaternion.Euler(0f, 180f, 0);
            speed = -5;
        }
        if (xInput == 1)
        {
            speed = 5;
            transform.rotation = new Quaternion(0, 0, 0, 0);
        }
        //this is the same concept as the enemy attacking 
        //another way of doing delay instead of using a coroutine
        if (Time.time >= nextAttackTimeP)
        {
            canAttack = true;
            //sees if current time is >= next time when we can attack
            if (Input.GetKeyDown(KeyCode.E))
            {
                Attack();
                canAttack = false;
                nextAttackTimeP = Time.time + 1f / attackRateP;
                //sets the next attack time to our current time plus the amount we can attack per second which is 1 so
                //it adds one second to our next attack time
            }
        }
    }
    //this sets onGround to true if the player collides with the platform or an obstacle
    //(this is used so that the player can jump when on the ground)
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Platform"))
        {
            onGround = true;
        }
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            onGround = true;
        }
    }
    //when this function is called, all enemies within a certain range of the player will take damage
    void Attack()
    {
        //find enemies in range
        Collider[] hitEnemies = Physics.OverlapSphere(attackPoint.position, attackRangeP, enemyLayer);

        //damage them
        foreach (Collider enemy in hitEnemies)
        {
            enemy.GetComponent<Enemy>().TakeDamage(attackDamageP);
        }

    }
    //this is used to see where the player's attack range is
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(attackPoint.position, attackRangeP);
    }
    //when this function is called, the player will be damaged for x amount of damage
    //ie. TakeDamageFromEnemy(25) will reduce the player's health by 25
    public void TakeDamageFromEnemy(int damageE)
    {
        currentHealthP -= damageE;
        //when the player's health is less than or equal to 0, the Die function will be called which will destroy the player
        if (currentHealthP <= 0)
        {
            Die();
        }
    }
    //when this function is called, the player and the player's gun will be destroyed. 
    void Die()
    {
        Destroy(gameObject);
        Destroy(gun);
        isAlive = false;
    }
    private void OnTriggerEnter(Collider other)
    {
        //if the player gets hit by the boss's bullet, then the player will take 10 damage
        //if the player's health is less than or equal to 0 then the die function will be called 
        if (other.gameObject.CompareTag("Boss Bullet"))
        {
            Destroy(other.gameObject);
            currentHealthP -= 10;
            if (currentHealthP <= 0)
            {
                Die();
            }
        }
        //if the player touches a health pack then the player will gain 40 health,
        //however if the player will have more than 100 health after grabbing a health pack, then the
        //player's health will cap out at 100
        if (other.gameObject.CompareTag("Health Pack"))
        {
            if (currentHealthP + 40 > 100)
            {
                currentHealthP = 100;
            }
            else
            {
                currentHealthP += 40;
            }
            //destroys health pack
            Destroy(other.gameObject);
        }
    }   


}
//-notes to self
//add delay to melee combat | X
//switching weapons(may or may not be possible with time)
//UI related: add health bar (probably not possible with time constraints)
//
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Overview:
//this script makes the enemies move to the player and attack the player when close enough
public class Enemy : MonoBehaviour
{
    //References
    public Rigidbody enemyRb;
    public GameObject player;
    public Transform attackPointE;
    public LayerMask playerLayer;
    //public GameObject attackPE;-note to self
      
    //movement

    public float speed = 7.5f;
    public float jumpforce = 1000f;
    public bool onGroundE;
    Vector3 vel;
    
    //Stats

    public int maxHealthE = 100;
    int currentHealthE;
    public int attackDamageE = 20;
    public float attackRangeE = 1f;
    public float attackRateE = 1f;
    float nextAttackTimeE = 0;

    // Start is called before the first frame update
    void Start()
    {
        //this will store information about physics on the enemy
        enemyRb = GetComponent<Rigidbody>();

        //this will find the player and store its information within a variable
        player = GameObject.FindGameObjectWithTag("Player");

        //this will set the current health to the max health at the start of the game
        currentHealthE = maxHealthE;
    }

    // Update is called once per frame
    void Update()
    {
        //this is used to make the enemy move to the player
        Vector3 lookDir = player.transform.position - transform.position;

        //this stores the velocity of the enemy
        vel = enemyRb.velocity;

        //this moves the enemy to the player
        enemyRb.MovePosition(transform.position + lookDir.normalized * speed * Time.deltaTime);

        /*not added to game
        //this makes it so that the enemy would jump a second after the player jumps,
        //however this isn't needed. I was going to use it for when I added obstacles but I would want to add some more things 
        //before adding them
        if (Input.GetKeyDown(KeyCode.Space) && onGroundE)
        {
            onGroundE = false;
            StartCoroutine(JumpDelay());
        }
        */

        //this accelerates the object downwards when in the air  
        if (!onGroundE)
        {
            vel.y -= 100f * Time.deltaTime;
        }

         //this makes the enemy attack once per second
        if(Time.time >= nextAttackTimeE)
        {
            EnemyAttack();
            nextAttackTimeE = Time.time + 1 / attackRateE; 
        }

        //this stops this entire script from running if the player dies
        if(!player.GetComponent<Playercontroler>().isAlive)
        {
            this.enabled = false;
        }
        
    }
    /*
    //this was used to make the enemy jump after the player would jump
    IEnumerator JumpDelay()
    {
        Debug.Log("coroutine");
        yield return new WaitForSeconds(.5f);
        Debug.Log("post time");
        enemyRb.AddForce(Vector3.up * jumpforce, ForceMode.Impulse);
    }
    */

    //this will reduce the enemy's health by 10 if the player's bullet hits it, and if the enemy's health is less than or equal to 0, then the 
    //die function will be called (it will destroy that enemy)
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            currentHealthE -= 10;
            if (currentHealthE <= 0)
            {
                Die();
            }
        }
        //this sees if the enemy is on the ground, then onGroundE will be true
        /*
         * -going to be implemented later
        //it's used so that the enemy doesn't jump while in mid air
        if (collision.gameObject.CompareTag("Platform"))
        {
            onGroundE = true;
        }
        */
    }
    /*this is a function that when called, will damage the enemy for x amout of damage
     * ie.TakeDamage(20); will reduce the enemy's health by 20
     *also if the enemy's health is less than or equal to 0 then the Die function will be called which destroys the enemy
     */
    public void TakeDamage(int damage)
    {
        currentHealthE -= damage;

        if (currentHealthE <= 0)
        {
            Die();
        }
    }
    //this will destroy the enemy when this function is called
    void Die()
    {
        //this.enabled = false; <-- use to disable scipt very useful - note to self
        Destroy(gameObject);
    }
    //when this function is called, it will attack the player if the player is within a certain range from the enemy
    void EnemyAttack()
    {
        //find the player
        Collider[] hitPlayer = Physics.OverlapSphere(attackPointE.position, attackRangeE, playerLayer);
        //attack the player
        foreach(Collider player in hitPlayer)
        {
            player.GetComponent<Playercontroler>().TakeDamageFromEnemy(attackDamageE);
        }
    }
    //this is a function that can be used to draw different shapes
    //here it is used to visualize the enemy's attack range
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(attackPointE.position, attackRangeE);
    }

}
//-notes to self
// add health | X
// decrease health when u get shot | X
// make enemy move to player| X
// make enemy melee player | X
// add head, body, and legs to enemy all recieving different damage from the bullet of the player | inc (not possible atm with time constraints)

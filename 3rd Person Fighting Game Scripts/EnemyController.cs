using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    NavMeshAgent agent;
    public GameObject player;
    public Vector3 spawnLocation;
    public Camera cam;

    Transform target;
    public Transform firepoint;

    //magic:
    public float fireballRate = .25f;
    public float nextFireBallTime = 0f;

    public int level = 1;
    public float maxHealth = 100f;
    public float currentHealth = 100f;
    public float speed;
    public bool isRunning;
    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        spawnLocation = transform.position;
        cam = Camera.main;
    }
    // Start is called before the first frame update
    void Start()
    {
        transform.position = spawnLocation;
        agent = gameObject.GetComponent<NavMeshAgent>();
        maxHealth = 100 * level;
        currentHealth = 100 * level;
    }

    // Update is called once per frame
    void Update()
    {
        /*if(target != null)
        {
            agent.SetDestination(target.position);
        }*/
        if((player.transform.position - spawnLocation).magnitude <= 20f)
        {
            moveToTarget(player.transform.position);
            if(Time.time >= nextFireBallTime)
            {
                agent.transform.LookAt(player.transform.position);
                FindObjectOfType<FireMagic>().FireBall(firepoint.position,transform.rotation);

                nextFireBallTime = Time.time + 1f / fireballRate;
            }
            if (FindObjectOfType<ThirdPersonMovement>().isRunning)
            {
                agent.speed = 6f;
            }
            else
            {
                agent.speed = 3f;
            }
        }
        else if ((player.transform.position - spawnLocation).magnitude >= 20f)
        {
            moveToTarget(spawnLocation);
            agent.stoppingDistance = 0f;
        }
    }
    public void moveToTarget(Vector3 position)
    {
        agent.stoppingDistance = 2f;
        agent.SetDestination(position);

        
        //target.position = position;
    }
    public void stopMovingToTarget()
    {
        // target = null;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("FireBall"))
        {
            Debug.Log("hit");
            currentHealth -= LevelSystem.level * 5 + 25f;
            if(currentHealth <= 0)
            {
                Destroy(gameObject);
            }
        }
    }
    public void TakeDamage(int damageE)
    {
        Debug.Log("taking damage");
        currentHealth -= damageE;
        //when the player's health is less than or equal to 0, the Die function will be called which will destroy the player
        if (currentHealth <= 0)
        {
            Destroy(gameObject);
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonMovement : MonoBehaviour
{
    public CharacterController controller;
    public Transform cam;
    public Camera kam;

    //movement:
    public float speed = 5f;
    public float gravity = -9.81f;
    public float jumpHeight = 2f;
    public float stamina = 100;
    public float maxStamina = 100;
    public bool isRunning = false;

    //Physics:
    Vector3 velocity;
    public Transform groundCheck;
    public float groundDistance = 0.1f;
    public LayerMask groundMask;
    public Quaternion currentRotation;
    public float currentHeight;

    public Vector3 mousePos;
    Rigidbody playerRb;
    public bool isGrounded;

    //Magic:
    public FireMagic magic;
    public float fireballRate = .25f;
    public float nextFireBallTime = 0f;
    public float firePillarRate = .125f;
    public float nextPillarTime = 0f;

    //Melee:
    public float attackRateP = .25f;
    public float nextAttackTimeP = 0f;
    int attackDamageP = 10;
    float attackRange = 2;
    public LayerMask enemyLayer;

    public float currentHealth = 100f;
    public float maxHealth = 100f;
    float nextHealTime = 0f;
    float healingRate = 1;

    public Transform firepoint;
    public Collider[] hitEnemies;
    public Tutorial tutorial;
    public GameObject sword;

    // Update is called once per frame
    private void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        kam = Camera.main;
    }
    private void FixedUpdate()
    {
        if ((currentHealth < maxHealth) && Time.time >= nextHealTime)
        {
            currentHealth += maxHealth * .02f;
            nextHealTime = Time.time + 1 / healingRate;
        }
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
    }
    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(horizontal, 0, vertical).normalized;

        if (Input.GetKeyDown(KeyCode.LeftControl))
            isRunning = !isRunning;
        if (isRunning && !(stamina < 0))
        {
            speed = 7.5f;
            if (horizontal != 0 || vertical != 0)
                stamina -= 20 * Time.deltaTime;
            else if(stamina <= maxStamina)
                stamina += 10 * Time.deltaTime;
        }
        else
        {
            isRunning = false;
            speed = 5f;
        }
        if (!isRunning && !(stamina >= maxStamina))
            stamina += 15 * Time.deltaTime;
        if (Input.GetKey(KeyCode.T) && isGrounded && stamina - 17.5 > 0)
            speed = 2f;
        if (Input.GetKeyUp(KeyCode.T) && isGrounded && stamina - 17.5 > 0)
        {
            stamina -= maxStamina * .175f;
            velocity.y = Mathf.Sqrt(8 * -2f * gravity);
            speed = 5f;
        }

        if (direction.magnitude >= .1f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            transform.rotation = Quaternion.Euler(0f, targetAngle, 0f);

            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            controller.Move(moveDir.normalized * speed * Time.deltaTime);
        }
        if (Input.GetButton("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
        if (Input.GetKeyDown(KeyCode.Q) && Time.time >= nextFireBallTime)
        {
            currentRotation = transform.rotation;
            currentHeight = transform.position.y;
        }
        if (Input.GetKey(KeyCode.Q) && Time.time >= nextFireBallTime)
        {
            Ray ray = kam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 100))
            {
                horizontal = 0f;
                vertical = 0f;
                transform.position = new Vector3(transform.position.x, currentHeight, transform.position.z);
                transform.LookAt(hit.point);
                velocity.y = 0f;
            }
        }
        if (Input.GetKeyUp(KeyCode.Q) && Time.time >= nextFireBallTime)
        {
            float timeUp = Time.time + 2;
            magic.FireBall(firepoint.position, transform.rotation);
            LevelSystem.currentXP += 2.5f;
            nextFireBallTime = Time.time + 1f / fireballRate;
            transform.rotation = currentRotation;
            //while (Time.time < timeUp)
            //{

            //}
        }
        if (Input.GetKeyDown(KeyCode.R) && Time.time >= nextPillarTime)
        {
            currentHeight = transform.position.y;
        }
        if (Input.GetKey(KeyCode.R) && Time.time >= nextPillarTime)
        {
            horizontal = 0f;
            vertical = 0f;
            transform.position = new Vector3(transform.position.x, currentHeight, transform.position.z);
            velocity.y = 0f;
        }
        if (Input.GetKeyUp(KeyCode.R) && Time.time >= nextPillarTime)
        {
            nextPillarTime = Time.time + 1f / firePillarRate;
            magic.FirePillar();
            hitEnemies = Physics.OverlapSphere(transform.position, 5, enemyLayer);

            //damage them
            foreach (Collider enemy in hitEnemies)
            {
                //enemy.GetComponent<EnemyController>().TakeDamage(attackDamageP);
                enemy.GetComponentInParent<EnemyController>().TakeDamage(30);
            }
        }
        if (Input.GetKeyDown(KeyCode.X))
        {
            currentRotation = transform.rotation;
            currentHeight = transform.position.y;
        }
        if (Input.GetKey(KeyCode.X))
        {
            Ray ray = kam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 100))
            {
                horizontal = 0f;
                vertical = 0f;
                transform.position = new Vector3(transform.position.x, currentHeight, transform.position.z);
                transform.LookAt(hit.point);
                velocity.y = 0f;
            }
        }
        if (Time.time >= nextAttackTimeP)
        {
            sword.transform.localRotation = Quaternion.Euler(30, 0f, 0f);
            //sees if current time is >= next time when we can attack
            if (Input.GetMouseButtonDown(0))
            {
                sword.transform.Rotate(Vector3.right * 60);
                Attack();
                LevelSystem.currentXP += .5f;
                nextAttackTimeP = Time.time + .5f;
            }
        }
        if (Input.GetKeyUp(KeyCode.X))
        {
            int count = 0;
            float nextShootingTime = Time.time;
            float shootingRate = 2f;
            while (count != 10)
            {
                Ray ray = kam.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit, 100))
                {
                    horizontal = 0f;
                    vertical = 0f;
                    transform.position = new Vector3(transform.position.x, currentHeight, transform.position.z);
                    transform.LookAt(hit.point);
                    velocity.y = 0f;
                }
                if (Time.time >= nextShootingTime)
                {
                    count++;
                    magic.FireBall(firepoint.position, transform.rotation);
                    nextShootingTime = nextShootingTime + 1 / shootingRate;
                }
            }
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(groundCheck.position, groundDistance);
        Gizmos.DrawWireSphere(transform.position, 5);
        Gizmos.DrawWireSphere(firepoint.position, attackRange);
    }
    public void Attack()
    {
        //find enemies in range
        hitEnemies = Physics.OverlapSphere(firepoint.position, attackRange, enemyLayer);

        //damage them
        foreach (Collider enemy in hitEnemies)
        {
            //enemy.GetComponent<EnemyController>().TakeDamage(attackDamageP);
            enemy.GetComponentInParent<EnemyController>().TakeDamage(attackDamageP);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBall : MonoBehaviour
{
    public float speed = 15f;
    float time;
    public GameObject player;
    public LayerMask enemyLayer;
    private void Awake()
    {
        time = Time.time;
        player = GameObject.FindGameObjectWithTag("Player");
    }
    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);

        if (Time.time > time + 8f)
        {
            Destroy(gameObject);
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        Collider[] hitEnemies = Physics.OverlapSphere(transform.position, .4f, enemyLayer);

        //damage them
        foreach (Collider enemy in hitEnemies)
        {
            //enemy.GetComponent<EnemyController>().TakeDamage(attackDamageP);
            enemy.GetComponentInParent<EnemyController>().TakeDamage(45);
        }
        Destroy(gameObject);
        if (collision.gameObject.CompareTag("Player"))
        {
            ThirdPersonMovement thing = player.GetComponent<ThirdPersonMovement>();
            thing.currentHealth -= 50;

            if(thing.currentHealth <= 0)
            {
                Destroy(collision.gameObject);
            }
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, .4f);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Overview:
//this script makes enemies spawn
public class EnemySpawner : MonoBehaviour
{
    public Transform player;
    public GameObject enemy;
    public GameObject boss;
    public int count;
    Vector3 randomPos;
    public GameObject[] bosses;
    public float nextEnemySpawn = 0;
    // Start is called before the first frame update
    void Start()
    {
        count = 1;

    }

    // Update is called once per frame
    void Update()
    {
        //spawn these enemies in start using coroutines and lists - note to self
        float randomX = Random.Range(-20, 20);
        randomPos = new Vector3(randomX, 0f, -.25f);
        bosses = GameObject.FindGameObjectsWithTag("Boss");

        //this makes an enemy spawn every 5.5 seconds
        if ((Time.time >= nextEnemySpawn) && !(count%5 == 0) && count >= 0)
        {
            Instantiate(enemy, randomPos, Quaternion.identity);
            nextEnemySpawn = Time.time + 5.5f;
            count++;
            
        }
        //after 4 enemies have spawned, a boss will be spawned 
        //this also makes it so that no enemies spawn while the boss is alive
        if(count %5 == 0 && (Time.time >= nextEnemySpawn))
        {
            Instantiate(boss, randomPos, Quaternion.identity);
            bosses = GameObject.FindGameObjectsWithTag("Boss");
            count = -1;
        }
        //if the boss dies, then count will be set to 1 which will then allow normal enemies to spawn again
        if(count == -1)
        {
            if(bosses.Length == 0)
            {
                count = 1;
            }
        }

    }   
}

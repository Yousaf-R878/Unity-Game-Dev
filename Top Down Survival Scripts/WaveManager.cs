using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Overview:
//This script is responsible for making enemies spawn
public class WaveManager : MonoBehaviour
{
    public int waveNum;
    public GameObject enemyPrefab;
    public GameObject player;
    public GameObject[] enemies;
    public Vector3 randomPos;
    // Start is called before the first frame update
    void Start()
    {
        waveNum = 0;
    }

    // Update is called once per frame
    void Update()
    {
        //this creates a list of all gameobjects with a tag of "Enemy"
        enemies = GameObject.FindGameObjectsWithTag("Enemy");

        //if the list of enemies is empty then a new wave will start with 1 more enemy than the previous wave
        if (enemies.Length == 0)
        {

            waveNum += 1;
            MakeEnemies(waveNum);

        }

        //this is a function that will spawn enemies 
        void MakeEnemies(int Waves)
        {
            for (int wave = Waves; wave > 0; wave--)
            {
                //these are just variables
                Vector3 playerPos = player.transform.position;
                float rangeX = Random.Range(playerPos.x - 10f, playerPos.x + 10f);
                float rangeY = Random.Range(playerPos.y - 10f, playerPos.y + 10f);

                //makes it so the enemies dont spawn too close to u
                if(rangeX - playerPos.x < 5f && rangeX - playerPos.x > 0)
                {
                    rangeX += 8f;
                }else if(rangeX - playerPos.x > -5f && rangeX - playerPos.x < 0)
                {
                    rangeX -= 8f;
                }
                if (rangeY - playerPos.y < 5f && rangeY - playerPos.y > 0)
                {
                    rangeY += 8f;
                }
                else if (rangeY - playerPos.y > -5f && rangeY - playerPos.y < 0)
                {
                    rangeY -= 8f;
                }
                randomPos = new Vector3(rangeX, rangeY, -1);

                //this is what duplicates the enemy at a random position that isnt too close to the player with the rotation of the enemy object
                Instantiate(enemyPrefab, randomPos, Quaternion.identity);
            }    
        }

    }
}

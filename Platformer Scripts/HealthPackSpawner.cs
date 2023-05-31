using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Overview:
//this script makes health packs spawn
public class HealthPackSpawner : MonoBehaviour
{
    Vector3 randomPos;
    public GameObject healthPack;

    // Start is called before the first frame update
    void Start()
    {
        //this makes a health pack spawn 5 seconds after the start of the game and then every 11.5 seconds every time after that 
        InvokeRepeating("DuplicateHealthPack", 5, 11.5f);
    }

    // Update is called once per frame
    void Update()
    {
        //this will hold a position (x,y,z) with x being a random value from -22.5 to 22.5, y being 0, and z being -.5
        randomPos = new Vector3(Random.Range(-22.5f, 22.5f), 0, -.5f);
    }
    //when this function is called, a healthpack is duplicated at a random position which was stored in randomPos
    void DuplicateHealthPack()
    {
        Instantiate(healthPack, randomPos, Quaternion.identity);
    }
}

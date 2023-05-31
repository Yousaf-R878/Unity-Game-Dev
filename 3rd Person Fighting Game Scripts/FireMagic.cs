using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireMagic : MonoBehaviour
{
    public Transform firePoint;
    public Transform player;
    public GameObject playerObj;
    public GameObject fireBall;
    public GameObject firePillar;
    private void Update()
    {
        /*
        if (Input.GetKeyUp(KeyCode.Q))
        {
            FireBall();
        }
        if (Input.GetKeyUp(KeyCode.R))
        {
            FirePillar();
        }
        */
    }
    public void FireBall(Vector3 pos, Quaternion rot)
    {
        Instantiate(fireBall, pos, rot);
    }
    public void FirePillar()
    {
        Instantiate(firePillar, player.position + new Vector3(0f,1.5f,0f), Quaternion.identity);
        Collider[] hitEnemies = Physics.OverlapSphere(player.position, 5);
        LevelSystem.currentXP += 6f;
    }
    public void FireStorm()
    {
        float fireRate = .2f;
        float nextFireTime = 0f;
        for(int i = 0; i <= 15; i++)
        {
            if(Time.time >= nextFireTime)
            {
                Instantiate(fireBall, firePoint.position, player.rotation);
                nextFireTime = nextFireTime + 1f / fireRate;
            }
        }
    }
}

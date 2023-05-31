using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Overview:
//this script allows the player's health to be displayed as well as whether or not the player can do a melee attack
public class DisplayStuff : MonoBehaviour
{
    public Playercontroler player;
    public Text healthText;
    public Text canAttackText;

    // Update is called once per frame
    void Update()
    {   //This displays the player's health and whether or not the player can melee attack
        healthText.text = "Health = " + player.currentHealthP.ToString();
        canAttackText.text = "Can Attack = " + player.canAttack.ToString();
    }
}

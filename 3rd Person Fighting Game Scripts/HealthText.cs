using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthText : MonoBehaviour
{
    public Text healthText;
    public ThirdPersonMovement player;
    // Update is called once per frame
    void Update()
    {
        healthText.text = "Health: " + player.currentHealth + "/" + player.maxHealth;
    }
}

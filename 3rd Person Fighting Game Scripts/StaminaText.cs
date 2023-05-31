using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StaminaText : MonoBehaviour
{
    public Text staminaText;
    public ThirdPersonMovement player;
    // Update is called once per frame
    void Update()
    {
        staminaText.text = "Stamina: " + player.stamina.ToString("0") + "/" + player.maxStamina;
    }
}

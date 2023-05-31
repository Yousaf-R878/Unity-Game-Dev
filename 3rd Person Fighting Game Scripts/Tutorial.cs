using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tutorial : MonoBehaviour
{
    public GameObject tutorialUI;
    public Text tutorialText;
    public void Sprint()
    {
        tutorialText.text = "Press Ctrl to toggle sprint. Doing so consumes your stamina over time";
    }
    public void HighJump()
    {
        tutorialText.text = "Press T to perform a high jump that consumes stamina.";
    }
    public void Fight()
    {
        tutorialText.text = "Look out! There are enemies ahead. You can use the left mouse button to attack with your sword.";
        Invoke("ChangeText", 3.5f);
    }

    void ChangeText()
    {
        tutorialText.text = "You can also use magic to fight enemies. Hover your mouse over an enemy and press q to fire a fireball in that direction.";
        Invoke("ChangeAgain", 3.5f);
    }
    void ChangeAgain()
    {
        tutorialText.text = "Press R to use FirePillar, an attack that discharges flames dealing damage to surrounding players.";
        Invoke("TutorialComplete", 3.5f);
    }
    void TutorialComplete()
    {
        tutorialUI.SetActive(false);
    }
}

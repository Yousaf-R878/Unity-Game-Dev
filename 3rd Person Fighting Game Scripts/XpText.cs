using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class XpText : MonoBehaviour
{
    public Text xpText;
    public LevelSystem system;
    // Update is called once per frame
    void Update()
    {
        xpText.text = "Experience: " + system.getCurrentXP() + "/" + system.getMaxXP();
    }
}

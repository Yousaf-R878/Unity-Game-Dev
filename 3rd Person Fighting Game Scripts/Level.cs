using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Level : MonoBehaviour
{
    public Text level;
    public LevelSystem system;
    // Update is called once per frame
    void Update()
    {
        level.text = "Level: " + system.returnLevel();
    }
}

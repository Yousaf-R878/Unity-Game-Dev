using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSystem : MonoBehaviour
{
    public ThirdPersonMovement player;

    public static int level = 1;
    public static float currentXP = 0f;
    public static float maxXP = 10f;
    [SerializeField] private int _level = level;
    [SerializeField] private float _currentXP = currentXP;
    [SerializeField] private float _maxXP = maxXP;
    
    void FixedUpdate()
    {
        _level = level;
        _currentXP = currentXP;
        _maxXP = maxXP;
        if (currentXP >= maxXP)
        {
            currentXP = currentXP - maxXP;
            level++;
            maxXP *= 1.6f;
            player.maxHealth += 25 * level;
        }
    }
    public int returnLevel()
    {
        return level;
    }
    public float getCurrentXP()
    {
        return currentXP;
    }
    public float getMaxXP()
    {
        return maxXP;
    }
}

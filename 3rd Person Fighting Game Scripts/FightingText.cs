using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightingText : MonoBehaviour
{
    public Tutorial tutorial;
    private void OnTriggerEnter(Collider other)
    {
        tutorial.Fight();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpingText : MonoBehaviour
{
    public Tutorial tutorial;
    private void OnTriggerEnter(Collider other)
    {
        tutorial.HighJump();
    }
}

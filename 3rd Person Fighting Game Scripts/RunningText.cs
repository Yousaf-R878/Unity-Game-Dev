using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunningText : MonoBehaviour
{
    public Tutorial tutorial;
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Hello");
        tutorial.Sprint();
    }
}

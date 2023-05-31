using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Score : MonoBehaviour
{

    public Text scoreText;
    public PlayerController playerCont;
    public GameObject player;

    // Start is called before the first frame update
    void Start()
    {

        playerCont = player.GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        //scoreText.text = playerCont.score.ToString();
        //Debug.Log(playerCont.score);
    }
}

using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using Unity.Collections;

public class CanvasTextCoinCount : MonoBehaviour
{
    public Text coinText; 
    public Player player; 

    void Start()
    {
        
        if (coinText == null)
        {
            coinText = GetComponent<Text>();
        }

        
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        }
    }

    void Update()
    {
        
        coinText.text = "COINS: " + player.coinCount;
    }
}


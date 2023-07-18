using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    public int scoreGive = 5;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            AudioManager.instance.PlayCoinSound();
            UIManager.instance.updateScore(scoreGive);
            GameManager.instance.addScore(scoreGive);
            gameObject.SetActive(false);
        }
    }
    
}

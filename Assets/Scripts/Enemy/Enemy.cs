using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private GameObject explosionPrefab;
    
    public int scoreGive = 8;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            AudioManager.instance.PlayExplosionSound();
            UIManager.instance.updateScore(scoreGive);
            GameManager.instance.addScore(scoreGive);
            collision.gameObject.SetActive(false);
            Instantiate(explosionPrefab, transform.position, Quaternion.identity);
            Deactive();
        }
    }

    private void Deactive()
    {
        gameObject.SetActive(false);
    }
}

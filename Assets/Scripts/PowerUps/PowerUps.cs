using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUps : MonoBehaviour
{
    private float speed = 0.5f;
    [SerializeField]
    private int powerUpID; 

    private void Update()
    {
        transform.Translate(Vector3.down * speed * Time.deltaTime);
        if (transform.position.y < -6)
            gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            AudioManager.instance.PlayPowerUpSound();
            PlayerController player = collision.GetComponent<PlayerController>();
            if (player != null)
            {
                switch (powerUpID)
                {
                    case 0:
                        player.enableShield();
                        break;
                    case 1:
                        player.enableTripleShot();
                        break;
                    default:
                        Debug.Log("Default Value");
                        break;
                }
            }
            if (this.gameObject != null)
                gameObject.SetActive(false);
        }
    }
}

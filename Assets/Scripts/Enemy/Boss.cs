using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    [SerializeField]
    private HealthBar healthBar;
    
    [SerializeField]
    private GameObject explosionPrefab;

    [SerializeField] 
    private GameObject miniExplosion;
    
    [SerializeField]
    private float maxHealth;
    private float health;
    
    private Animator animator;

    private float shootInterval = 4f;
    private float elapsedTime;


    private void Start()
    {
        animator = GetComponent<Animator>();

        health = maxHealth;
        healthBar.UpdateHealthBar(maxHealth, health);
    }

    
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            StartCoroutine(GetDamage());
            collision.gameObject.SetActive(false);
        }
    }
    

    private void Update()
    {
        {
            UpdateShootTimer();
        }
    }

    private void UpdateShootTimer()
    {
        elapsedTime += Time.deltaTime;
        if (elapsedTime >= shootInterval)
        {
            elapsedTime = 0f;
            Shoot();
        }
    }

    IEnumerator GetDamage()
    {
        float miniExplosionDuartion = 0.1f;
        float damage = 1;
        health -= damage;
        healthBar.UpdateHealthBar(maxHealth, health);

        if (health > 0)
        {
            AudioManager.instance.PlayExplosionSound();
            GameObject miniExplosionInstance = Instantiate(miniExplosion, transform.position, Quaternion.identity);
            yield return new WaitForSeconds(miniExplosionDuartion); 
            Destroy(miniExplosionInstance);
        }
        else
        {
            AudioManager.instance.PlayExplosionSound();
            Instantiate(explosionPrefab, transform.position, Quaternion.identity);
            gameObject.SetActive(false);
        }
    }

    public void Shoot() 
    {
        GameObject obj = ObjectPooler.instance.GetPoolObject("BossBullet");
        obj.transform.position = transform.position;
        obj.transform.rotation = transform.rotation;
        obj.SetActive(true);
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Bullet : MonoBehaviour
{
    private Rigidbody2D rb;
    public float speed;
    public float destroyY = 16f;
    public float destroyX = 16f;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {   
        rb.velocity = transform.up * speed;
        
        if (transform.position.y > destroyY || transform.position.y < -destroyY || transform.position.x > destroyX || transform.position.x < -destroyX)
            gameObject.SetActive(false);
    }
    
}

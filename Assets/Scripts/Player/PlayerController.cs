using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;


[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] 
    private GameObject explosionPrefab;
    
    private Rigidbody2D rb;
    public bool canShoot = false;
    public bool isInmune = false;

    private float movementX;
    private float movementY;
    
        
    private float minX = -16f; 
    private float maxX = 16.8f;
    private float minY = -16f;
    private float maxY = 16f;  
    
    public float speed;
    public int rotationSpeed;
    public float maxSpeed;
    
    [SerializeField]
    private GameObject shield;
    public bool isShieldActive = false;
    
    [SerializeField]
    private bool isTripleShotActive = false;
    private Coroutine tripleShotRoutine;
    
    
    
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    
    
    void FixedUpdate()
    {
        rb.rotation -= movementX * rotationSpeed;
        speed = Mathf.Clamp(speed + movementY, 1.5f, maxSpeed);
        rb.velocity = transform.up * speed;
        limits();
    }
    
    private void OnMove(InputValue movementvalue)
    {
        Vector2 movementVector = movementvalue.Get<Vector2>();
        movementX = movementVector.x;
        movementY = movementVector.y;
    }

    /// <summary>
    ///  On Fire Method is called when the player press the space bar or the left mouse button
    /// Have some interesting features like the triple shot using Quaternion.Euler and the Object Pooling for optimize the performance
    /// </summary>
    private void OnFire()
    {
        if (canShoot)
        {
            AudioManager.instance.PlayShootSound();
        if (isTripleShotActive)
        {
            float angle = 45f;

            GameObject obj = ObjectPooler.instance.GetPoolObject("Bullet");
            obj.transform.position = transform.position;
            obj.transform.rotation = transform.rotation;
            obj.SetActive(true);

            obj = ObjectPooler.instance.GetPoolObject("Bullet");
            obj.transform.position = transform.position;
            obj.transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x,
                transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.y - angle);
            obj.SetActive(true);

            obj = ObjectPooler.instance.GetPoolObject("Bullet");
            obj.transform.position = transform.position;
            obj.transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x,
                transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.y + angle);
            obj.SetActive(true);
        }
        else
        {
            GameObject obj = ObjectPooler.instance.GetPoolObject("Bullet");
            obj.transform.position = transform.position;
            obj.transform.rotation = transform.rotation;
            obj.SetActive(true);
        }
    }
}
    
    /// <summary>
    /// I use this Trigger to detect the collision with the enemies and the power ups
    /// 1. SHIELD Power Up: If the player has the shield active, the enemy will be destroyed, have a duration of 3 seconds for the corutine
    /// </summary>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            if (isShieldActive)
            {
                AudioManager.instance.PlayExplosionSound();
                Instantiate(explosionPrefab, transform.position, Quaternion.identity);
                Debug.Log("Shield Destroyed");
                collision.gameObject.SetActive(false);

            }
            else if (!isInmune)
            {
                AudioManager.instance.PlayExplosionSound();
                gameObject.SetActive(false);
                gameObject.SetActive(true);
                GameManager.instance.gameOver();
                SceneManager.LoadScene("GameOver");
                Debug.Log("Game Over");
                
            }
            else
            {
                AudioManager.instance.PlayExplosionSound();
                gameObject.SetActive(false);
                gameObject.SetActive(true);
                GameManager.instance.gameOver();
                SceneManager.LoadScene("GameOver");
                Debug.Log("Game Over");
                
            }
        }// ---- Colision co la bala del Boss ---- ///
        else if (collision.gameObject.CompareTag("BossBullet"))
        {
            if (isShieldActive)
            {
                AudioManager.instance.PlayExplosionSound();
                Instantiate(explosionPrefab, collision.transform.position, Quaternion.identity);
                collision.gameObject.SetActive(false);
            }
            else if (!isInmune)
            {
                AudioManager.instance.PlayExplosionSound();
                gameObject.SetActive(false);
                GameManager.instance.gameOver();
                SceneManager.LoadScene("GameOver");
                Debug.Log("Game Over");
            }
        }
        // ---- Colision con la el boss ---- ///
        else if (collision.gameObject.CompareTag("miniBoss"))
        {
            AudioManager.instance.PlayExplosionSound();
            gameObject.SetActive(false);
            GameManager.instance.gameOver();
            SceneManager.LoadScene("GameOver");
            Debug.Log("Game Over");
        }
    }
    
    public IEnumerator TripleShotPowerDownRoutine()
    {
        yield return new WaitForSeconds(5f);
        isTripleShotActive = false;
        
    }
    
    public IEnumerator isInmuneRoutine()
    {
        yield return new WaitForSeconds(3f);
        isInmune = false;
        isShieldActive = false;
        shield.SetActive(false);
    }

    /// <summary>
    /// Activates the shield power-up, providing temporary invulnerability.
    /// </summary>
    public void enableShield()
    {
        {
            isInmune = true;
            isShieldActive = true;
            shield.SetActive(true);
            StartCoroutine(isInmuneRoutine());
        }
    }

    /// <summary>
    /// Activates the triple shot power-up for a limited duration.
    /// </summary>
    public void enableTripleShot()
    {
        if (isTripleShotActive)
        {
            StopCoroutine(tripleShotRoutine);
        }
        isTripleShotActive = true;
        tripleShotRoutine = StartCoroutine(TripleShotPowerDownRoutine());
        
       float angle = 45f;
       
         GameObject obj = ObjectPooler.instance.GetPoolObject("Bullet");
         obj.transform.position = transform.position;
         obj.transform.rotation = transform.rotation;
         obj.SetActive(true);
            
        obj = ObjectPooler.instance.GetPoolObject("Bullet");
        obj.transform.position = transform.position;
        obj.transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.y - angle);
        obj.SetActive(true);
        
        obj = ObjectPooler.instance.GetPoolObject("Bullet");
        obj.transform.position = transform.position;
        obj.transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.y + angle);
        obj.SetActive(true);
    }
    
    public void limits()
    {
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, minX, maxX), Mathf.Clamp(transform.position.y, minY, maxY), 0); 
    }
    
    public void isInmuned()
    {
        isInmune = true;
        StartCoroutine(isInmuneRoutine());
    }
    
}

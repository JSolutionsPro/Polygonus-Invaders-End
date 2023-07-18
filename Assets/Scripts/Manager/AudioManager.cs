using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    
    public static AudioManager instance;
    
    public AudioClip coinSound;
    public AudioClip explosionSound;
    public AudioClip shootSound;
    public AudioClip powerUpSound;
    public AudioClip startSound;
    
    private AudioSource audioSource;
    private void Awake()
    { 
        if (instance != null)
        {
            Destroy(gameObject);
        }   else
        {
            instance = this;
            audioSource = GetComponent<AudioSource>();
        }
        DontDestroyOnLoad(gameObject);
    }
    
    public void PlayCoinSound()
    {
        audioSource.PlayOneShot(coinSound);
    }
    
    public void PlayExplosionSound()
    {
        audioSource.PlayOneShot(explosionSound);
    }
    
    public void PlayShootSound()
    {
        audioSource.PlayOneShot(shootSound);
    }
    
    public void PlayPowerUpSound()
    {
        audioSource.PlayOneShot(powerUpSound);
    }
    
    public void PlayStartSound()
    {
        audioSource.PlayOneShot(startSound);
    }
}

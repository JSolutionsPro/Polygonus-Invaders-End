using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;
    public TextMeshProUGUI scoreText;
    public Transform UIPanel;
    public GameObject[] gameObjectsToActivate;
    private PlayerController playerController;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);
        
        playerController = FindObjectOfType<PlayerController>();
    }
    

    public void updateScore(int score)
    {
        scoreText.text = "Score: " + GameManager.instance.score;
    }
    
    public void Play()
    {
        AudioManager.instance.PlayStartSound();
        UIPanel.gameObject.SetActive(false);
        playerController.canShoot = true; 
        ActivateObjects();
    }
    
    public void ActivateObjects ()
    {
        foreach (GameObject gameObject in gameObjectsToActivate)
        {
            gameObject.SetActive(true);
        }

    }
    
}

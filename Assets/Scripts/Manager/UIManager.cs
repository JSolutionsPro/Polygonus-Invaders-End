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
    public Transform winPanel;
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

    private void Start()
    {
        winPanel.gameObject.SetActive(false);
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

    public void Restart()
    {
        AudioManager.instance.PlayStartSound();
        winPanel.gameObject.SetActive(false);
        UIPanel.gameObject.SetActive(true);
        SceneManager.LoadScene(0);
        //winPanel.gameObject.SetActive(true);
        //playerController.canShoot = false;
        //SceneManager.LoadScene(0);
    }
    
    public void ActivateObjects ()
    {
        foreach (GameObject gameObject in gameObjectsToActivate)
        {
            gameObject.SetActive(true);
        }

    }
    
}

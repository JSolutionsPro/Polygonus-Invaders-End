using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public bool isGameOver = false;
    public bool gameWin = false;
    public int score = 0;
    public GameObject[] gameObjectsToDisable;
    

    private void Awake()
    { 
        if (instance != null)
        {
            Destroy(gameObject);
        }   else
        {
            instance = this;
        }
        DontDestroyOnLoad(gameObject);
    }

    public void Start()
    {
        DisableObjects();

    }

    public void addScore(int scoreGive) 
    {
        score += scoreGive; 
        UIManager.instance.updateScore(scoreGive);
    }
    
    public void Win()
    {
        gameWin = true;
        DisableObjects();
        UIManager.instance.winPanel.gameObject.SetActive(true);
        score = 0;
        //SceneManager.LoadScene(2); 
        //Destroy(UIManager.instance.gameObject);
    }

    public void BossDestroyed()
    {
        if (!gameWin)
        {
            Win();
        }
    }
   public void gameOver()
    {
        isGameOver = true;
        DisableObjects();
        score = 0;
        SceneManager.LoadScene(1); 
        Destroy(UIManager.instance.gameObject);
        
    }

   public void DisableObjects () 
   {
       foreach (GameObject gameObject in gameObjectsToDisable)
       {
           gameObject.SetActive(false);
       }
   }
   
}

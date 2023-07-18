using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    public static GameOver instance;

    public void restartGame()
     {
         

         AudioManager.instance.PlayStartSound();
         SceneManager.LoadScene(0);
     }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;


public class Fade : MonoBehaviour
{
    [SerializeField]
    SpriteRenderer spriteRenderer;

    [ContextMenu("FadeIn")]
    public void fadeIn()
    {
        spriteRenderer.DOFade(1, 2);
    } 
    
    [ContextMenu("FadeOut")]
    public void fadeOut()
    {
        spriteRenderer.DOFade(0, 2);
    }

    private void Start()
    {
        fadeOut();
    }
}

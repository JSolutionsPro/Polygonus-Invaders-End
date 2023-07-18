using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explotion : MonoBehaviour
{
    private void Start()
    {
        Destroy(this.gameObject,4f);
    }
}

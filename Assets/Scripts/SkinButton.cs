using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;

public class SkinButton : MonoBehaviour
{
    //private int _numb;
    
    void Start()
    {
        //_numb = Convert.ToInt32(gameObject.name);
    }

    void Update()
    {
        
    }

    public void Press()
    {
        if (Bank.skins[Convert.ToInt32(gameObject.name)] == 1)
        {
            Bank.currentSkin = Convert.ToInt32(gameObject.name);
        }
    }
}

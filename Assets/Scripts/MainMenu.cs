using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public Text money;
    
    private void Start()
    {
        money.text = Bank.money.ToString();
    }

    public void PlayButton()
    {
        SceneManager.LoadScene(1);
    }
}

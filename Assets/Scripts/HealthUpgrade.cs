using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;
using UnityEngine.UI;

public class HealthUpgrade : MonoBehaviour
{
    public Text costText;
    public Text healthText;
    public Text money;
    
    private void Start()
    {
        costText.text = Bank.healthCost.ToString();
        money.text = Bank.money.ToString();
        healthText.text = Bank.health.ToString();
    }

    public void Press()
    {
        if (Bank.money >= Bank.healthCost)
        {
            Bank.health++;
            Bank.money -= Bank.healthCost;
            Bank.healthCost += 10;
            costText.text = Bank.healthCost.ToString();
            money.text = Bank.money.ToString();
            healthText.text = Bank.health.ToString();
        }
    }
}

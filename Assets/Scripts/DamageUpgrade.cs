using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;
using UnityEngine.UI;

public class DamageUpgrade : MonoBehaviour
{
    public Text costText;
    public Text damageText;
    public Text money;
    
    private void Start()
    {
        costText.text = Bank.damageCost.ToString();
        money.text = Bank.money.ToString();
        damageText.text = Bank.damage.ToString();
    }

    public void Press()
    {
        if (Bank.money >= Bank.damageCost)
        {
            Bank.damage++;
            Bank.money -= Bank.damageCost;
            money.text = Bank.money.ToString();
            damageText.text = Bank.damage.ToString();
        }
    }
}

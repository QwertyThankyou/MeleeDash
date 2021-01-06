using System;
using DefaultNamespace;
using UnityEngine;
using UnityEngine.UI;

public class SkinButton : MonoBehaviour
{
    public Text money;
    private int _numb;
    
    void Start()
    {
        _numb = Convert.ToInt32(gameObject.name);

        if (Bank.skins[_numb] == 0)
        {
            GetComponent<Image>().color = Color.black;
        }
    }

    public void Press()
    {
        if (GetComponent<Image>().color == Color.black)
        {
            if (Bank.money >= 100)
            {
                GetComponent<Image>().color = Color.white;
                Bank.skins[_numb] = 1;
                Bank.currentSkin = _numb;
                Bank.money -= 100;
                money.text = Bank.money.ToString();
            }
        }
        
        else if (Bank.skins[Convert.ToInt32(gameObject.name)] == 1)
        {
            Bank.currentSkin = _numb;
        }
    }
}

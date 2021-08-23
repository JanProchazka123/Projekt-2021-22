using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoneySystem : MonoBehaviour
{
    public Text moneyText;
    public int currentMoney;
    // Start is called before the first frame update
    void Start()
    {
        moneyText.text = currentMoney.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void UseMoney(int amount)
    {
        if(currentMoney - amount >= 0)
        {
            currentMoney -= amount;
            moneyText.text = currentMoney.ToString();
        }
        else
        {
            
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyUse : MonoBehaviour
{
    [Header("Assinables")]
    public MoneySystem moneySystem;
    public BoxCollider boxCollider;
    [Header("Toggle Pickup / Press")]
    public bool pickup;
    [Header("Spend / Gain")]
    public int spendMoney = 0;
    public int gainMoney = 0;

    void Start()
    {
        
    }

    void Update()
    {

    }
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if(pickup == false)
            {
                moneySystem.UseMoney(-gainMoney);
                moneySystem.UseMoney(spendMoney);
            }
            if(pickup == true)
            {
                moneySystem.UseMoney(-gainMoney);
                moneySystem.UseMoney(spendMoney);
            }
        }
    }
}

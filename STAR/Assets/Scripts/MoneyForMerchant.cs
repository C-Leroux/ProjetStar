using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoneyForMerchant
{
    private static MoneyForMerchant instance = null;// SINGLETON
    private int currentMoney;
    private int departMoney;
    private Text moneyText;

    public MoneyForMerchant()
    {
        instance = this;
        departMoney = 500;
        currentMoney = 0;
    }

    public static MoneyForMerchant Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new MoneyForMerchant();
            }
            return instance;
        }
    }

    public void AddMoney(int money)
    {
        currentMoney += money;
    }

    public int GetMoney()
    {
        return currentMoney;
    }

    public void RemoveMoney(int money)
    {
        currentMoney -= money;
    }

    public void SetMoneyText(Text moneyText)
    {
        this.moneyText = moneyText;
        this.moneyText.text = ("" + currentMoney);
    }

    public void SetDepartMoney(int moneyDepart)
    {
        departMoney += moneyDepart;
        currentMoney = departMoney;
    }

    public void UpdateMoney()
    {
        moneyText.text = ("" + currentMoney);
    }
}

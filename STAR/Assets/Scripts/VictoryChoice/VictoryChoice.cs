using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VictoryChoice
{
    private static VictoryChoice instance = null;// SINGLETON
    private string[] m_choix = {"Vaisseau", "Base", "Money", "Turret", "MoneyForMerchant" };
    private string[] m_vaisseau = {"ReduceTime"};
    private string[] m_money = {"AddMaxMoney", "AddDepartMoney", "AddMoneyPerSecond"};
    private string[] m_base = {"AddMaxLp"};
    private string[] m_turret = {"AddNewTurret"};
    private string[] m_moneyForMerchant = { "AddMoney" };

    public VictoryChoice()
    {
        instance = this;
    }

    public static VictoryChoice Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new VictoryChoice();
            }
            return instance;
        }
    }

    public string GetChoix(int choix)
    {
        if(choix > m_choix.Length)
        {
            choix = choix % m_choix.Length;
        }
        return m_choix[choix];
    }
}

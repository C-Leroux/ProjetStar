using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts
{
    public class Money
    {
        private static Money instance = null;// SINGLETON
        private int currentMoney;
        private int maxMonney;
        private int departMoney;
        private int moneyPerSecond;
        private Text moneyText;

        public Money()
        {
            instance = this;
            maxMonney = 500;
            departMoney = 20;
            moneyPerSecond = 1;
        }

        public static Money Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new Money();
                }
                return instance;
            }
        }

        public int GetCurrentMoney()
        {
            return currentMoney;
        }

        public void AddMoneyText(Text moneyText)
        {
            this.moneyText = moneyText;
            moneyText.text = ("" + currentMoney);
        }

        public void UpdateMoney()
        {
            if (currentMoney > maxMonney)
            {
                currentMoney = maxMonney;
            }
            moneyText.text = ("" + currentMoney);
        }

        public void AddMoneyPerSecond()
        {
            moneyPerSecond++;
        }

        public void AddDepartMoney(int addMoney)
        {
            departMoney += addMoney;
            if(departMoney > maxMonney)
            {
                departMoney = maxMonney;
            }
        }
        public void AddMaxMoney(int addMoney)
        {
            maxMonney += addMoney;
            if(maxMonney > 10000)
            {
                maxMonney = 10000;
            }
        }

        public void LaunchBoard()
        {
            currentMoney = departMoney;
        }

        public void UpdateMoneySecond()
        {
            currentMoney += moneyPerSecond;
            UpdateMoney();
        }

        public void AddMoney(int money)
        {
            currentMoney += money;
            UpdateMoney();
        }

        public void RemoveMoney(int money)
        {
            currentMoney -= money;
            if (currentMoney < 0)
            {
                currentMoney = 0;
            }
            UpdateMoney();
        }

        public string GetInfos()
        {
            return ("Argent en début : " + "\n" + departMoney + "\n"
                + "Argent maximum : " + "\n" + maxMonney + "\n"
                + "Argent par seconde : " + "\n" + moneyPerSecond);
        }

        public static void Reset()
        {
            instance = null;
        }

    }
}

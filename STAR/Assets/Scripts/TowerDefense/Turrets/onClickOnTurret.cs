using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class onClickOnTurret : MonoBehaviour
{
    [SerializeField]
    private GameObject turret;
    [SerializeField]
    private Canvas turretUI;
    private GameObject boardManager;
    private GameObject turretManager;


    // Start is called before the first frame update
    void Start()
    {
        turretUI.enabled = false;
        boardManager = GameObject.FindGameObjectWithTag("boardManager");
        turretManager = GameObject.FindGameObjectWithTag("turretManager");
    }


    private void OnMouseDown()
    {
        if (BoardManager.Instance().timerIsRunning)
        {
            turretUI.enabled = !turretUI.isActiveAndEnabled;
        }
    }

    public void onClickUITurretButton(string Name)
    {
        if (BoardManager.Instance().timerIsRunning)
        {
            switch (Name)
            {
                case "upgrade":
                    upgradeTower();
                    //Debug.Log("tourelle Upgraded");
                    break;
                case "sell":
                    int sellPrice = turret.GetComponent<TurretBehaviour>().getTurretSelectedCost() / 2;
                    Money.Instance.AddMoney(sellPrice);
                    GameObject.Destroy(this.gameObject);
                    break;
            }
        }
    }
  
    private void upgradeTower()
    {
        int upgradeCost = 0;
        switch (turret.GetComponent<TurretBehaviour>().getTurretSelectedName())
        {
            case "STAR": //TOURELLE DE LA STAR
                upgradeCost = 15;
                if (checkIfUpgradeIsPossible(upgradeCost))
                {
                    Money.Instance.RemoveMoney(upgradeCost);
                    turret.GetComponent<TurretBehaviour>().setCostForUpgrade(upgradeCost);
                    turret.GetComponent<TurretBehaviour>().setNameForUpgrade("STAR1");
                    turret.GetComponent<TurretBehaviour>().setAttackSpeedForUpgrade(1.5f);
                    turret.GetComponent<TurretBehaviour>().setSpriteForUpgrade("STAR1");
                    SpecialEffectsHelper.Instance.buildOrUpgradeTower(turret.transform.position);
                }
                else
                {
                    Debug.Log("no money");
                }
                break;
            case "STAR1": //TOURELLE DE LA STAR NIVEAU 1
                upgradeCost = 20;
                if (checkIfUpgradeIsPossible(upgradeCost))
                {
                    Money.Instance.RemoveMoney(upgradeCost);
                    turret.GetComponent<TurretBehaviour>().setCostForUpgrade(upgradeCost);
                    turret.GetComponent<TurretBehaviour>().setNameForUpgrade("STAR2");
                    turret.GetComponent<TurretBehaviour>().setAttackSpeedForUpgrade(2f);
                    turret.GetComponent<TurretBehaviour>().setSpriteForUpgrade("STAR2");
                    SpecialEffectsHelper.Instance.buildOrUpgradeTower(turret.transform.position);
                }
                else
                {
                    Debug.Log("no money");
                }
                break;
           

            case "Grenadiere": //Grenadiere
                upgradeCost = 20;
                if (checkIfUpgradeIsPossible(upgradeCost))
                {
                    Money.Instance.RemoveMoney(upgradeCost);
                    turret.GetComponent<TurretBehaviour>().setCostForUpgrade(upgradeCost);
                    turret.GetComponent<TurretBehaviour>().setNameForUpgrade("Grenadiere1");
                    turret.GetComponent<TurretBehaviour>().setSpriteForUpgrade("Grenadiere1");
                    SpecialEffectsHelper.Instance.buildOrUpgradeTower(turret.transform.position);
                }
                else
                {
                    Debug.Log("no money");
                }
                break;
            case "Grenadiere1": //Grenadiere NIVEAU 1
                upgradeCost = 30;
                if (checkIfUpgradeIsPossible(upgradeCost))
                {
                    Money.Instance.RemoveMoney(upgradeCost);
                    turret.GetComponent<TurretBehaviour>().setCostForUpgrade(upgradeCost);
                    turret.GetComponent<TurretBehaviour>().setNameForUpgrade("Grenadiere2");
                    turret.GetComponent<TurretBehaviour>().setSpriteForUpgrade("Grenadiere2");
                    SpecialEffectsHelper.Instance.buildOrUpgradeTower(turret.transform.position);
                }
                else
                {
                    Debug.Log("no money");
                }
                break;
           


            case "Cryomancienne": //Cryomancienne
                upgradeCost = 20;
                if (checkIfUpgradeIsPossible(upgradeCost))
                {
                    Money.Instance.RemoveMoney(upgradeCost);
                    turret.GetComponent<TurretBehaviour>().setCostForUpgrade(upgradeCost);
                    turret.GetComponent<TurretBehaviour>().setNameForUpgrade("Cryomancienne1");
                    turret.GetComponent<TurretBehaviour>().setAttackSpeedForUpgrade(1f);
                    turret.GetComponent<TurretBehaviour>().setRangeForUpgrade(6f);
                    turret.GetComponent<TurretBehaviour>().setSpriteForUpgrade("Cryomancienne1");
                    SpecialEffectsHelper.Instance.buildOrUpgradeTower(turret.transform.position);
                }
                else
                {
                    Debug.Log("no money");
                }
                break;
            case "Cryomancienne1": //Cryomancienne NIVEAU 1
                upgradeCost = 25;
                if (checkIfUpgradeIsPossible(upgradeCost))
                {
                    Money.Instance.RemoveMoney(upgradeCost);
                    turret.GetComponent<TurretBehaviour>().setCostForUpgrade(upgradeCost);
                    turret.GetComponent<TurretBehaviour>().setNameForUpgrade("Cryomancienne2");
                    turret.GetComponent<TurretBehaviour>().setAttackSpeedForUpgrade(1.2f);
                    turret.GetComponent<TurretBehaviour>().setRangeForUpgrade(7f);
                    turret.GetComponent<TurretBehaviour>().setSpriteForUpgrade("Cryomancienne2");
                    SpecialEffectsHelper.Instance.buildOrUpgradeTower(turret.transform.position);
                }
                else
                {
                    Debug.Log("no money");
                }
                break;
            


            case "Empoisonneuse": //Empoisonneuse
                upgradeCost = 20;
                if (checkIfUpgradeIsPossible(upgradeCost))
                {
                    Money.Instance.RemoveMoney(upgradeCost);
                    turret.GetComponent<TurretBehaviour>().setCostForUpgrade(upgradeCost);
                    turret.GetComponent<TurretBehaviour>().setNameForUpgrade("Empoisonneuse1");
                    turret.GetComponent<TurretBehaviour>().setAttackSpeedForUpgrade(1.5f);
                    turret.GetComponent<TurretBehaviour>().setRangeForUpgrade(6f);
                    turret.GetComponent<TurretBehaviour>().setSpriteForUpgrade("Empoisonneuse1");
                    SpecialEffectsHelper.Instance.buildOrUpgradeTower(turret.transform.position);
                }
                else
                {
                    Debug.Log("no money");
                }
                break;
            case "Empoisonneuse1": //Empoisonneuse NIVEAU 1
                upgradeCost = 25;
                if (checkIfUpgradeIsPossible(upgradeCost))
                {
                    Money.Instance.RemoveMoney(upgradeCost);
                    turret.GetComponent<TurretBehaviour>().setCostForUpgrade(upgradeCost);
                    turret.GetComponent<TurretBehaviour>().setNameForUpgrade("Empoisonneuse2");
                    turret.GetComponent<TurretBehaviour>().setAttackSpeedForUpgrade(2f);
                    turret.GetComponent<TurretBehaviour>().setRangeForUpgrade(7f);
                    turret.GetComponent<TurretBehaviour>().setSpriteForUpgrade("Empoisonneuse2");
                    SpecialEffectsHelper.Instance.buildOrUpgradeTower(turret.transform.position);
                }
                else
                {
                    Debug.Log("no money");
                }
                break;
           


            case "Pyromancienne": //Pyromancienne
                upgradeCost = 20;
                if (checkIfUpgradeIsPossible(upgradeCost))
                {
                    Money.Instance.RemoveMoney(upgradeCost);
                    turret.GetComponent<TurretBehaviour>().setCostForUpgrade(upgradeCost);
                    turret.GetComponent<TurretBehaviour>().setNameForUpgrade("Pyromancienne1");
                    turret.GetComponent<TurretBehaviour>().setAttackSpeedForUpgrade(0.8f);
                    turret.GetComponent<TurretBehaviour>().setRangeForUpgrade(6f);
                    turret.GetComponent<TurretBehaviour>().setSpriteForUpgrade("Pyromancienne1");
                    SpecialEffectsHelper.Instance.buildOrUpgradeTower(turret.transform.position);
                }
                else
                {
                    Debug.Log("no money");
                }
                break;
            case "Pyromancienne1": //Pyromancienne NIVEAU 1
                upgradeCost = 25;
                if (checkIfUpgradeIsPossible(upgradeCost))
                {
                    Money.Instance.RemoveMoney(upgradeCost);
                    turret.GetComponent<TurretBehaviour>().setCostForUpgrade(upgradeCost);
                    turret.GetComponent<TurretBehaviour>().setNameForUpgrade("Pyromancienne2");
                    turret.GetComponent<TurretBehaviour>().setAttackSpeedForUpgrade(1f);
                    turret.GetComponent<TurretBehaviour>().setRangeForUpgrade(7f);
                    turret.GetComponent<TurretBehaviour>().setSpriteForUpgrade("Pyromancienne2");
                    SpecialEffectsHelper.Instance.buildOrUpgradeTower(turret.transform.position);
                }
                else
                {
                    Debug.Log("no money");
                }
                break;
            


            case "Survolteuse": //Survolteuse
                upgradeCost = 20;
                if (checkIfUpgradeIsPossible(upgradeCost))
                {
                    Money.Instance.RemoveMoney(upgradeCost);
                    turret.GetComponent<TurretBehaviour>().setCostForUpgrade(upgradeCost);
                    turret.GetComponent<TurretBehaviour>().setNameForUpgrade("Survolteuse1");
                    turret.GetComponent<TurretBehaviour>().setAttackSpeedForUpgrade(8f);
                    turret.GetComponent<TurretBehaviour>().setSpriteForUpgrade("Survolteuse1");
                    SpecialEffectsHelper.Instance.buildOrUpgradeTower(turret.transform.position);
                }
                else
                {
                    Debug.Log("no money");
                }
                break;
            case "Survolteuse1": //Survolteuse NIVEAU 1
                upgradeCost = 25;
                if (checkIfUpgradeIsPossible(upgradeCost))
                {
                    Money.Instance.RemoveMoney(upgradeCost);
                    turret.GetComponent<TurretBehaviour>().setCostForUpgrade(upgradeCost);
                    turret.GetComponent<TurretBehaviour>().setNameForUpgrade("Survolteuse2");
                    turret.GetComponent<TurretBehaviour>().setAttackSpeedForUpgrade(10f);
                    turret.GetComponent<TurretBehaviour>().setSpriteForUpgrade("Survolteuse2");
                    SpecialEffectsHelper.Instance.buildOrUpgradeTower(turret.transform.position);
                }
                else
                {
                    Debug.Log("no money");
                }
                break;
            default: // NIVEAU MAX
                break;
             }
    }


    private bool checkIfUpgradeIsPossible(int cost)
    {
        return cost < Money.Instance.GetCurrentMoney();
    }
    // Update is called once per frame
    void Update()
    {
     
    }
}

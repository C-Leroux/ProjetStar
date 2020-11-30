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
    [SerializeField]
    private GameObject sell;
    [SerializeField]
    private GameObject upgrade;
    private GameObject boardManager;
    private GameObject turretManager;


    private string selectedTurretName;
    // Start is called before the first frame update
    void Start()
    {
        turretUI.enabled = false;
        boardManager = GameObject.FindGameObjectWithTag("boardManager");
        turretManager = GameObject.FindGameObjectWithTag("turretManager");
    }


    private void OnMouseDown()
    {
        turretUI.enabled = !turretUI.isActiveAndEnabled;
    }

    public void onClickUITurretButton(string Name)
    {
            
        switch (Name)
        {
            case "upgrade":
                upgradeTower();
                Debug.Log("tourelle Upgraded");
                break;
            case "sell":
                int sellPrice = this.GetComponent<TurretBehaviour>().getTurretSelectedCost() / 2;
                Money.Instance.AddMoney(sellPrice);
                GameObject.Destroy(this.gameObject);
                break;
            
        }
    }
  
    private void upgradeTower()
    {
        int upgradeCost = 0;
        switch (this.GetComponent<TurretBehaviour>().getTurretSelectedName())
        {
            case "STAR": //TOURELLE DE LA STAR
                upgradeCost = 15;
                if (checkIfUpgradeIsPossible(upgradeCost))
                {
                    Money.Instance.RemoveMoney(upgradeCost);
                    this.GetComponent<TurretBehaviour>().setCostForUpgrade(upgradeCost);
                    this.GetComponent<TurretBehaviour>().setNameForUpgrade("STAR1");
                    this.GetComponent<TurretBehaviour>().setAttackSpeedForUpgrade(1.5f);
                    this.GetComponent<TurretBehaviour>().setSpriteForUpgrade("STAR1");
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
                    this.GetComponent<TurretBehaviour>().setCostForUpgrade(upgradeCost);
                    this.GetComponent<TurretBehaviour>().setNameForUpgrade("STAR2");
                    this.GetComponent<TurretBehaviour>().setAttackSpeedForUpgrade(2f);
                    this.GetComponent<TurretBehaviour>().setSpriteForUpgrade("STAR2");
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
                    this.GetComponent<TurretBehaviour>().setCostForUpgrade(upgradeCost);
                    this.GetComponent<TurretBehaviour>().setNameForUpgrade("Grenadiere1");
                    this.GetComponent<TurretBehaviour>().setSpriteForUpgrade("Grenadiere1");
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
                    this.GetComponent<TurretBehaviour>().setCostForUpgrade(upgradeCost);
                    this.GetComponent<TurretBehaviour>().setNameForUpgrade("Grenadiere2");
                    this.GetComponent<TurretBehaviour>().setSpriteForUpgrade("Grenadiere2");
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
                    this.GetComponent<TurretBehaviour>().setCostForUpgrade(upgradeCost);
                    this.GetComponent<TurretBehaviour>().setNameForUpgrade("Cryomancienne1");
                    this.GetComponent<TurretBehaviour>().setAttackSpeedForUpgrade(1f);
                    this.GetComponent<TurretBehaviour>().setRangeForUpgrade(6f);
                    this.GetComponent<TurretBehaviour>().setSpriteForUpgrade("Cryomancienne1");
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
                    this.GetComponent<TurretBehaviour>().setCostForUpgrade(upgradeCost);
                    this.GetComponent<TurretBehaviour>().setNameForUpgrade("Cryomancienne2");
                    this.GetComponent<TurretBehaviour>().setAttackSpeedForUpgrade(1.2f);
                    this.GetComponent<TurretBehaviour>().setRangeForUpgrade(7f);
                    this.GetComponent<TurretBehaviour>().setSpriteForUpgrade("Cryomancienne2");
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
                    this.GetComponent<TurretBehaviour>().setCostForUpgrade(upgradeCost);
                    this.GetComponent<TurretBehaviour>().setNameForUpgrade("Empoisonneuse1");
                    this.GetComponent<TurretBehaviour>().setAttackSpeedForUpgrade(1.5f);
                    this.GetComponent<TurretBehaviour>().setRangeForUpgrade(6f);
                    this.GetComponent<TurretBehaviour>().setSpriteForUpgrade("Empoisonneuse1");
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
                    this.GetComponent<TurretBehaviour>().setCostForUpgrade(upgradeCost);
                    this.GetComponent<TurretBehaviour>().setNameForUpgrade("Empoisonneuse2");
                    this.GetComponent<TurretBehaviour>().setAttackSpeedForUpgrade(2f);
                    this.GetComponent<TurretBehaviour>().setRangeForUpgrade(7f);
                    this.GetComponent<TurretBehaviour>().setSpriteForUpgrade("Empoisonneuse2");
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
                    this.GetComponent<TurretBehaviour>().setCostForUpgrade(upgradeCost);
                    this.GetComponent<TurretBehaviour>().setNameForUpgrade("Pyromancienne1");
                    this.GetComponent<TurretBehaviour>().setAttackSpeedForUpgrade(0.8f);
                    this.GetComponent<TurretBehaviour>().setRangeForUpgrade(6f);
                    this.GetComponent<TurretBehaviour>().setSpriteForUpgrade("Pyromancienne1");
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
                    this.GetComponent<TurretBehaviour>().setCostForUpgrade(upgradeCost);
                    this.GetComponent<TurretBehaviour>().setNameForUpgrade("Pyromancienne2");
                    this.GetComponent<TurretBehaviour>().setAttackSpeedForUpgrade(1f);
                    this.GetComponent<TurretBehaviour>().setRangeForUpgrade(7f);
                    this.GetComponent<TurretBehaviour>().setSpriteForUpgrade("Pyromancienne2");
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
                    this.GetComponent<TurretBehaviour>().setCostForUpgrade(upgradeCost);
                    this.GetComponent<TurretBehaviour>().setNameForUpgrade("Survolteuse1");
                    this.GetComponent<TurretBehaviour>().setAttackSpeedForUpgrade(8f);
                    this.GetComponent<TurretBehaviour>().setSpriteForUpgrade("Survolteuse1");
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
                    this.GetComponent<TurretBehaviour>().setCostForUpgrade(upgradeCost);
                    this.GetComponent<TurretBehaviour>().setNameForUpgrade("Survolteuse2");
                    this.GetComponent<TurretBehaviour>().setAttackSpeedForUpgrade(10f);
                    this.GetComponent<TurretBehaviour>().setSpriteForUpgrade("Survolteuse2");
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
         if (cost < Money.Instance.GetCurrentMoney())
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    // Update is called once per frame
    void Update()
    {
     
    }
}

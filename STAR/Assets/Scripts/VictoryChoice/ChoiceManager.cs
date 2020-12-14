using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts
{
    public class ChoiceManager : MonoBehaviour
    {
        private static ChoiceManager instance = null;// SINGLETON
        public Sprite[] m_sprite;
        public Text[] m_tabText;
        public Text[] m_desciptText;
        public Image[] m_tabImage;
        public Sprite[] turretSprites;
        //private string[] m_stringText;
        private ArrayList m_stringText;
        private ArrayList couts;
        //private int[] couts;
        private string choice1;
        private string choice2;
        private string choice3;
        private bool isChoice;
        private bool checkTurret;
        private string turretName;
        private ArrayList tirageAleatoire = new ArrayList();
        private string currentBiome;

        public ChoiceManager()
        {
            instance = this;
        }

        public static ChoiceManager Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new ChoiceManager();
                }
                return instance;
            }
        }

        // Start is called before the first frame update
        void Start()
        {
            currentBiome = "terre";
        }

        // Update is called once per frame
        void Update()
        {
            //DEBUG
            if (Input.GetKeyDown("space"))
            {
                isChoice = true;
            }
            else if (Input.GetKeyDown("a"))
            {
                Player.Instance.AddTurret("Cryomancienne");
            }
            else if (Input.GetKeyDown("z"))
            {
                Player.Instance.AddTurret("Pyromancienne");
            }
            else if (Input.GetKeyDown("e"))
            {
                Player.Instance.AddTurret("Empoisonneuse");
            }
            else if (Input.GetKeyDown("r"))
            {
                Player.Instance.AddTurret("Survolteuse");
            }
            if (isChoice)
            {
                GameManager.Instance().ReturnToSolarSystem();
                isChoice = false;
            }
        }


        public void SetupChoice(Planet destination)
        {
                checkTurret = true;
                List<Turret> turrets = Player.Instance.GetTurrets();
                turretName = "Empoisonneuse";
                switch (destination.biome)
                {
                    case Planet.Biome.fire:
                        turretName = "Pyromancienne";
                         currentBiome = "fire";
                        break;
                    case Planet.Biome.ice:
                        turretName = "Cryomancienne";
                         currentBiome = "ice";
                        break;
                    case Planet.Biome.desert:
                        turretName = "Survolteuse";
                         currentBiome = "desert";
                         break;
                }

            for (int i = 0; i < turrets.Count; i++)
            {
                if(turrets[i].name == turretName)
                {
                    checkTurret = false;
                }
            }
            m_stringText = new ArrayList();
            m_stringText.Add("Augmentation des LP de la base");
            m_stringText.Add("Augmentation du revenu par seconde");
            m_stringText.Add("Augmentation de la limite d'argent");
            m_stringText.Add("Diminution du temps de récupération du pouvoir");
            m_stringText.Add("Augmentation du temps de freeze");
            m_stringText.Add("Récupération de la vie");/*,
                "Ajout de tourelle"*/
        //};
            couts = new ArrayList();
            couts.Add(100);
            couts.Add(200);
            couts.Add(75);
            couts.Add(125);
            couts.Add(100);
            couts.Add(300);/*,
             500*/
            if (checkTurret)
            {
                m_stringText.Add("Ajout de tourelle");
                couts.Add(500);
            }

                isChoice = false;
            for(int i=0; i < m_stringText.Count; i++)
            {
                tirageAleatoire.Add(i);
            }
            for (int j = 0; j < 3; j++)
            {
                int tirage = Random.Range(0, tirageAleatoire.Count);
                TransformChoice(m_tabText[j], m_tabImage[j], (int)tirageAleatoire[tirage]);
                tirageAleatoire.RemoveAt(tirage);
            }
            m_desciptText[0].text = "BASE \n" + Base.Instance.GetInfos();
            m_desciptText[1].text = "POUVOIR \n" + Spell.Instance.GetInfos();
            m_desciptText[2].text = "TOURELLES \n" + Player.Instance.GetInfos();
            m_desciptText[3].text = "ARGENT \n" + Money.Instance.GetInfos();
            //TransformChoice(text1, image1, 0);
            //TransformChoice(text2, image2, 1);
            // TransformChoice(text3, image3, 4);
        }
        
        public void Choice(string choice)
        {
            isChoice = true;
        }

        public void FoundChoice(int choice)
        {
            if(choice == 1)
            {
                ExecuteChoice(m_tabText[0].text);
            }
            else if(choice == 2)
            {
                ExecuteChoice(m_tabText[1].text);
            }
            else
            {
                ExecuteChoice(m_tabText[2].text);
            }
        }

        public void ExecuteChoice(string choice)
        {
            switch (choice)
            {
                case "Augmentation des points de vie de la base":
                    BaseLifeUp();
                    break;
                case "Augmentation du revenu par seconde":
                    MoneyRate();
                    break;
                case "Augmentation de la limite d'argent":
                    MoneyLimiteRate();
                    break;
                case "Diminution du temps de récupération du pouvoir":
                    CoolDownTime();
                    break;
                case "Augmentation du temps de freeze":
                    SpellStunTime();
                    break;
                case "Récupération de la vie":
                    BaseRecupLife();
                    break;
                case "Ajout de tourelle":
                    AddTurret();
                    break;

            }
        }

        public void TransformChoice(Text new_text, Image new_image, int indice)
        {
            new_text.text = (string)m_stringText[indice];
            new_image.sprite = m_sprite[indice];
            if (indice == 6)
            {
                switch (currentBiome) {
                case "terre" :
                     new_image.sprite = turretSprites[1];
                     break;
                case "fire" :
                        new_image.sprite = turretSprites[2];
                        break;
                case "ice" :
                        new_image.sprite = turretSprites[0];
                        break;
                case "desert":
                        new_image.sprite = turretSprites[3];
                        break;
                }

        }
            }
        
        public void BaseLifeUp()
        {
            Base.Instance.AddMaxLp(500);
            isChoice = true;
        }
        
        public void BaseRecupLife()
        {
            Base.Instance.ResetLp();
            isChoice = true;
        }

        public void MoneyLimiteRate()
        {
            Money.Instance.AddMaxMoney(250);
            isChoice = true;
        }

        public void MoneyRate()
        {
            Money.Instance.AddMoneyPerSecond();
            isChoice = true;
        }

        public void CoolDownTime()
        {
            Spell.Instance.SetColldownTime();
            isChoice = true;
        }

        public void SpellStunTime()
        {
            Spell.Instance.SetSpellStunTime();
            isChoice = true;
        }

        public void AddDepartMoney()
        {
            Money.Instance.AddDepartMoney(25);
            isChoice = true;
        }

        public void AddTurret()
        {
            Player.Instance.AddTurret(turretName);
            isChoice = true;
        }
    }
}

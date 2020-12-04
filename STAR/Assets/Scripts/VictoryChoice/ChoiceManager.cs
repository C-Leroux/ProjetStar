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
        private string[] m_stringText;
        private int[] couts;
        private string choice1;
        private string choice2;
        private string choice3;
        private bool isChoice;
        private ArrayList tirageAleatoire = new ArrayList();

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
            if (isChoice)
            {
                GameManager.Instance().Merchant();
            }
        }

        public void SetupChoice(Planet destination)
        {

            /*if(VictoryChoice.Instance.GetChoix(3) == "AddNewTurret")
            {*
                string turretName = "Empoisonneuse";
                switch (destination.Biome)
                {
                    case Planet.Biome.fire:
                        turretName = "Pyromancienne";
                        break;
                    case Planet.Biome.ice:
                        turretName = "Cryomancienne";
                        break;
                    case Planet.Biome.desert:
                        turretName = "Survolteuse";
                        break;
                }
                bool verif = false;
                Array<Turret> turrets = Player.Instance.GetTurrets();
                for (int i=0; i < turrets.Count; i++)
                {
                    if(turrets[i].name == turretName)
                    {
                        verif = true;
                    }
                }
                if (!verif)
                {
                    Player.Instance.AddTurret(turretName);
                }
                else
                {
                    VictoryChoice.Instance.GetChoix(2);
                }
            }*/
            /*choice1 = VictoryChoice.Instance.GetChoix(0);
            choice2 = VictoryChoice.Instance.GetChoix(1);
            choice3 = VictoryChoice.Instance.GetChoix(2);
            SetImage();
            text1.text = "Augmentation du revenu par seconde";
            text2.text = "Augmentation des LP de la base";
            text3.text = "Augmentation de la limite d'argent";*/
            m_stringText = new string[] 
            {
                "Augmentation des LP de la base",
                "Augmentation du revenu par seconde",
                "Augmentation de la limite d'argent",
                "Diminution du temps de récupération du pouvoir",
                "Augmentation du temps de freeze",
                "Récupération de la vie"/*,
                "Ajout de tourelle"*/
            };
            couts = new int[]
            {
                100,
                200,
                75,
                125,
                100,
                300/*,
                500*/
            };
            isChoice = false;
            for(int i=0; i < m_stringText.Length; i++)
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
            m_desciptText[2].text = "VAISSEAU \n" +Player.Instance.GetInfos();
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
                case "Augmentation des LP de la base":
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

            }
        }

        public void TransformChoice(Text new_text, Image new_image, int indice)
        {
            new_text.text = m_stringText[indice];
            new_image.sprite = m_sprite[indice];
        }

        public void SetImage()
        {
            m_tabImage[0].sprite = m_sprite[1];
            m_tabImage[1].sprite = m_sprite[0];
            m_tabImage[2].sprite = m_sprite[2];
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
    }
}

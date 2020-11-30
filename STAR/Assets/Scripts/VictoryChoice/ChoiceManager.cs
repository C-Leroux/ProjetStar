using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts
{
    public class ChoiceManager : MonoBehaviour
    {
        private static ChoiceManager instance = null;// SINGLETON
        private string choice1;
        private string choice2;
        private string choice3;
        private bool isChoice;
        public Image image1;
        public Image image2;
        public Image image3;
        public Sprite[] m_sprite;
        public string[] m_stringText;
        public Text text1;
        public Text text2;
        public Text text3;

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
            if (Input.GetKeyDown("space"))
            {
                isChoice = true;
            }

            if (isChoice)
            {
                GameManager.Instance().ReturnToSolarSystem();
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
            m_stringText = new string[] { "Augmentation des LP de la base", "Augmentation du revenu par seconde", "Augmentation de la limite d'argent" };
            isChoice = false;
            TransformChoice(text1, image1, 0);
            TransformChoice(text2, image2, 1);
            TransformChoice(text3, image3, 2);
        }
        
        public void Choice(string choice)
        {
            isChoice = true;
        }


        public void FoundChoice(int choice)
        {
            if(choice == 1)
            {
                ExecuteChoice(text1.text);
            }
            else if(choice == 2)
            {
                ExecuteChoice(text2.text);
            }
            else
            {
                ExecuteChoice(text3.text);
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

            }
        }

        public void TransformChoice(Text new_text, Image new_image, int indice)
        {
            new_text.text = m_stringText[indice];
            new_image.sprite = m_sprite[indice];
        }

        public void SetImage()
        {
            image1.sprite = m_sprite[1];
            image2.sprite = m_sprite[0];
            image3.sprite = m_sprite[2];
        }

        public void BaseLifeUp()
        {
            Base.Instance.AddMaxLp(500);
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

        public void ReduceTime()
        {

        }

        public void AddDepartMoney()
        {
            Money.Instance.AddDepartMoney(25);
        }
    }
}

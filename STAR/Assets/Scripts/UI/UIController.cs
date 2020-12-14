using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Assets.Scripts
{
    public class UIController : MonoBehaviour
    {
        private string blueShipName = "BLUE STAR"; //NOM VAISSEAU
        private string blueSpellName = "FREEZE"; //NOM SPELL
        [SerializeField] private Sprite blueStarSpellSprite; //ICON SPELL  
        [SerializeField] private Image backgroundCockpit;
        [SerializeField] private float wait_time;

        [SerializeField] private TMP_Text shipName;//UI text 
        [SerializeField] private TMP_Text spellName; //UI text
        [SerializeField] private Image spellIcon; //UI image
        [SerializeField] private GameObject secondaryPlanet;
        [SerializeField] private GameObject[] secondaryPlanets = new GameObject[4];


        // Start is called before the first frame update
        void Start()
        {

        }

        public void playSound()
        {
            soundEffectHelper.Instance.UIInteraction();
        }

        public void createMission()
        {
            //SceneManager.LoadScene("SolarSystem");
            GameManager.Instance().GoToSolarSystem();

            Debug.Log("création de la partie");
        }

        public void loadMission()
        {
            //SceneManager.LoadScene("SolarSystem");
            GameManager.Instance().GoToSolarSystem();
            playSound();
            Debug.Log("chargement de la partie");
        }

        public void LoadCredits()
        {
            GameManager.Instance().GoToCredits();
        }

        public void setShipCaract() //ship shipPicked
                                    //Un seul vaisseau dans le prototype, pas besoin de condition
        {
            //if (ship == blue ship)
            shipName.text = blueShipName;
            spellName.text = blueSpellName;
            spellIcon.sprite = blueStarSpellSprite;
        }

        public void QuitGame()
        {
            playSound();
            Application.Quit();
            //Debug.Log("Game quit");
            //Just to make sure its working
        }
    }
}
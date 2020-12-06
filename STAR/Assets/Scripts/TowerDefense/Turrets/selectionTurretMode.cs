using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts
{
    public class selectionTurretMode : MonoBehaviour
    {
        [SerializeField]
        private GameObject[] backgroundUI;
        private bool isActivate;
        // Start is called before the first frame update
        void Start()
        {

        }


        public void onClick(string name)
        {
            if (BoardManager.Instance().timerIsRunning)
            {
                List<Turret> turrets = Player.Instance.GetTurrets();

                switch (name)
                {
                    case "STAR": //TOURELLE DE LA STAR
                        isActivate = backgroundUI[0].GetComponent<Image>().isActiveAndEnabled;
                        break;
                    case "Grenadiere": //GRENADIERE
                        isActivate = backgroundUI[1].GetComponent<Image>().isActiveAndEnabled;
                        break;
                    case "Cryomancienne": //CRYOMANCIENNE
                        isActivate = backgroundUI[2].GetComponent<Image>().isActiveAndEnabled;
                        break;
                    case "Empoisonneuse": //EMPOISONNEUSE
                        isActivate = backgroundUI[3].GetComponent<Image>().isActiveAndEnabled;
                        break;
                    case "Pyromancienne": //PYROMANCIENNE
                        isActivate = backgroundUI[4].GetComponent<Image>().isActiveAndEnabled;
                        break;
                    case "Survolteuse": //SURVOLTEUSE
                        isActivate = backgroundUI[5].GetComponent<Image>().isActiveAndEnabled;
                        break;
                }

                for (int i = 0; i < turrets.Count; i++)
                {
                    switch (turrets[i].name)
                    {
                        case "STAR": //TOURELLE DE LA STAR
                            backgroundUI[0].GetComponent<Image>().enabled = true;
                            break;
                        case "Grenadiere": //GRENADIERE
                            backgroundUI[1].GetComponent<Image>().enabled = true;
                            break;
                        case "Cryomancienne": //CRYOMANCIENNE
                            backgroundUI[2].GetComponent<Image>().enabled = true;
                            break;
                        case "Empoisonneuse": //EMPOISONNEUSE
                            backgroundUI[3].GetComponent<Image>().enabled = true;
                            break;
                        case "Pyromancienne": //PYROMANCIENNE
                            backgroundUI[4].GetComponent<Image>().enabled = true;
                            break;
                        case "Survolteuse": //SURVOLTEUSE
                            backgroundUI[5].GetComponent<Image>().enabled = true;
                            break;
                    }
                }

                switch (name)
                {
                    case "STAR": //TOURELLE DE LA STAR
                        backgroundUI[0].GetComponent<Image>().enabled = !isActivate;
                        break;
                    case "Grenadiere": //GRENADIERE
                        backgroundUI[1].GetComponent<Image>().enabled = !isActivate;
                        break;
                    case "Cryomancienne": //CRYOMANCIENNE
                        backgroundUI[2].GetComponent<Image>().enabled = !isActivate;
                        break;
                    case "Empoisonneuse": //EMPOISONNEUSE
                        backgroundUI[3].GetComponent<Image>().enabled = !isActivate;
                        break;
                    case "Pyromancienne": //PYROMANCIENNE
                        backgroundUI[4].GetComponent<Image>().enabled = !isActivate;
                        break;
                    case "Survolteuse": //SURVOLTEUSE
                        backgroundUI[5].GetComponent<Image>().enabled = !isActivate;
                        break;
                }
            }

        }


        public void ChangeImage()
        {
            this.gameObject.GetComponent<Image>().enabled = !this.gameObject.GetComponent<Image>().isActiveAndEnabled;
        }



        // Update is called once per frame
        void Update()
        {

        }
    }
}

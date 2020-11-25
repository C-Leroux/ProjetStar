using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
    public class turretDisplay : MonoBehaviour
    {
        public Turret[] turrets;
        public TurretBehaviour turretTemplate;
        // Start is called before the first frame update
        void Start()
        {

        }

        public Sprite getSprite(string Name)
        {
            Sprite turretSprite=null;
            switch (Name)
            {
                case "STAR": //TOURELLE DE LA STAR
                    turretSprite = turrets[0].sprite;
                    break;
                case "Grenadiere": //GRENADIERE
                    turretSprite = turrets[1].sprite;
                    break;
                case "Cryomancienne": //CRYOMANCIENNE
                    turretSprite = turrets[2].sprite;
                    break;
                case "Empoisonneuse": //EMPOISONNEUSE
                    turretSprite = turrets[3].sprite;
                    break;
                case "Pyromancienne": //PYROMANCIENNE
                    turretSprite = turrets[4].sprite;
                    break;
                case "Survolteuse": //SURVOLTEUSE
                    turretSprite = turrets[5].sprite;
                    break;
            }
            return turretSprite;
        }

        public int getCout(string name)
        {
            int cost = 0;
            switch (name)
            {
                case "STAR": //TOURELLE DE LA STAR
                    cost = turrets[0].cost;
                    break;
                case "Grenadiere": //GRENADIERE
                    cost = turrets[1].cost;
                    break;
                case "Cryomancienne": //CRYOMANCIENNE
                    cost = turrets[2].cost;
                    break;
                case "Empoisonneuse": //EMPOISONNEUSE
                    cost = turrets[3].cost;
                    break;
                case "Pyromancienne": //PYROMANCIENNE
                    cost = turrets[4].cost;
                    break;
                case "Survolteuse": //SURVOLTEUSE
                    cost = turrets[5].cost;
                    break;
            }
            return cost;
        }

        public void createTurret(string name) //Name est a rentrer dans l'appel de la fonction dans le UI bouton
        {
            //Entrée correspondant à la fin du drag and drop 
            int towerPositionX, towerPositionY;
            //towerPositionX = xPosition;
            //towerPositionY = yPosition;
            towerPositionX = Random.Range(-2, 2);
            towerPositionY = Random.Range(-2, 2); ;
            //Faire en sorte que towerPositionX et Y s'adapte à la position de la tourelle (drag and drop ?)
            GameObject obj = GameObject.Instantiate(turretTemplate.gameObject);
            obj.transform.position = new Vector3((float)(3 * 2.56), (float)(-2 * 2.56), -1);
            TurretBehaviour behaviour = obj.GetComponent<TurretBehaviour>();
            switch (name)
            {
                case "STAR": //TOURELLE DE LA STAR
                    behaviour.init(turrets[0]);
                    break;
                case "Grenadiere": //GRENADIERE
                    behaviour.init(turrets[1]);
                    break;
                case "Cryomancienne": //CRYOMANCIENNE
                    behaviour.init(turrets[2]);
                    break;
                case "Empoisonneuse": //EMPOISONNEUSE
                    behaviour.init(turrets[3]);
                    break;
                case "Pyromancienne": //PYROMANCIENNE
                    behaviour.init(turrets[4]);
                    break;
                case "Survolteuse": //SURVOLTEUSE
                    behaviour.init(turrets[5]);
                    break;
            }
        }
        
        public void createTurret(string name, float x, float y) //Name est a rentrer dans l'appel de la fonction dans le UI bouton
        {

            GameObject obj = GameObject.Instantiate(turretTemplate.gameObject);
            obj.transform.position = new Vector3((float)(x), (float)(y), -1);
            TurretBehaviour behaviour = obj.GetComponent<TurretBehaviour>();
            switch (name)
            {
                case "STAR": //TOURELLE DE LA STAR
                    behaviour.init(turrets[0]);
                    break;
                case "Grenadiere": //GRENADIERE
                    behaviour.init(turrets[1]);
                    break;
                case "Cryomancienne": //CRYOMANCIENNE
                    behaviour.init(turrets[2]);
                    break;
                case "Empoisonneuse": //EMPOISONNEUSE
                    behaviour.init(turrets[3]);
                    break;
                case "Pyromancienne": //PYROMANCIENNE
                    behaviour.init(turrets[4]);
                    break;
                case "Survolteuse": //SURVOLTEUSE
                    behaviour.init(turrets[5]);
                    break;
            }
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
    public class turretDisplay : MonoBehaviour
    {
        public Turret[] turrets;
        public TurretBehaviour turretTemplate;
        private int xPosition;
        private int yPosition;
        // Start is called before the first frame update
        void Start()
        {

        }

        public int getCout(string name)
        {
            GameObject obj = GameObject.Instantiate(turretTemplate.gameObject);
            TurretBehaviour behaviour = obj.GetComponent<TurretBehaviour>();
            int cost = 100;
            switch (name)
            {
                case "STAR": //TOURELLE DE LA STAR
                    cost = behaviour.getCout(turrets[0]);
                    break;
                case "Grenadiere": //GRENADIERE
                    cost = behaviour.getCout(turrets[1]);
                    break;
                case "Cryomancienne": //CRYOMANCIENNE
                    cost = behaviour.getCout(turrets[2]);
                    break;
                case "Empoisonneuse": //EMPOISONNEUSE
                    cost = behaviour.getCout(turrets[3]);
                    break;
                case "Pyromancienne": //PYROMANCIENNE
                    cost = behaviour.getCout(turrets[4]);
                    break;
                case "Survolteuse": //SURVOLTEUSE
                    cost = behaviour.getCout(turrets[5]);
                    break;
            }
            Destroy(obj);
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
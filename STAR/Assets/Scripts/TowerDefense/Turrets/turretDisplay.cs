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

        public string getName()
        {
            return this.GetComponent<Turret>().name;
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

        public Turret GetTurret(string name)
        {
            Turret new_turret = null;
            switch (name)
            {
                case "STAR": //TOURELLE DE LA STAR
                    new_turret = turrets[0];
                    break;
                case "Grenadiere": //GRENADIERE
                    new_turret = turrets[1];
                    break;
                case "Cryomancienne": //CRYOMANCIENNE
                    new_turret = turrets[2];
                    break;
                case "Empoisonneuse": //EMPOISONNEUSE
                    new_turret = turrets[3];
                    break;
                case "Pyromancienne": //PYROMANCIENNE
                    new_turret = turrets[4];
                    break;
                case "Survolteuse": //SURVOLTEUSE
                    new_turret = turrets[5];
                    break;
            }
            return new_turret;
        }

        public TurretBehaviour GetTurretBehaviour()
        {
            return turretTemplate;
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}
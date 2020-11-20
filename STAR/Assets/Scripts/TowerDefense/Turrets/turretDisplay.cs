using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    
    public void createTurret(string name) //Name est a rentrer dans l'appel de la fonction dans le UI bouton
    {
        //Entrée correspondant à la fin du drag and drop 
        int towerPositionX,  towerPositionY;
        //towerPositionX = xPosition;
        //towerPositionY = yPosition;
        towerPositionX = Random.Range(-2,2);
        towerPositionY = Random.Range(-2, 2); ;
        //Faire en sorte que towerPositionX et Y s'adapte à la position de la tourelle (drag and drop ?)
        GameObject obj = GameObject.Instantiate(turretTemplate.gameObject);
        obj.transform.position = new Vector3(towerPositionX, towerPositionY, 0);
        TurretBehaviour behaviour = obj.GetComponent<TurretBehaviour>();
        switch (name) {
            case "STAR" : //TOURELLE DE LA STAR
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

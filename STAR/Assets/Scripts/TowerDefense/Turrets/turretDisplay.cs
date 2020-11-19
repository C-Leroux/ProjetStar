using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class turretDisplay : MonoBehaviour
{
    public Turret blueTurret;
    public Turret greenTurret;
    public TurretBehaviour turretTemplate;
    public int xPosition;
    public int yPosition;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    
    public void createTurret(string name)
    {
        //Entrée correspondant à la fin du drag and drop 
        int towerPositionX,  towerPositionY;
        //towerPositionX = xPosition;
        //towerPositionY = yPosition;
        towerPositionX = Random.Range(-2,2);
        towerPositionY = Random.Range(-2, 2); ;
        //Faire en sorte que towerPositionX et Y s'adapte à la position de la tourelle (drag and drop ?)
        GameObject obj = GameObject.Instantiate(turretTemplate.gameObject);
        obj.transform.position = new Vector3(towerPositionX, towerPositionY, -1);
        TurretBehaviour behaviour = obj.GetComponent<TurretBehaviour>();
        switch (name) {
            case "blue" :
                behaviour.init(blueTurret);
                break;
            case "green":
                behaviour.init(greenTurret);
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

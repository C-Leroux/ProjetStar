using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class selectionTurretMode : MonoBehaviour
{
    [SerializeField]
    private GameObject[] backgroundUI;
    // Start is called before the first frame update
    void Start()
    {
        
    }


    public void onClick(string name)
    {
        soundEffectHelper.Instance.UIInteraction();
        switch (name)
        {
            case "STAR": //TOURELLE DE LA STAR
                backgroundUI[0].GetComponent<Image>().enabled = !backgroundUI[0].GetComponent<Image>().isActiveAndEnabled;
                backgroundUI[1].GetComponent<Image>().enabled = true;
                backgroundUI[2].GetComponent<Image>().enabled = true;
                backgroundUI[3].GetComponent<Image>().enabled = true;
                backgroundUI[4].GetComponent<Image>().enabled = true;
                backgroundUI[5].GetComponent<Image>().enabled = true;
                break;
            case "Grenadiere": //GRENADIERE
                backgroundUI[1].GetComponent<Image>().enabled = !backgroundUI[1].GetComponent<Image>().isActiveAndEnabled;
                backgroundUI[0].GetComponent<Image>().enabled = true;
                backgroundUI[2].GetComponent<Image>().enabled = true;
                backgroundUI[3].GetComponent<Image>().enabled = true;
                backgroundUI[4].GetComponent<Image>().enabled = true;
                backgroundUI[5].GetComponent<Image>().enabled = true; 
                break;
            case "Cryomancienne": //CRYOMANCIENNE
                backgroundUI[2].GetComponent<Image>().enabled = !backgroundUI[2].GetComponent<Image>().isActiveAndEnabled;
                backgroundUI[1].GetComponent<Image>().enabled = true;
                backgroundUI[0].GetComponent<Image>().enabled = true;
                backgroundUI[3].GetComponent<Image>().enabled = true;
                backgroundUI[4].GetComponent<Image>().enabled = true;
                backgroundUI[5].GetComponent<Image>().enabled = true; 
                break;
            case "Empoisonneuse": //EMPOISONNEUSE
                backgroundUI[3].GetComponent<Image>().enabled = !backgroundUI[3].GetComponent<Image>().isActiveAndEnabled;
                backgroundUI[1].GetComponent<Image>().enabled = true;
                backgroundUI[2].GetComponent<Image>().enabled = true;
                backgroundUI[0].GetComponent<Image>().enabled = true;
                backgroundUI[4].GetComponent<Image>().enabled = true;
                backgroundUI[5].GetComponent<Image>().enabled = true; 
                break;
            case "Pyromancienne": //PYROMANCIENNE
                backgroundUI[4].GetComponent<Image>().enabled = !backgroundUI[4].GetComponent<Image>().isActiveAndEnabled;
                backgroundUI[1].GetComponent<Image>().enabled = true;
                backgroundUI[2].GetComponent<Image>().enabled = true;
                backgroundUI[3].GetComponent<Image>().enabled = true;
                backgroundUI[0].GetComponent<Image>().enabled = true;
                backgroundUI[5].GetComponent<Image>().enabled = true; 
                break;
            case "Survolteuse": //SURVOLTEUSE
                backgroundUI[5].GetComponent<Image>().enabled = !backgroundUI[5].GetComponent<Image>().isActiveAndEnabled;
                backgroundUI[1].GetComponent<Image>().enabled = true;
                backgroundUI[2].GetComponent<Image>().enabled = true;
                backgroundUI[3].GetComponent<Image>().enabled = true;
                backgroundUI[4].GetComponent<Image>().enabled = true;
                backgroundUI[0].GetComponent<Image>().enabled = true; 
                break;
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

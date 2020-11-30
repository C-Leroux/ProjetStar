using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class boutiqueManager : MonoBehaviour
{

    [SerializeField]
    private GameObject boutique;

    [SerializeField]
    private GameObject popUpAchat;
    [SerializeField]
    private GameObject[] items;
    [SerializeField]
    private string[] objectName;
    [SerializeField]
    private Sprite[] objectSprite;
    [SerializeField]
    private string[] objectDescription;
    [SerializeField]
    private int[] objectPrice;

    [SerializeField]
    private GameObject titrePopUp;
    [SerializeField]
    private GameObject iconPopUp;
    [SerializeField]
    private GameObject textPopUp;


    int currentSelectedItemPrice;


    void Start()
    {
        boutiqueLoadData();
    }


    private void popUpLoadData(int index)
    {
        iconPopUp.GetComponent<Image>().sprite = items[index].transform.Find("Icon/objectIcon").GetComponent<Image>().sprite;
        string objectName = items[index].transform.Find("Name/ObjectName").GetComponent<TMP_Text>().text;
        currentSelectedItemPrice = objectPrice[index];
        titrePopUp.GetComponent<TMP_Text>().text = "Achat de l'objet : " + objectName;
        textPopUp.GetComponent<TMP_Text>().text = "L'OBJECT COUTE " + currentSelectedItemPrice + " PIECES D'OR";
    }

    private void boutiqueLoadData()
    {
        int position = 0;
        for(int i = 0; i < items.Length; i++)
        {
            Transform icon = items[i].transform.Find("Icon/objectIcon");
            Image iconImage = icon.GetComponent<Image>();
            iconImage.sprite= objectSprite[position];
            Transform textName = items[i].transform.Find("Name/ObjectName");
            TMP_Text textN = textName.GetComponent<TMP_Text>();
            textN.text = objectName[position];
            Transform textDescription = items[i].transform.Find("Description/Text (TMP)");
            TMP_Text textD = textDescription.GetComponent<TMP_Text>();
            textD.text = objectDescription[position];

            //Position est un indice qui fait des boucle entre 0 et 3 simplement car le contenu n'est pas créer, donc il permet juste d'afficher qq info hardcodée

            position++;
            if (position == 3)
            {
                position = 0;
            }
        }
    }



    public void onClickBuyItemButton()
    {
        //à Remplir
        Debug.Log("item acheté au prix de  : " + currentSelectedItemPrice);
    }


    public void OnClickreturnToShopButton()
    {
        boutique.SetActive(true);
        popUpAchat.SetActive(false);
    }


    public void onClickUIItem(int index)
    {
        boutique.SetActive(false);
        popUpLoadData(index);
        popUpAchat.SetActive(true);

    }


    public void resume()
    {
        SceneManager.LoadScene("SolarSystem");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

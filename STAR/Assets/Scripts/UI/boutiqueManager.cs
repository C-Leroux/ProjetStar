using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Assets.Scripts
{
    public class boutiqueManager : MonoBehaviour
    {
        private int currentIndexItem;
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
        private GameObject[] textPrices;
        public TMP_Text feedbackText;

        [SerializeField]
        private GameObject titrePopUp;
        [SerializeField]
        private GameObject iconPopUp;
        [SerializeField]
        private GameObject textPopUp;
        [SerializeField]
        private GameObject goldDisplay;


        int currentSelectedItemPrice;


        void Start()
        {
            boutiqueLoadData();
            updateGoldDisplay();
            for(int i=0; i<objectSprite.Length; i++)
            {
                
            }
        }

        private void updateGoldDisplay()
        {
            goldDisplay.GetComponent<TMP_Text>().text = MoneyForMerchant.Instance.GetMoney().ToString();
        }

        private void popUpLoadData(int index)
        {
            iconPopUp.GetComponent<Image>().sprite = items[index].transform.Find("Icon/objectIcon").GetComponent<Image>().sprite;
            string objectName = items[index].transform.Find("Name/ObjectName").GetComponent<TMP_Text>().text;
            currentSelectedItemPrice = objectPrice[index];
            titrePopUp.GetComponent<TMP_Text>().text = "Achat de l'objet : " + objectName;
            textPopUp.GetComponent<TMP_Text>().text = "L'OBJECT COUTE " + currentSelectedItemPrice + " PIECES D'OR";
            currentIndexItem = index;
        }

        private void boutiqueLoadData()
        {
            for (int i = 0; i < items.Length; i++)
            {
                Transform icon = items[i].transform.Find("Icon/objectIcon");
                Image iconImage = icon.GetComponent<Image>();
                iconImage.sprite = objectSprite[i];
                Transform textName = items[i].transform.Find("Name/ObjectName");
                TMP_Text textN = textName.GetComponent<TMP_Text>();
                textN.text = objectName[i];
                Transform textDescription = items[i].transform.Find("Description/Text (TMP)");
                TMP_Text textD = textDescription.GetComponent<TMP_Text>();
                textD.text = objectDescription[i];
                TMP_Text textG = textPrices[i].GetComponent<TMP_Text>();
                textG.text = objectPrice[i].ToString();
            }
        }

        private bool checkIfAbleToBuy()
        {
            return MoneyForMerchant.Instance.GetMoney() >= currentSelectedItemPrice;
        }

        public void onClickBuyItemButton()
        {
            if (checkIfAbleToBuy())
            {
                MoneyForMerchant.Instance.RemoveMoney(currentSelectedItemPrice);
                int augmentation = (int) (objectPrice[currentIndexItem] * 0.2);
                objectPrice[currentIndexItem] = objectPrice[currentIndexItem] + augmentation;
                switch (objectDescription[currentIndexItem])
                {
                    case "Augmentation des points de vie de la base":
                        Base.Instance.AddMaxLp(500);
                        break;
                    case "Augmentation du revenu par seconde":
                        Money.Instance.AddMoneyPerSecond();
                        break;
                    case "Augmentation de la limite d'argent":
                        Money.Instance.AddMaxMoney(250);
                        break;
                    case "Diminution du temps de récupération du pouvoir":
                        Spell.Instance.SetColldownTime();
                        break;
                    case "Augmentation du temps de freeze":
                        Spell.Instance.SetSpellStunTime();
                        break;
                    case "Récupération de la vie":
                        Base.Instance.ResetLp();
                        break;
                }
                TMP_Text textG = textPrices[currentIndexItem].GetComponent<TMP_Text>();
                textG.text = objectPrice[currentIndexItem].ToString();
                feedbackText.gameObject.SetActive(true);
                feedbackText.text = "Vous avez acheté l'objet " + objectName[currentIndexItem];
                StartCoroutine(Wait());


            }
            else
            {
                Debug.Log("pas assez d'argent");
            }
        }
        IEnumerator Wait()
        {
            yield return new WaitForSeconds(4f);

            feedbackText.gameObject.SetActive(false);
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
            GameManager.Instance().ReturnToSolarSystem();
        }

        // Update is called once per frame
        void Update()
        {
            updateGoldDisplay();
        }
    }
}
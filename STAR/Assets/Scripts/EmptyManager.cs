using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts
{
    public class EmptyManager : MonoBehaviour
    {
        public Text MoneyText;
        // Start is called before the first frame update
        void Start()
        {
            MoneyForMerchant.Instance.SetMoneyText(MoneyText);
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}
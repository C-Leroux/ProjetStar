using System.Collections;
using UnityEngine;

namespace Assets.Scripts
{
    public class Merchant : SpaceObject
    {
        public static Merchant CreateMerchant()
        {
            GameObject obj = new GameObject("Merchant");
            Merchant merchant = obj.AddComponent<Merchant>();

            return merchant;
        }

        public void SetSpriteMerchant()
        {
            string path = "Sprites/Solar_system/merchant";
            float scale = 0.1f;

            SetSprite(path, scale);
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
    public class planetController : MonoBehaviour
    {
        private Animator anim; //Declare l'animator
                               // Start is called before the first frame update
        void Start()
        {
            anim = GetComponent<Animator>(); //Declare le component animator
        }

        public void setCompendiumMvt(bool isActive)
        {
            anim.SetBool("fowardCompendium", isActive);
            anim.SetBool("backCompendium", !isActive);

        }
        public void setOptionsMvt(bool isActive)
        {
            anim.SetBool("fowardOptions", isActive);
            anim.SetBool("backOptions", !isActive);

        }
        public void setStatistiquesMvt(bool isActive)
        {
            anim.SetBool("fowardStatistiques", isActive);
            anim.SetBool("backStatistiques", !isActive);

        }
        public void setPlayMvt(bool isActive)
        {
            anim.SetBool("fowardPlayMenu", isActive);
            anim.SetBool("backPlayMenu", !isActive);

        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}
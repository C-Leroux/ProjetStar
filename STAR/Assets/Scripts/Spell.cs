using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
    public class Spell
    {
        private static Spell instance = null;// SINGLETON
        private float spellStunTime;
        private float cooldownTime;
        private float limiteSpellStunTime;
        private float limiteCooldownTime;


        public Spell()
        {
            this.spellStunTime = 0.5f;
            this.cooldownTime = 20;
            this.limiteSpellStunTime = 3;
            this.limiteCooldownTime = 15;
        }

        public static Spell Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new Spell();
                }
                return instance;
            }
        }

        public float GetSpellStunTime()
        {
            return spellStunTime;
        }

        public float GetCooldownTime()
        {
            return cooldownTime;
        }

        public void SetSpellStunTime()
        {
            if (spellStunTime + 0.5 < limiteSpellStunTime)
            {
                spellStunTime = spellStunTime + 0.5f;
            }
            Debug.Log(" spellStunTime " + spellStunTime);
        }

        public void SetColldownTime()
        {
            if(cooldownTime > limiteCooldownTime)
            {
                cooldownTime = cooldownTime - 1;
            }
            Debug.Log("cooldownTime " + cooldownTime);
        }

        //DEBUG
        public void SetColldownTime(float test)
        {
            cooldownTime = test;
            Debug.Log("DEBUG : cooldownTime " + cooldownTime);
        }

        public void SetSpellStunTime(float test)
        {
            spellStunTime = test;
            Debug.Log("DEBUG : spellStunTime " + spellStunTime);
        }

        public string GetInfos()
        {
            return ("Temps d'activité : " + "\n" + spellStunTime + "\n"
                + "Temps de récupération : " + "\n" + cooldownTime);
        }

        public static void Reset()
        {
            instance = null;
        }

    }
}

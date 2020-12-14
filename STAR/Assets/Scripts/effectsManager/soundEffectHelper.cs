using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
    public class soundEffectHelper : MonoBehaviour
    {
        public static soundEffectHelper Instance;

        public AudioClip[] sounds;

        void Awake()
        {
            // On garde une référence du singleton p
            if (Instance != null)
            {
                Debug.LogError("Multiple instances of soundEffectHelper!");
            }

            Instance = this;
        }

        private void Update()
        {

        }
        public void starFire()
        {
            instantiate(sounds[0]);
        }
        public void grenadiereFire()
        {
            instantiate(sounds[1]);
        }
        public void empoisonneuseFire()
        {
            instantiate(sounds[2]);
        }
        public void cryomancienneFire()
        {
            instantiate(sounds[3]);
        }
        public void pyromancienneFire()
        {
            instantiate(sounds[4]);
        }
        public void survolteuseFire()
        {
            instantiate(sounds[5]);
        }
        //public void EnnemiHit( )
        //{
        //   instantiate(sounds[6]);

        //}
        public void baseHit()
        {
            instantiate(sounds[7]);

        }

        public void UIInteraction()
        {
            instantiate(sounds[8]);
        }

        public void destroyEnnemi()
        {
            instantiate(sounds[9]);
        }

        public void buildTower()
        {
            instantiate(sounds[10]);
        }

        public void upgradeTower()
        {
            instantiate(sounds[11]);
        }

        public void sellTower()
        {
            instantiate(sounds[12]);
        }
        public void spellStunUse()
        {
            instantiate(sounds[13]);
        }
        private void instantiate(AudioClip sound)
        {
            AudioClip newAudioClip = Instantiate(
              sound
            ) as AudioClip;
            GameObject audioSourceObject = new GameObject();
            audioSourceObject.AddComponent<AudioSource>();
            AudioSource audioSource = audioSourceObject.GetComponent<AudioSource>();
            audioSource.clip = newAudioClip;
            audioSource.volume = SaveSlider.Instance.GetVolume()/2;
            audioSource.Play();
            StartCoroutine(WaitBeforeUnStun(sound.length, audioSourceObject));
        }

        IEnumerator WaitBeforeUnStun(float stunTime, GameObject audioSourceObject) //On lance de décompte, une fois le stunTime écoulé, la cilbe n'est plus étourdie
        {
            yield return new WaitForSeconds(stunTime);
            GameObject.Destroy(audioSourceObject);
        }

    }
}



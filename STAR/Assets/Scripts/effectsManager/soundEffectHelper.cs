using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class soundEffectHelper : MonoBehaviour
{
    public static soundEffectHelper Instance;

    public AudioClip[] sounds;
    public GameObject effectManager;

    void Awake()
    {
        // On garde une référence du singleton
        if (Instance != null)
        {
            Debug.LogError("Multiple instances of soundEffectHelper!");
        }

        Instance = this;
    }

    private void Update()
    {

    }
    public void starFire( )
    {
        instantiate(sounds[0]);
    }
    public void grenadiereFire( )
    {
        instantiate(sounds[1]);
    }
    public void empoisonneuseFire( )
    {
        instantiate(sounds[2]);
    }
    public void cryomancienneFire( )
    {
        instantiate(sounds[3]);
    }
    public void pyromancienneFire( )
    {
        instantiate(sounds[4]);
    }
    public void survolteuseFire( )
    {
        instantiate (sounds[5]);
    }
    //public void EnnemiHit( )
    //{
     //   instantiate(sounds[6]);

    //}
    public void baseHit( )
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
        effectManager.AddComponent<AudioSource>();
        AudioSource audioSource = effectManager.GetComponent<AudioSource>();
        audioSource.clip = newAudioClip;
        audioSource.Play();
    }
}



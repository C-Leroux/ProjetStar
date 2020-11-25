using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class spellBehaviour : MonoBehaviour
{
    public float spellStunTime;
    public float cooldownTime;
    private float currentTime;
    private bool isSpellAvailable;
    public GameObject spellBackground;

    void Start()
    {
        isSpellAvailable = false;
        currentTime = 0;
    }

    public void spellActivation()
    {
        if (isSpellAvailable)
        {
            GameObject[] allCurrentEnnemis = GameObject.FindGameObjectsWithTag("Ennemi");
            for (int i = 0; i < allCurrentEnnemis.Length; i++)
            {
                allCurrentEnnemis[i].GetComponent<Enemy>().setStunTrue(spellStunTime);
            }
            isSpellAvailable = false;
            currentTime = 0;
        }
        else
        {
            Debug.Log("spell not available");
        }
    }


    void Update()
    {
        if (!isSpellAvailable)
        {
            spellBackground.GetComponent<Image>().fillAmount = currentTime /cooldownTime;
        }
        if(currentTime >=cooldownTime)
        {
            isSpellAvailable = true;
        }
        currentTime = currentTime + Time.deltaTime;
    }
}

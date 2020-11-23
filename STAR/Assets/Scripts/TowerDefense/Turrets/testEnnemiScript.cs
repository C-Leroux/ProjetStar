using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class testEnnemiScript : MonoBehaviour
{
    public float speed;
    public float life;
    private float maxLife;
    public bool poisonned;
    public bool stun;
    public Image healthbar;

    void Start()
    {
        poisonned = false;
        stun = false;
        maxLife = life;
    }
    public void updateHealthBarColor(float life)
    {
        float lifeRatio = life / maxLife;
        if (lifeRatio < 0.6)
        {
            healthbar.color = new Color(255, 165, 0); //ORANGE
        }
        if (lifeRatio < 0.25)
        {
            healthbar.color = Color.red;
        }
    }
    public void looseLife(float damage)
    {
        this.life = this.life - damage;
        healthbar.fillAmount = life / maxLife;
    }
    //Poisonned Methods
    public bool getPoisonned()
    {
        return poisonned;
    }
    public void setPoisonnedTrue(float damage, float poisonnedTime) //La cible est empoisonnée
    {
        poisonned=true;
        StartCoroutine(WaitBeforeUnPoisonned(poisonnedTime));
        StartCoroutine(WaitBeforeDealDamagePoisonned(damage, poisonnedTime));
    }
    IEnumerator WaitBeforeUnPoisonned(float poisonnedTime) //On lance de décompte, une fois le poisonnedTime écoulé, la cilbe n'est plus empoisonnée
    {
        yield return new WaitForSeconds(poisonnedTime);
        poisonned=false;
    }
    
    IEnumerator WaitBeforeDealDamagePoisonned(float damage, float poisonnedTime)
    {
        for (int i = 0; i < poisonnedTime; i++)
        {
            looseLife(damage);
            Debug.Log("DAMAGE TAKEN FROM EMPOISONNEUSE EACH SECOND");
            yield return new WaitForSeconds(1f); //On attend une seconde, puis on fait les dégats du poisson
        }
    }
   

    //Stun Methods
    public bool getStun()
    {
        return stun;
    }
    public void setStunTrue(float stunTime) //La cible est étourdie
    {
        stun=true;
        StartCoroutine(WaitBeforeUnStun(stunTime));
    }

    IEnumerator WaitBeforeUnStun(float stunTime) //On lance de décompte, une fois le stunTime écoulé, la cilbe n'est plus étourdie
    {
        yield return new WaitForSeconds(stunTime);
        stun=false;
    }

    public void changeSpriteColor()
    {
        if (stun && !poisonned)
        {
            this.GetComponent<SpriteRenderer>().color = Color.blue;
        }
        if (!stun && poisonned) 
        {
                this.GetComponent<SpriteRenderer>().color = Color.green;
        }
        if(stun && poisonned)
        {
            this.GetComponent<SpriteRenderer>().color = Color.cyan;
        }
        if(!stun && !poisonned)
        {
            this.GetComponent<SpriteRenderer>().color = Color.white;
        }
    }

    void Update()
    { 
        if (!stun)
        {
            this.transform.position = this.transform.position + new Vector3(1f * speed, 0, 0);
        }
        //Changing Color sprite according to ennemi state
        updateHealthBarColor(this.life);
        changeSpriteColor();
        if (life <= 0)
        {
            GameObject.Destroy(this.gameObject);
        }
    }
}

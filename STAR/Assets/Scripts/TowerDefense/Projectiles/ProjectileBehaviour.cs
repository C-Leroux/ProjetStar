using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileBehaviour : MonoBehaviour
{
    private string projectileName;
    private string turretLinkedName;
    private float damage;
    private float scale;
    private int explosionRange;
    private float poisonnedTime;
    private float stunTime;
    private Enemy ennemi; 
    //testEnnemiScript temporaire à remplacer avec la classe ennemi, ici on vient rechercher la methode looseLife notamment 



    private Vector3 dead_target;
    private Collider2D target;

    public void init(Projectile projectile)
    {
        poisonnedTime = 4;//TIME EMPOISONNEUSE
        explosionRange =1;//RANGE GRENADIERE EXPLOSION
        stunTime = 1f; //TIME STUN
        this.ennemi = projectile.ennemi;
        this.scale = projectile.scale;
        this.damage = projectile.damage;
        this.projectileName = projectile.projectileName;
        this.turretLinkedName = projectile.turretLinkedName;
        this.GetComponent<SpriteRenderer>().sprite = projectile.sprite;
        this.transform.localScale = new Vector3(scale, scale, scale);
    }
    public void initFire(Collider2D target)
    {
        this.target = target;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag != "Ennemi")
        {
            return;
        }
        if (collision.gameObject.tag == "Ennemi" && collision == target)
        {
            switch (this.turretLinkedName)
            {
                case "STAR":
                    dealDamageTo(collision, this.damage); //La tourelle tire sur une cible sans effet excepté les dégats
                    //Debug.Log("DAMAGE TAKEN FROM STAR");
                    GameObject.Destroy(this.gameObject);
                    break;
                case "Grenadiere":
                    Collider2D[] targets = getEnnemisInExplosionRange();//La tourelle tire sur une cible, et affecte des dégats dans la zone de l'explosion
                    for (int i = 0; i < targets.Length; i++)
                    {
                        if (targets[i].tag == "Ennemi")
                        {
                            dealDamageTo(targets[i], this.damage);
                            //Debug.Log("DAMAGE TAKEN FROM GRENADIERE");
                        }
                    }
                    GameObject.Destroy(this.gameObject);
                    break;
                case "Pyromancienne":
                    dealDamageTo(collision, this.damage);//La tourelle tire lentement mais faut beaucoup de dégats. Plus de travail sur l'animation                
                    //Debug.Log("DAMAGE TAKEN FROM Pyromancienne");
                    GameObject.Destroy(this.gameObject);
                    break;
                case "Survolteuse":
                    dealDamageTo(collision, this.damage);//Je ne sais pas comment faire l'effet pour l'instant
                    //Debug.Log("DAMAGE TAKEN FROM Survolteuse");
                    GameObject.Destroy(this.gameObject);
                    break;
                case "Cryomancienne":
                    collision.GetComponent<Enemy>().setStunTrue(stunTime);//La tourelle stun l'ennemi, et lui inflige des dégats
                    dealDamageTo(collision, this.damage);
                    //Debug.Log("DAMAGE TAKEN FROM Cryomancienne");
                    GameObject.Destroy(this.gameObject);
                    break;
                case "Empoisonneuse"://La tourelle empoisonne l'ennemi sur 4 secondes, attention les damages indiqué pour ce project sont les damage par secondes
                    collision.GetComponent<Enemy>().setPoisonnedTrue(this.damage ,poisonnedTime); //La cible est empoisonnée
                    //Damage per second de l'empoisonnement
                    GameObject.Destroy(this.gameObject);
                    break;
            }
        }
        
    }

    public Collider2D[] getEnnemisInExplosionRange()
    {
        return Physics2D.OverlapCircleAll(this.transform.position, explosionRange);
    }


    public void dealDamageTo(Collider2D collision, float damage)
    {
        collision.GetComponent<Enemy>().TakeDamages(damage);
    }
   
    
    void Update()
    {
        if (target != null)
        {
            dead_target = target.transform.position;
        }
        transform.position = transform.position +
           ((target == null ? dead_target : target.transform.position) - transform.position) * Time.deltaTime * 10f;
       if (target==null)
        {
            GameObject.Destroy(this.gameObject);
        }

    }


}

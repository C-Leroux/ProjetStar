using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileBehaviour : MonoBehaviour
{
    private string projectileName;
    private string turretLinkedName;
    private Sprite sprite;


    private Vector3 dead_target;
    private Transform target;

    public void init(Projectile projectile)
    {
        this.projectileName = projectile.projectileName;
        this.turretLinkedName = projectile.turretLinkedName;
        this.GetComponent<SpriteRenderer>().sprite = projectile.sprite;
    }
    public void initFire(Transform target)
    {
        this.target = target;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag != "Ennemi")
        {
            return;
        }
        GameObject.Destroy(this.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        if (target != null)
        {
            dead_target = target.position;
        }
        transform.position = transform.position +
           ((target == null ? dead_target : target.position) - transform.position) *
           Time.deltaTime * 10f;


    }


}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[CreateAssetMenu]
public class Turret : ScriptableObject
{
    public new string name;
    public int cost;
    public float scale;
    public float range;
    public float attack_speed;
    public Sprite sprite;
    public float readyTime;
    public ProjectileBehaviour projectileTemplate;
    public Projectile projectileToSpawn;
}

﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[CreateAssetMenu]
public class Projectile : ScriptableObject
{
    public string projectileName;
    public string turretLinkedName;
    public float damage;
    public Sprite sprite;
    public testEnnemiScript ennemi;
}
using System.Collections;
using System.Collections.Generic;
using System;

[Serializable]
public class Weapon : Loot //class for weapon that are a type of loot
{
    public readonly float dmg; //how much damage it deals
    public readonly float atkSpd; //intervales between attacks
    public readonly float bulletSpeed; //how fast the bullet goes
    public static readonly string bulletPrefabPath = "DefaultBullet"; //path to prefab that contains bullet specific components
    public readonly string bulletMeshPath; //path to bullet mesh

    public Weapon(Loot l, float _dmg, float _atkSpd, float _bulletSpeed, string _mesh) : base(l.spritePath, l.meshPath, l.name, l.amount, l.stack, l.weight, l.weapon, l.empty, l.num) //constructor to create weapons
    {
        dmg = _dmg;
        atkSpd = _atkSpd;
        bulletSpeed = _bulletSpeed;
        bulletMeshPath = _mesh;
    }
}

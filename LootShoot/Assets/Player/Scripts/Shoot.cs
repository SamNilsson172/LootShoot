using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour //lets you shoot when you have a weapon in your hand
{
    private void Start()
    {
        Inventory.Add(AllLoot.Glock()); //adds a test gun to you inventory, to test shooting
    }

    float timer = float.MaxValue; //time since your last shot
    // Update is called once per frame
    void FixedUpdate()
    {
        timer += Time.deltaTime; //increase the timer
        if (Inventory.loots.Count > 0) //if a loot instance is in your hand
        {
            if (Inventory.loots[0].weapon && Input.GetButton("Fire1")) //if a weapon is in hand and your pressing the shoot button
            {
                Weapon weapon = (Weapon)Inventory.loots[0]; //convert loot to weapon
                if (timer >= weapon.atkSpd) //check if your allowed to shoot according to attack speed
                {
                    timer = 0; //reset timer to wait for next attack
                    SpawnBullet(weapon); //spawn a bullet
                }
            }
        }
    }

    void SpawnBullet(Weapon weapon) //spawn a bullet and asign its values
    {
        GameObject bullet = Instantiate(Resources.Load<GameObject>(Weapon.bulletPrefabPath));
        bullet.transform.position = transform.position;
        bullet.transform.rotation = transform.rotation;
        BulletBehaviour bulletBehaviour = bullet.GetComponent<BulletBehaviour>();
        bulletBehaviour.dmg = weapon.dmg;
        bulletBehaviour.speed = weapon.bulletSpeed;
        bulletBehaviour.GetComponent<MeshFilter>().sharedMesh = Resources.Load<GameObject>(weapon.bulletMeshPath).GetComponent<MeshFilter>().sharedMesh;
        bulletBehaviour.GetComponent<MeshRenderer>().sharedMaterial = Resources.Load<GameObject>(weapon.bulletMeshPath).GetComponent<MeshRenderer>().sharedMaterial;
    }
}

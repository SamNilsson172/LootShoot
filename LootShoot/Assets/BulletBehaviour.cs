using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class BulletBehaviour : MonoBehaviour
{
    public float speed = 1; //how fast is the bullet going
    public float dmg; //how much damage will the bullet deal
    float timer; //timer for how long the bullet will live
    bool alreadyDead = false; //if the raycast predicts that it will hit something
    Vector3 dieHere; //position the bullet will hit somthing at
    Vector3 dir; //direction the bullet is traveling

    // Update is called once per frame
    void FixedUpdate()
    {
        timer += Time.deltaTime;
        if (timer > 10) //destroy bullet if it lives longer than 10 seconds
            Destroy(gameObject);

        if (Physics.Raycast(transform.position, transform.forward, out RaycastHit hit, speed * Time.deltaTime + speed / 20) && !alreadyDead) //will the bullet hit something this frame and has not already hit something
        {
            alreadyDead = true; //the bullet will soon hit somthing
            dieHere = hit.point; //the point the bullet will hit something in
            dir = hit.point - transform.position; //the direction the bullet is traveling in

            if (hit.transform.gameObject.tag == "Enemy") //if it hits an enemy
            {
                hit.transform.gameObject.GetComponent<EnemyBehaviour>().Hp -= dmg; //damage the enemy
                Vector3 knockback = dir * dmg / hit.transform.gameObject.GetComponent<EnemyBehaviour>().thisEnemy.hp;
                hit.transform.gameObject.GetComponent<Rigidbody>().AddForce(knockback * 100);
                Debug.Log(knockback * 100);
            }
        }

        transform.localPosition += transform.forward * speed * Time.deltaTime; //go forward

        if (alreadyDead)
            if (PassedDeadPoint())
                Destroy(gameObject); //kill the bullet
    }

    bool PassedDeadPoint() //if bullet has passed the point of collision
    {
        bool passed = false;

        if (dir.x > 0) //if traveling towords a bigger value
            passed = transform.position.x > dieHere.x; //if it has passed the collision point

        if (dir.x < 0) //if traveling towords a smaller value
            passed = transform.position.x < dieHere.x; //if it has passed the collision point

        if (dir.y > 0) //if traveling towords a bigger value
            passed = transform.position.y > dieHere.y; //if it has passed the collision point

        if (dir.y < 0) //if traveling towords a smaller value
            passed = transform.position.y < dieHere.y; //if it has passed the collision point

        if (dir.z > 0) //if traveling towords a bigger value
            passed = transform.position.z > dieHere.z; //if it has passed the collision point

        if (dir.z < 0) //if traveling towords a smaller value
            passed = transform.position.z < dieHere.z; //if it has passed the collision point

        return passed;
    }
}

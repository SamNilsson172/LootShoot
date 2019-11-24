using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehaviour : MonoBehaviour
{
    public float speed; //how fats id the bullet going
    public float dmg; //how much damage will the bullet deal
    float timer; //timer for how long the bullet will live


    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer > 10) //destroy bullet if it lives longer than 10 seconds
            Destroy(gameObject);

        transform.Translate(Vector3.forward * speed * Time.deltaTime, Space.Self); //go forward
        if (Physics.Raycast(transform.position, transform.forward, out RaycastHit hit, speed * Time.deltaTime + speed * .1f)) //will the bullet hit something this frame
        {
            if (hit.transform.gameObject.tag == "Enemy") //if it hits an enemy
                hit.transform.gameObject.GetComponent<EnemyBehaviour>().Hp -= dmg; //damage the enemy
            Destroy(gameObject); //kill the bullet
        }
    }
}

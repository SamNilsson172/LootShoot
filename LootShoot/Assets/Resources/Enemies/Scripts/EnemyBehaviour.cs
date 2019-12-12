using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    GameObject player;

    public Enemy thisEnemy;
    float hp; //variable for enemy hp
    public float Hp
    {
        get
        {
            return hp;
        }
        set
        {
            if (value <= 0) // if hp is less than 0
            {
                thisEnemy.Drop(); //drop loot when dead
                gameObject.SetActive(false); //don't destroy it to acces its components
                hp = 0; //set hp to 0 so it won't be negative
            }
            else hp = value;

        }
    }

    private void Start()
    {
        player = GameObject.Find("Player");
        thisEnemy = GetComponent<Enemy>(); //find enemy instance for values
        hp = thisEnemy.hp; //set hp on start
    }

    //private void Update()
    //{
    //    transform.position = Vector3.MoveTowards(transform.position, player.transform.position, 3 * Time.deltaTime); //move to player
    //}
}

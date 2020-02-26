using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WhatImILookingAt : MonoBehaviour //defines what your looking at and lets you pick up loot
{
    float timer = 0;
    public LayerMask layerMask;
    bool loot = false;
    bool enemy = false;
    bool menu = false;
    public Text lootText;
    public Text enemyText;
    public Text enemyHp;
    public Image image;
    public CanvasGroup enemyCanvas;
    RaycastHit hit;
    LootInstance lootLookingAt;
    EnemyBehaviour enemyBehaviour;
    OpenMenu menuObj;

    Inventory playerInv;
    private void Start()
    {
        playerInv = GameObject.Find("Player").GetComponent<InventoryInstance>().myInv;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer > .1f)
        {
            loot = false;
            enemy = false;
            menu = false;
            timer = 0;
            if (Physics.Raycast(transform.position, transform.forward, out hit, 25, layerMask))
            {
                if (hit.transform.tag == "Loot")
                {
                    lootLookingAt = hit.transform.gameObject.GetComponent<LootInstance>();
                    loot = true;
                }
                if (hit.transform.tag == "Enemy")
                {
                    enemyBehaviour = hit.transform.gameObject.GetComponent<EnemyBehaviour>();
                    enemy = true;
                }
                if (hit.transform.CompareTag("Menu"))
                {
                    menuObj = hit.transform.gameObject.GetComponent<OpenMenu>();
                    menu = true;

                }
            }
        }

        if (loot)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                loot = false;
                lootLookingAt.AddToInventory(playerInv);
            }
            lootText.text = lootLookingAt.name + "\r\n" + lootLookingAt.amount + " x" + "\r\n" + "Press E";
        }
        else lootText.text = "";

        if (enemy && enemyBehaviour != null)
        {
            enemyText.text = enemyBehaviour.name;
            enemyHp.text = enemyBehaviour.Hp + "/" + enemyBehaviour.thisEnemy.hp;
            image.transform.localScale = new Vector3(enemyBehaviour.Hp / enemyBehaviour.thisEnemy.hp, 1);
            enemyCanvas.alpha = 1;
        }
        else
        {
            enemyText.text = "";
            enemyHp.text = "";
            enemyCanvas.alpha = 0;
        }

        if (menu)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                print("open");
                menuObj.Open(true);
            }
            lootText.text = menuObj.name + "\r\n" + "Press E";
        }
        else lootText.text = "";
    }
}

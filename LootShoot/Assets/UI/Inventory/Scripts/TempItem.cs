using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TempItem : MonoBehaviour //script for swapLootSlot
{
    public InventoryInstance Ii;
    Inventory playerInv;

    private void Awake()
    {
        playerInv = GameObject.Find("Player").GetComponent<InventoryInstance>().myInv;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Input.mousePosition; //have ui element follow mouse
    }

    public void UpdateText()
    {
        Text text = GetComponentInChildren<Text>();
        if (playerInv.swapLoot.empty) text.text = ""; //set text to nothing if swap loot is empty
        else text.text = playerInv.swapLoot.name; //if not empty set it to swapLoots name

    }
}

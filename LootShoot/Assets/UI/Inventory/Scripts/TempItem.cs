using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TempItem : MonoBehaviour //script for swapLootSlot
{

    // Update is called once per frame
    void Update()
    {
        transform.position = Input.mousePosition; //have ui element follow mouse
    }

    public void UpdateText()
    {
        Text text = GetComponentInChildren<Text>();
        if (Inventory.swapLoot.empty) text.text = ""; //set text to nothing if swap loot is empty
        else text.text = Inventory.swapLoot.name; //if not empty set it to swapLoots name

    }
}

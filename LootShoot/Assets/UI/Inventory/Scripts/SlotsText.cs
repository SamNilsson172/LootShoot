using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SlotsText : MonoBehaviour
{
    public Text text;
    public InventoryInstance Ii;
    Inventory playerInv;

    private void Start()
    {
        playerInv = Ii.myInv;
    }

    // Update is called once per frame
    void Update()
    {
        text.text = playerInv.loots.Count + "/" + playerInv.slots + " slots used";
    }
}

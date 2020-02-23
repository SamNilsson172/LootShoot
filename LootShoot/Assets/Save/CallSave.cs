using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CallSave : MonoBehaviour
{
    public OpenInventory inventory;
    public GameObject player;
    public void Save()
    {
        player.GetComponent<SaveFunctions>().Save(player.GetComponent<InventoryInstance>().myInv); ; //save when pressing save button
        inventory.Show(); //close inventory after saving
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CallLoad : MonoBehaviour
{
    public OpenInventory inventory;
    public void Load()
    {
        SaveFunctions.Load(); //load save file
        GameObject[] lootInstances = GameObject.FindGameObjectsWithTag("Loot"); //find all loot instances
        foreach (GameObject g in lootInstances) //remove all loot game objects for no duplication, commentify for testing
        {
            Destroy(g);
        }
        inventory.Show(); //close after saving, instead of updating inventory UI
    }
}

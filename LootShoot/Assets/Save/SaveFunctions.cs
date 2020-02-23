using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveFunctions : MonoBehaviour
{
    public void Save(Inventory inv)
    {
        Debug.Log("Saved");
        SaveFile saveFile = new SaveFile(inv.loots); //create a new savefile storing the inventory content
        Serialization.Save(saveFile); //save the savefile to the system
    }

    public void Load(Inventory inv)
    {
        List<Loot> newLoots = Serialization.Load().loots; //get savefile from computer

        if (newLoots != null) //if savefile exists
        {
            Debug.Log("Loaded");
            inv.loots = newLoots; //get the content of inventory from savefile
        }
        else
        {
            Save(inv); //if savefile does not exist create it
        }
    }
}

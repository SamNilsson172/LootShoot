using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SaveFunctions
{
    public static void Save()
    {
        Debug.Log("Saved");
        SaveFile saveFile = new SaveFile(Inventory.loots); //create a new savefile storing the inventory content
        Serialization.Save(saveFile); //save the savefile to the system
    }

    public static void Load()
    {
        List<Loot> newLoots = Serialization.Load().loots; //get savefile from computer

        if (newLoots != null) //if savefile exists
        {
            Debug.Log("Loaded");
            Inventory.loots = newLoots; //get the content of inventory from savefile
        }
        else
        {
            Save(); //if savefile does not exist create it
        }
    }
}

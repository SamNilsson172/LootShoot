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
        Debug.Log("Loaded");
        Inventory.loots = Serialization.Load().loots; //get the content of inventory from savefile
    }
}

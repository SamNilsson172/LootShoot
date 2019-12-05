using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CallSave : MonoBehaviour
{
    public OpenInventory inventory;
    public void Save()
    {
        SaveFunctions.Save(); //save when pressing save button
        inventory.Show(); //close inventory after saving
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryInstance : MonoBehaviour
{
    public Inventory myInv;

    private void Awake()
    {
        myInv = new Inventory();
    }
}

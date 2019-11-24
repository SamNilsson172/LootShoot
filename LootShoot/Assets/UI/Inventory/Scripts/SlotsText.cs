using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SlotsText : MonoBehaviour
{
    public Text text;

    // Update is called once per frame
    void Update()
    {
        text.text = Inventory.loots.Count + "/" + Inventory.slots + " slots used";
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemButton : MonoBehaviour
{
    public int index; //what index it has in inventory
    public Text text;
    public Image image;

    private void Start()
    {
        
    }

    public void UpdateText() //updates text
    {
        if (Inventory.loots.Count > 0) //if inventory contains something, if the text reads button somthings fucked
        {
            if (!Inventory.loots[index].empty)
            {
                text.text = Inventory.loots[index].name + "\r\n" + Inventory.loots[index].amount + " x"; //if item in not empty, set the text to the items name
                image.sprite = Resources.Load<Sprite>(Inventory.loots[index].spritePath);
            }
            else
            {
                text.text = "Empty"; //if it's empty set it to empty
                image.sprite = null;
            }
        }
    }

    public void Swap() //swaps stuff
    {
        Inventory.Swap(index);
        GameObject.Find("Inventory").GetComponent<OpenInventory>().MasterUpdate(); //update all text when swap has occured
    }
}

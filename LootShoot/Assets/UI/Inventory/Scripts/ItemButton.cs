using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemButton : MonoBehaviour
{
    public int index; //what index it has in inventory
    public Text text;
    public Image image;
    Inventory playerinv;

    private void Start()
    {
        print(GameObject.Find("Player").name);
        playerinv = GameObject.Find("Player").GetComponent<InventoryInstance>().myInv;
    }

    public void UpdateText() //updates text
    {
        if (playerinv.loots.Count > 0) //if inventory contains something, if the text reads button somthings fucked
        {
            if (!playerinv.loots[index].empty)
            {
                text.text = playerinv.loots[index].name + "\r\n" + playerinv.loots[index].amount + " x"; //if item in not empty, set the text to the items name
                image.sprite = Resources.Load<Sprite>(playerinv.loots[index].spritePath);
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
        if (playerinv.loots.Count > 0) //hand buton still exists even if inventory is empty, so make sure it's not to swap
        {
            playerinv.Swap(index);
            GameObject.Find("Inventory").GetComponent<OpenInventory>().MasterUpdate(); //update all text when swap has occured
        }
    }
}

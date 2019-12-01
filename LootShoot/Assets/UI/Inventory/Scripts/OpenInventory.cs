using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenInventory : MonoBehaviour //handels gameobjects in inventory
{
    public Move move; //players move script, pause when inventory is open
    public Shoot shoot; //players shoot script, pause when inventory is open
    public CanvasGroup canvas; //get canvas group to make UI stuff invisible
    bool open; //if inventory is visable  or not
    public GameObject itemButton; //prefab for inventory buttons
    public GameObject HandSlot; //prefab for hand button
    public GameObject TempSlot; // prefan for sawp loot nutton
    GameObject currentHand; //save current hand button to update its text and remove it
    GameObject currentTemp; //same as above

    private void Start()
    {
        Inventory.swapLoot = AllLoot.Empty(); //set swap loot to empty for safety
        OpenOrClose(false); //have inventory closed when starting
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab)) //open and close inventory on tab, use get button for better customization
        {
            open = !open; //change between open and close
            OpenOrClose(open); //open or close it depending on open variable
        }
    }

    void OpenOrClose(bool open) //change values depending on if inventory is closed or open
    {
        canvas.alpha = open ? 1 : 0; //set visability 
        canvas.interactable = open; //set interactability
        canvas.blocksRaycasts = open; //set raycast blockage
        Cursor.visible = open; //set cursor visability
        Cursor.lockState = open ? CursorLockMode.None : CursorLockMode.Locked; //set if cursor is movable
        move.enabled = !open; //enable and diasble scripts
        shoot.enabled = !open;

        if (open)
        {
            LoadItems(); //create buttons
            MasterUpdate(); //update buttons text
            Time.timeScale = 0;
        }
        if (!open)
        {
            RemoveItems(); //remove all items
            Time.timeScale = 1;
        }

        if (!open && !Inventory.swapLoot.empty) Inventory.Drop(Inventory.swapLoot.num, Inventory.swapLoot.amount, true, 0, GameObject.Find("Player").transform); //drop loot in swap loot slot if it's not empty

        Inventory.swapLoot = AllLoot.Empty(); // set swap loot to empty for safety
        Inventory.RemoveEmpties(); //remove all empty slots in inventory
    }

    void LoadItems() //create all buttons and link the to their coresponding item
    {
        currentHand = Instantiate(HandSlot); //create hand item
        currentHand.transform.SetParent(transform); //set parent for acces
        currentHand.transform.localPosition = Vector3.zero;

        currentTemp = Instantiate(TempSlot); //same as above for swap loot
        currentTemp.transform.SetParent(transform);

        for (int i = 1; i < Inventory.loots.Count; i++) //loop for each item in inventory, besides the first one which is the hand slot
        {
            GameObject newButt = Instantiate(itemButton); //create the gameobject
            newButt.transform.SetParent(transform.GetChild(0)); //give it the correct parent for sorting
            ItemButton itemButtonScript = newButt.GetComponent<ItemButton>();
            itemButtonScript.index = i; // link it to item by giving it it's index in inventory
        }
    }

    void RemoveItems() //removes all buttons
    {
        Destroy(currentTemp);
        Destroy(currentHand);
        for (int i = 0; i < transform.GetChild(0).childCount; i++)
        {
            Destroy(transform.GetChild(0).GetChild(i).gameObject);
        }
    }

    public void MasterUpdate() //updates all text 
    {
        currentHand.GetComponent<ItemButton>().UpdateText();
        currentTemp.GetComponent<TempItem>().UpdateText();
        for (int i = 0; i < transform.GetChild(0).childCount; i++)
            transform.GetChild(0).GetChild(i).GetComponent<ItemButton>().UpdateText();
    }
}

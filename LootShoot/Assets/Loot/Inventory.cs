using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory 
{
    public List<Loot> loots = new List<Loot>(); //list for all loot, use list to easily add and remove stuff, would be smarter to use arrays cuz now you can acces loots.Add from other scripts
    public Loot swapLoot = AllLoot.Empty(); //a slot to put loot in when swaping placements in inventory
    public int slots = 10; //how many loot slots the inventory contains

    public bool Add(Loot loot) //adds given loot to inventory, returns bool to know if it worked or not for error messages
    {
        bool added = false;

        foreach (Loot l in loots) //loops for all loot in inventory
            if (l.name == loot.name && l.amount < l.stack) //if same type of loot already exists in inventory and is not at its max capacity
            {
                if (l.amount + loot.amount > l.stack) //if adding the amounts together would be to big
                {
                    loot.amount -= l.stack - l.amount; //amount of loot to add get subtracted by how much that will be added to already existing loot
                    l.amount = l.stack; //set already existing loot to max capacity
                }
                else //if you can add the amounts together
                {
                    l.amount += loot.amount; //do that
                    added = true;
                }
            }

        if (loots.Count < slots && !added) //if inventory is not full and loot has not alreasy been added
        {
            loots.Add(loot); //add given loot to inventory
            added = true;
        }
        return added; //adding didn't work
    }

    public bool Remove(Loot loot) //removes given loot from inventory, returns bool to know if it worked or not for error messages
    {
        if (loots.Contains(loot)) //if given loot is in inventory 
        {
            loots.Remove(loot); //remove given loot from inventory
            return true; //removing worked
        }
        return false; //removing didn't work
    }

    public void Swap(int index) //swaps item between temploot slot and the slot you clicked
    {
        Loot temp = null; //variable to temporarily store loot in, set to null to avoid unassigned variable error, only temp 
        if (!swapLoot.weapon) temp = NewInstance(swapLoot); //set temp to a copy of the swap loot item
        if (swapLoot.weapon) temp = NewWeaponInstance((Weapon)swapLoot); //same as above but for weapons

        if (!loots[index].weapon) swapLoot = NewInstance(loots[index]); //set swap loot slot to a copy the clicked item
        if (loots[index].weapon) swapLoot = NewWeaponInstance((Weapon)loots[index]); //same as aboe but for weapons

        loots[index] = temp; //set clicked item slot to temp variable
    }

    public void Drop(int lootNum, int amount, bool swapItem, int index, Transform dropPos) //moves an item from inventory to game world, needs loot num to identify it, swap bool to know if it's in loot or swapLoot slot, needs index if it's not and a transform to know where to drop it
    {
        LootSpawner.SpawnLoot(lootNum, amount, dropPos); //spawns the loot
        if (swapItem) swapLoot = AllLoot.Empty(); //remove it from swapLoot slot
        if (!swapItem) Remove(loots[index]); //currently unused, but the idea is the you can drop it directly from you inventory with a keyboard shortcut, remove it from inventory
    }

    public void RemoveEmpties() //if an item is empty, decrease the size of the list, only needed when closing inventory
    {
        foreach (Loot l in loots.ToArray()) //check if any loot in loots is empty, if it is remove it
        {
            if (l.empty) Remove(l);
        }
    }

    Loot NewInstance(Loot oldLoot) => new Loot(oldLoot.spritePath, //method to copy an item
        oldLoot.meshPath,
        oldLoot.name,
        oldLoot.amount,
        oldLoot.stack,
        oldLoot.weight,
        oldLoot.weapon,
        oldLoot.empty,
        oldLoot.num);

    Weapon NewWeaponInstance(Weapon oldWeapon) => new Weapon(NewInstance(oldWeapon), //method to copy a wepon
        oldWeapon.dmg,
        oldWeapon.atkSpd,
        oldWeapon.bulletSpeed,
        oldWeapon.bulletMeshPath);
}

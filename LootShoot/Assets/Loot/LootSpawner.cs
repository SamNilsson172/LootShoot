using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootSpawner : MonoBehaviour
{
    public static GameObject SpawnLoot(int lootNum, int amount, Transform spawnPos) //instanciates loot game objcts and assigns their needed values, returns game object for forgotten reasons
    {
        GameObject lootObj = Instantiate(Resources.Load<GameObject>(Loot.prefabPath)); //instanciates a defalut loot 
        lootObj.transform.position = spawnPos.position; //give it the given position
        LootInstance newLoot = lootObj.GetComponent<LootInstance>(); //acces the loot instance script
        newLoot.lootNum = lootNum; //give values that can create the loot instance
        newLoot.amount = amount;
        newLoot.DefineMe(); //give the loot it's apperance and instanciate loot
        return lootObj;
    }

    public static Loot GetLootFromNum(int num, int amount)
    {
        if (num == 0) return AllLoot.Coin(amount);
        if (num == 1) return AllLoot.Glock();
        if (num == 2) return AllLoot.Musket();
        if (num == 3) return AllLoot.Svante();

        Debug.LogError("Number for loot dosn't exist");
        return null;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int hp;
    public int[] drops; //given in lootnum
    public int[] dropRates; // chances form 1-100
    public int[] dropAmount; //how much loot to drop
    public void Drop() //drop loot 
    {
        for (int i = 0; i < drops.Length; i++) //loop for all possible drop chances 
        {
            if (Random.Range(1, 101) <= dropRates[i]) LootSpawner.SpawnLoot(drops[i], dropAmount[i], transform); //spawn the loot if RNJ says so
        }
    }
}

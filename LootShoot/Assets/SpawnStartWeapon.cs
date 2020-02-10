using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnStartWeapon : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        LootSpawner.SpawnLoot(1, 1, transform);
    }

    // Update is called once per frame
    void Update()
    {

    }
}

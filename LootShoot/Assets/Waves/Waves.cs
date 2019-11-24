using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waves : MonoBehaviour
{
    public GameObject chicken; //prefab of enemies to spawn
    readonly int[] enemiesInWave = { 1, 2, 3, 4, 5 }; //how many enemies that are in each wave
    int currentWave; //what wave your currently on
    readonly List<EnemyBehaviour> currentWaveEnemies = new List<EnemyBehaviour>(); //list of all enemies this turn

    // Start is called before the first frame update
    void Start()
    {
        StartNewWave(); //start first wave
    }

    // Update is called once per frame
    void Update()
    {
        if (AllDead()) //check if all enemies in wave are dead
        {
            currentWaveEnemies.Clear(); //clear enemies from previouse wave
            currentWave++; //go to next wave
            StartNewWave(); //start next wave
        }
    }

    void StartNewWave() 
    {
        for (int i = 0; i < enemiesInWave[currentWave]; i++) //loop for all enemies in this turn
            currentWaveEnemies.Add(Instantiate(chicken).GetComponent<EnemyBehaviour>()); //add all enemies to a list
    }

    bool AllDead() //returns bool to know if all enemies are dead
    {
        float totHp = 0; //total hp of all enemies in this wave
        foreach (EnemyBehaviour e in currentWaveEnemies) //loop for all enemies in this wave
            totHp += e.Hp; //add up all enemies hp
        return totHp <= 0; //all eneies are dead if total hp is less than 0
    }
}

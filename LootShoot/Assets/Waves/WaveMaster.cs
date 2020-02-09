using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaveMaster : MonoBehaviour
{
    public bool hard;
    public bool normal; //bools that define what waves that will spawn
    public bool easy;
    public Transform[] spawnPoints; //points where anemies can spawn
    public GameObject exit;

    Wave[] waves; //array for the enemies that will spawn
    int currentWave = 0; //index for waves, syays what wave your on
    List<GameObject> spawnedEnemies = new List<GameObject>(); //the enemies that have been spawned

    public Text waveNum; //show the player what wave they're on 

    float timer = 0;

    private void Start()
    {
        if (easy) //create waves depending on what bool is true
        {
            waves = new Wave[1];
            waves[0] = AllWaves.Wave1();
        }
        if (normal)
        {
            waves = new Wave[2];
            waves[0] = AllWaves.Wave1();
            waves[1] = AllWaves.Wave2();
        }
        if (hard)
        {
            waves = new Wave[3];
            waves[0] = AllWaves.Wave1();
            waves[1] = AllWaves.Wave2();
            waves[2] = AllWaves.Wave3();
        }

        StartNewWave(); //starts first wave
    }

    int currentToActivate; //what enemy in the wave to spawn now, needs to be rememberd if not spawn all enemies at once
    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer > 5) //evety 5 seconds
        {
            timer = 0;
            if (currentToActivate > 0) SpawnEnemies(); //if already spawning enemies for wave
            else if (AllDead()) //if you should start a new wave
            {
                currentWave++; //go to new wave
                StartNewWave(); //start the new wave
            }
        }
    }

    void SpawnEnemies()
    {
        foreach (Transform t in spawnPoints)
        {
            spawnedEnemies.Add(Instantiate(waves[currentWave].enemies[currentToActivate]));
            waves[currentWave].enemies[currentToActivate].transform.position = t.position;
            currentToActivate++;
            if (currentToActivate >= waves[currentWave].enemies.Length)
            {
                currentToActivate = 0;
                break;
            }
        }
    }

    void StartNewWave()
    {
        foreach (GameObject g in spawnedEnemies) Destroy(g);
        spawnedEnemies.Clear();
        if (currentWave >= waves.Length) //you won
        {
            waveNum.text = "You won!";
            exit.SetActive(true); //open the exit
        }
        else //start new wave
        {
            waveNum.text = "Wave " + (currentWave + 1); //increase wave count
            SpawnEnemies(); //spawn new enemies
        }
    }

    public bool AllDead() //check if all enemies in the wave are dead
    {
        float totHp = 0;
        foreach (GameObject g in spawnedEnemies)
        {
            totHp += g.GetComponent<EnemyBehaviour>().Hp;
        }
        return totHp <= 0;
    }
}

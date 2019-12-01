using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wave //defnies the enemies in a wave
{
    public GameObject[] enemies; //array for enemies

    public Wave(string[] wavePath) //takes a string for each enemy in wave
    {
        enemies = new GameObject[wavePath.Length]; //define amount of enemies in wave from amount of strings given
        for (int i = 0; i < enemies.Length; i++) //loop for all enemies
        {
            enemies[i] = Resources.Load<GameObject>(wavePath[i]); //load enemy from string
        }
    }
}

public static class AllWaves //static class with methods that create waves
{
    public static Wave Wave1()
    {
        string[] wavePath = { "Enemies/Prefabs/Chicken", "Enemies/Prefabs/Chicken", "Enemies/Prefabs/Chicken" };
        return new Wave(wavePath);
    }

    public static Wave Wave2()
    {
        string[] wavePath = { "Enemies/Prefabs/Chicken", "Enemies/Prefabs/Chicken", "Enemies/Prefabs/Chicken", "Enemies/Prefabs/Chicken", "Enemies/Prefabs/Chicken", "Enemies/Prefabs/Chicken" };
        return new Wave(wavePath);
    }

    public static Wave Wave3()
    {
        string[] wavePath = { "Enemies/Prefabs/Chicken", "Enemies/Prefabs/Chicken", "Enemies/Prefabs/Chicken", "Enemies/Prefabs/Chicken", "Enemies/Prefabs/Chicken", "Enemies/Prefabs/Chicken", "Enemies/Prefabs/Chicken", "Enemies/Prefabs/Chicken", "Enemies/Prefabs/Chicken" };
        return new Wave(wavePath);
    }
}

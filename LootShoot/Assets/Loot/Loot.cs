using System.Collections;
using System.Collections.Generic;

public class Loot //class for all items, with all necessary values
{
    public readonly string spritePath; //save path to sprite instead of actual sprite to be able to serialize, loads form path with resources.load
    public readonly string meshPath; //path to loots mesh
    public static readonly string prefabPath = "DefaultLoot"; //path to prefab that contains loot specific components
    public readonly string name; //name of loot
    public int amount; //amount to stack up items, so they don't take up unnecessary slots in inventory
    public readonly int stack; //how many can fit in a stack
    public readonly float weight; //weight for rigid body
    public readonly bool weapon; //if it's a weapon
    public readonly bool empty; //if instance contains nothing, used for invéntory managment
    public readonly int num; //what type of item is this

    public Loot(string _spritePath, string _meshPath, string _name, int _amount, int _stack, float _weight, bool _weapon, bool _empty, int _num) //constructor to create items
    {
        spritePath = _spritePath;
        meshPath = _meshPath;
        name = _name;
        amount = _amount;
        stack = _stack;
        weight = _weight;
        weapon = _weapon;
        empty = _empty;
        num = _num;
    }
}

public static class AllLoot //static calss containing methods creating all items
{
    public static Loot Empty() => new Loot(null, null, "Empty", 0, 0, 0, false, true, -1); //Method for empty loot

    public static Loot Coin(int amount) => new Loot("Sprites/Coin", "Meshes/Coin", "Coin", amount, 16, .1f, false, false, 0); //method for a coin

    static Loot TestGunBase() => new Loot("Sprites/Gun", "Meshes/TestGun", "Test Gun", 1, 1, 4, true, false, 1); //method for item values of a test gun
    public static Weapon TestGun() => new Weapon(TestGunBase(), 1, .2f, 50, "Meshes/Bullet"); //method for creating a test gun, 100 is highest tested working speed value, more and it might not work
}

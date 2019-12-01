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

    static Loot GlockBase() => new Loot("Sprites/Gun", "Meshes/TestGun", "Glock", 1, 1, 4, true, false, 1); //method for item values of a glock
    public static Weapon Glock() => new Weapon(GlockBase(), 1, .2f, 50, "Meshes/Bullet"); //method for creating a glock

    static Loot MusketBase() => new Loot("Sprites/Musket", "Meshes/Musket", "Musket", 1, 1, 12, true, false, 2); //method for item values of a musket
    public static Weapon Musket() => new Weapon(MusketBase(), 3, 1.5f, 75, "Meshes/Bullet"); //method for creating a musket
}

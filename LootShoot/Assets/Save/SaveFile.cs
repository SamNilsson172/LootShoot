using System.Collections;
using System.Collections.Generic;
using System;

[Serializable]
public class SaveFile //what a savefile contains
{
    public List<Loot> loots { private set; get; } //list of loot to save inventory

    public SaveFile(List<Loot> _loots) //to create a savefile
    {
        loots = _loots;
    }
}

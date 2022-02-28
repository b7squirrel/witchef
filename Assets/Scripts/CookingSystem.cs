using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CookingSystem : MonoBehaviour
{
    public static CookingSystem instance;
    private Dictionary<Rolls.rollType, Rolls.rollType[,]> recipeDictionary;

    private Rolls[,] rollArray;
    private Rolls outputRoll;

    

    public CookingSystem()
    {
        rollArray = new Rolls[2,1];
        recipeDictionary = new Dictionary<Rolls.rollType, Rolls.rollType[,]>();

        Rolls.rollType[,] recipe = new Rolls.rollType[2,1];
        recipe[0, 0] = Rolls.rollType.GoulF;    recipe[1, 0] = Rolls.rollType.ProjectileF;
        recipeDictionary[Rolls.rollType.BombF01] = recipe;

        recipe = new Rolls.rollType[2, 1];
        recipe[0, 0] = Rolls.rollType.GoulF; recipe[1, 0] = Rolls.rollType.GoulF;
        recipeDictionary[Rolls.rollType.GoulF02] = recipe;
    }

    public Rolls.rollType GetRecipeOutput()
    {

        return Rolls.rollType.None;
    }

    public void SetRoll(Rolls roll, int x, int y)
    {
        rollArray[x, y] = roll;
    }

    public Rolls GetRoll(int x, int y)
    {
        return rollArray[x, y];
    }

    public bool IsEmpty(int x, int y)
    {
        return rollArray[x, y] == null;
    }

    private void Awake() 
{
    instance = this;
}
}

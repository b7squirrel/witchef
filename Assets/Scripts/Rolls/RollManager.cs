using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RollManager : MonoBehaviour
{
    public static RollManager instance;

    public Rolls[] rollArray;
    Dictionary<Rolls.rollType, Rolls> rollDictionary;

    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        rollDictionary = new Dictionary<Rolls.rollType, Rolls>();

        for (int i = 0; i < rollArray.Length; i++)
        {
            rollDictionary.Add(rollArray[i].theRollType, rollArray[i]);
            Debug.Log(rollArray[i].name + rollArray[i].theRollType);
        }
    }

    public Rolls GetRoll(Rolls.rollType _rollType)
    {
        return rollDictionary[_rollType];
    }
}

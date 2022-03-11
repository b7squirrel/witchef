using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Cooking System의 recipe가 rollType만을 가지고 있으므로 
/// 해당 rollType을 가지고 있는 Roll을 묶어서 DIctionary로 만들어 준다
/// Cooking System에서 프리펩이나 인벤토리에 들어갈 UI아이콘을 생성할 때 불러낸다
/// </summary>

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
        }
    }

    public Rolls GetRoll(Rolls.rollType _rollType)
    {
        return rollDictionary[_rollType];
    }
}

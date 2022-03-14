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

    [Header("Temp Explosion")]
    public GameObject tempExplosion_small;
    public LayerMask whatToExplode;

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

    public void DestroyRoll(Rolls.rollType _rollType, Vector3 _point)
    {
        Rolls _roll = ScriptableObject.CreateInstance<Rolls>();
        _roll.rollSize = GetRoll(_rollType).rollSize;
        _roll.theRollProperty = GetRoll(_rollType).theRollProperty;

        var clone = Instantiate(tempExplosion_small, _point, transform.rotation);
        clone.GetComponent<explosion>().DestroyArea();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Cooking System�� recipe�� rollType���� ������ �����Ƿ� 
/// �ش� rollType�� ������ �ִ� Roll�� ��� DIctionary�� ����� �ش�
/// Cooking System���� �������̳� �κ��丮�� �� UI�������� ������ �� �ҷ�����
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

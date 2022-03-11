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

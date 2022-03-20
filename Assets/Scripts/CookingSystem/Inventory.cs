using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public static Inventory instance;

    public Slot[] InputSlots;
    public RollSO defaultRollSo;
    public FlavorSo defaultFlavorSo;

    public int numberOfRolls = 0;
    public int numberOfFlavors = 0;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        InputSlots = GetComponentsInChildren<Slot>();
        ResetInventory();
    }

    public void ResetInventory()
    {
        foreach (Slot _slot in InputSlots)
        {
            _slot.InitSlot();
        }
    }

    public void AcquireRoll(RollSO _rollSo)
    {
        if (InputSlots[0].GetRoll().rollSo.rollType == Roll.rollType.none)
        {
            InputSlots[0].AddRoll(_rollSo);
            numberOfRolls++;
            return;
        }
        else
        {
            for (int i = 0; i < numberOfRolls; i++)
            {
                if (InputSlots[i].GetRoll().rollSo.rollType != _rollSo.rollType)  // ������ ���鼭 ���ϴٰ� �ٸ� roll�� ������
                {
                    for (int j = 0; j < InputSlots.Length; j++)  // ��� ������ default�� �ʱ�ȭ �ϰ�
                    {
                        InputSlots[j].AddRoll(defaultRollSo);
                    }
                    numberOfRolls = 0;
                    return; // �׸��� �Լ� ����
                }
            }
        }
        // ������ ���鼭 ���ߴµ� ��� ĸ���� roll�� ���� Ÿ���̶��
        foreach (Slot _slot in InputSlots)
        {
            if(_slot.GetRoll().rollSo.rollType == Roll.rollType.none)
            {
                _slot.AddRoll(_rollSo);
                numberOfRolls++;
                return;
            }
        }
    }

    public void AcquireFlavor(FlavorSo _flavorSo)
    {
        if (InputSlots[0].GetRoll().rollSo.rollType == Roll.rollType.none)  // �����ִ� roll�� ������ ����
        {
            InputSlots[0].AddFlavor(defaultFlavorSo);
            return;
        }
        if (InputSlots[0].GetFlavor().flavorSo.flavorType == Flavor.flavorType.none)
        {
            InputSlots[0].AddFlavor(_flavorSo);
            numberOfFlavors++;
            return;
        }
        else
        {
            for (int i = 0; i < numberOfFlavors; i++)
            {
                if (InputSlots[i].GetFlavor().flavorSo.flavorType != _flavorSo.flavorType)  // ������ ���鼭 ���ϴٰ� �ٸ� flavor�� ������
                {
                    for (int j = 0; j < InputSlots.Length; j++)  // ��� ������ default�� �ʱ�ȭ �ϰ�
                    {
                        InputSlots[j].AddFlavor(defaultFlavorSo);
                    }
                    numberOfFlavors = 0;

                    InputSlots[0].AddFlavor(_flavorSo); // ù��° ���Կ� ĸ���� flavorSo ����
                    numberOfFlavors++;
                    return; // �׸��� �Լ� ����
                }
            }
        }
        // ������ ���鼭 ���ߴµ� ��� ĸ���� flavor�� ���� Ÿ���̶��
        foreach (Slot _slot in InputSlots)
        {
            if (_slot.GetFlavor().flavorSo.flavorType == Flavor.flavorType.none)
            {
                _slot.AddFlavor(_flavorSo);
                numberOfFlavors++;
                return;
            }
        }
    }
}

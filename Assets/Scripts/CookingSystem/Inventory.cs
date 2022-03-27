using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// UI관리
/// Roll, Flavor 갯수 관리
/// </summary>
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
        numberOfFlavors = 0;
        numberOfRolls = 0;
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
                if (InputSlots[i].GetRoll().rollSo.rollType != _rollSo.rollType)  // 슬롯을 돌면서 비교하다가 다른 roll이 나오면
                {
                    for (int j = 0; j < InputSlots.Length; j++)  // 모든 슬롯을 default로 초기화 하고
                    {
                        InputSlots[j].AddRoll(defaultRollSo);
                    }
                    numberOfRolls = 0;
                    return; // 그리고 함수 종료
                }
            }
        }
        // 슬롯을 돌면서 비교했는데 모두 캡쳐한 roll과 같은 타입이라면
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
        //if (InputSlots[0].GetRoll().rollSo.rollType == Roll.rollType.none)  // 받쳐주는 roll이 없으면 제거
        //{
        //    InputSlots[0].AddFlavor(defaultFlavorSo);
        //    return;
        //}
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
                if (InputSlots[i].GetFlavor().flavorSo.flavorType != _flavorSo.flavorType)  // 슬롯을 돌면서 비교하다가 다른 flavor가 나오면
                {
                    for (int j = 0; j < InputSlots.Length; j++)  // 모든 슬롯을 default로 초기화 하고
                    {
                        InputSlots[j].AddFlavor(defaultFlavorSo);
                    }
                    numberOfFlavors = 0;

                    InputSlots[0].AddFlavor(_flavorSo); // 첫번째 슬롯에 캡쳐한 flavorSo 넣음
                    numberOfFlavors++;
                    return; // 그리고 함수 종료
                }
            }
        }
        // 슬롯을 돌면서 비교했는데 모두 캡쳐한 flavor와 같은 타입이라면
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

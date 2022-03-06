using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public static Inventory instance;

    public Slot[] slots;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        slots = GetComponentsInChildren<Slot>();
        UpdateSlotAmount();
    }

    public void UpdateSlotAmount()
    {
        for (int i = 0; i < PlayerPanAttack.instance.capturableAmount; i++)
        {
            slots[i].gameObject.SetActive(true);
        }
        for (int i = PlayerPanAttack.instance.capturableAmount; i < slots.Length; i++)
        {
            slots[i].gameObject.SetActive(false);
        }
    }

    public void AcquireRolls(Rolls roll)
    {
        foreach (var _slot in slots)
        {
            // 슬롯이 비어있으면
            if (_slot.roll == null)  
            {
                // roll을 슬롯에 표시
                _slot.AddRoll(roll);
                return;
            }
        }
    }

    public int Test()
    {
        return 1;
    }

    public Rolls.rollType GetRollInfo(int _index)
    {
        return slots[_index].roll.theRollType;
    }

    public void ClearInventory()
    {
        foreach (var _slot in slots)
        {
            _slot.ClearSlot();
        }
    }
}

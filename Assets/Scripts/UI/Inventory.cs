using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField]
    private Slot[] slots;

    void Start()
    {
        slots = GetComponentsInChildren<Slot>();
        UpdateSlotAmount();
    }

    public void UpdateSlotAmount()
    {
        Debug.Log("Capturable Amount = " + PlayerPanAttack.instance.capturableAmount);
        for (int i = 0; i < PlayerPanAttack.instance.capturableAmount; i++)
        {
            slots[i].gameObject.SetActive(true);
            Debug.Log("slot[" + i + "] = " + slots[i].enabled);
        }
        for (int i = PlayerPanAttack.instance.capturableAmount; i < slots.Length; i++)
        {
            slots[i].gameObject.SetActive(false);
            Debug.Log("slot[" + i + "] = " + slots[i].enabled);
        }
    }

    public void AcquireRolls(Rolls roll)
    {
        foreach(var _slot in slots)
        {
            if(_slot.roll == null)  // 슬롯이 비어있으면
            {
                _slot.AddRoll(roll);  // roll을 슬롯에 표시
                return;
            }
        }
    }

    public void ClearInventory()
    {
        foreach (var _slot in slots)
        {
            _slot.ClearSlot();
        }
    }
}

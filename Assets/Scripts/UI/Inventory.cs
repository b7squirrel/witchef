using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public static Inventory instance;

    public Slot[] slots;
    public Slot outputSlot;  // �ν����Ϳ��� �ޱ�
    public Rolls defaultRoll; // �ν����Ϳ��� �ޱ�

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        slots = GetComponentsInChildren<Slot>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            for (int i = 0; i < 2; i++)
            {
                Debug.Log(slots[i]);
            }
        }
    }

    public void AcquireRolls(Rolls roll)
    {
        for (int i = 0; i < 2; i++)
        {
            // ������ ���������
            if (slots[i].GetRollType() == Rolls.rollType.None)
            {
                // roll�� ���Կ� �Է�
                slots[i].AddInputRoll(roll);
                return;
            }
        }
    }

    public void AcquireOutPutUI(Rolls roll)
    {
        outputSlot.ClearSlot();
        outputSlot.AddRoll(roll);
    }



    public void ClearInventory()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            slots[i].AddInputRoll(defaultRoll);
        }
    }
}

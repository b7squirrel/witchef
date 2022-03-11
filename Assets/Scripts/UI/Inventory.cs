using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public static Inventory instance;

    public Slot[] InputSlots;
    public Slot outputSlot;  
    public Rolls defaultRoll; 

    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        InitInputSlots();
        InitOutPutSlot();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            for (int i = 0; i < 2; i++)
            {
                Debug.Log("Input slot : " + InputSlots[i].GetRollType());
            }
            Debug.Log("output slot : " + outputSlot.GetRollType());
        }
    }
    public void GetInputRolls(Rolls _roll)
    {
        for (int i = 0; i < 2; i++)
        {
            InputSlots[i].AddRoll(_roll);
        }
    }
    public void GetOutputRoll(Rolls _roll)
    {
        outputSlot.AddRoll(_roll);
    }
    public void GetSlotsReady(Rolls _roll)
    {
        // ������ �ʱ�ȭ �ϰ� �����ǿ� ���� �� �ְ� ���� ��ġ�Ѵ�
        InitInputSlots();
        if(outputSlot.GetRollType() == Rolls.rollType.None)
        {
            outputSlot.AddRoll(_roll);
            InputSlots[0].AddRoll(_roll);
        }
        else
        {
            InputSlots[0].AddRoll(outputSlot.GetRoll()); 
            InputSlots[1].AddRoll(_roll);
        }
        InitOutPutSlot();
    }
    public void ClearInventory()
    {
        for (int i = 0; i < InputSlots.Length; i++)
        {
            InputSlots[i].AddRoll(defaultRoll);
        }
    }
    private void InitInputSlots()
    {
        InputSlots[0].AddRoll(defaultRoll);
        InputSlots[1].AddRoll(defaultRoll);
    }
    private void InitOutPutSlot()
    {
        outputSlot.AddRoll(defaultRoll);
    }
}

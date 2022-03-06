using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Slot : MonoBehaviour
{
    //»πµÊ«— æ∆¿Ã≈€
    public Rolls roll;
    private Rolls.rollType _rollType;
    public int rollAmount;
    public Image rollImage;
    private Inventory inventory;
    public void AddRoll(Rolls _roll)
    {
        roll = _roll;
        _rollType = _roll.theRollType;
        rollAmount += 1;
        rollImage.sprite = roll.rollSprite;
        SetColor(1);
    }

    public Rolls.rollType GetRollType()
    {
        return _rollType;
    }

    private void SetColor(float _alpha)
    {
        Color color = rollImage.color;
        color.a = _alpha;
        rollImage.color = color;
    }

    public void ClearSlot()
    {
        roll = null;
        rollImage.sprite = null;
        SetColor(0);
    }
}

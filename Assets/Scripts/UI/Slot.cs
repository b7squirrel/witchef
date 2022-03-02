using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Slot : MonoBehaviour
{
    //»πµÊ«— æ∆¿Ã≈€
    public Rolls roll;
    public int rollAmount;
    public Image rollImage;
    private Inventory inventory;
    public void AddRoll(Rolls _roll)
    {
        roll = _roll;
        rollAmount += 1;
        rollImage.sprite = roll.rollSprite;
        SetColor(1);
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

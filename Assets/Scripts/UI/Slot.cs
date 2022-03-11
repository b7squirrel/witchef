using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Slot : MonoBehaviour
{
    //»πµÊ«— æ∆¿Ã≈€
    public Rolls roll;
    public Rolls defaultRoll;
    public Image rollimage;
    public void ClearSlot()
    {
        roll = defaultRoll;
        roll.theRollType = defaultRoll.theRollType;
        roll.rollSprite = defaultRoll.rollSprite;
        rollimage.sprite = roll.rollSprite;
    }
    public void AddRoll(Rolls _roll)
    {
        roll = _roll;
        roll.theRollType = _roll.theRollType;
        roll.rollSprite = _roll.rollSprite;
        rollimage.sprite = roll.rollSprite;
    }
    public Rolls.rollType GetRollType()
    {
        return roll.theRollType;
    }
    public Rolls GetRoll()
    {
        return roll;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Slot : MonoBehaviour
{
    public Roll roll = new Roll();
    public RollSO defaultRollSo;

    public Flavor flavor = new Flavor();
    public FlavorSo defaultFlavorSo;

    public Image imageRoll;
    public Image imageFlavor;

    private void Start()
    {
        InitSlot();
    }

    public void AddRoll(RollSO _rollSo)
    {
        roll.rollSo = _rollSo;
        roll.rollSo.rollType = _rollSo.rollType;
        imageRoll.sprite = _rollSo.rollSprite;
    }

    public Roll GetRoll()
    {
        return roll;
    }

    public void AddFlavor(FlavorSo _flavorSo)
    {
        this.flavor.flavorSo = _flavorSo;
        flavor.flavorSo.flavorType = _flavorSo.flavorType;
        imageFlavor.sprite = _flavorSo.flavorSprite;
    }

    public Flavor GetFlavor()
    {
        return flavor;
    }

    public void InitSlot()
    {
        roll.rollSo = defaultRollSo;
        roll.rollSo.rollType = defaultRollSo.rollType;
        roll.rollSprite = defaultRollSo.rollSprite;

        flavor.flavorSo = defaultFlavorSo;
        flavor.flavorSo.flavorType = defaultFlavorSo.flavorType;
        flavor.flavorSprite = defaultFlavorSo.flavorSprite;
    }
}

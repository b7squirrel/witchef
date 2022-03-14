using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



[CreateAssetMenu(fileName = "New Roll", menuName = "Roll")]
public class Rolls : ScriptableObject
{
    public enum rollType
    {
        None,
        GoulF,
        ProjectileF,
        WarlockF,
        BombF01,
        BombF02,
        BombF03,
        GoulF02,
        GoulF03,
        WarlockFF,
        WarlockFFF,
        WarlockFFFF,
        WarlockF02,
        WarlockF03,
        BombF02T,
        BombF03T
    }

    public enum rollProperty
    {
        None,
        Bomb
    }

    public rollType theRollType;
    public Sprite rollSprite;
    public GameObject rollPrefab;
    public int rollSize =1 ;
    public rollProperty theRollProperty;
    

    
}

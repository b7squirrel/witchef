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
        GoulF02
    }

    public rollType theRollType;
    public Sprite rollSprite;
    public GameObject rollPrefab;

    
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Roll", menuName = "Roll")]
public class Rolls : ScriptableObject
{
    public rollType theRollType;
    public enum rollType 
    {
        None,
        GoulF,
        ProjectileF,
        WarlockF,
        BombF01,
        GoulF02
    }
    public void Print()
    {
        Debug.Log(theRollType);
    }

}

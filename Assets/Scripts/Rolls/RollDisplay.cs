using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RollDisplay : MonoBehaviour
{
    public Rolls roll;
    public Rolls.rollType theRollType;

    private void Start()
    {
        theRollType = roll.theRollType;
        roll.Print();
    }
}

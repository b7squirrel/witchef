using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    Rolls goulF;
    Rolls projF;

    CookingSystem cookingSystem = new CookingSystem();

    private void Start() 
    {
        cookingSystem.SetRoll(goulF, 0, 0);
        cookingSystem.SetRoll(projF, 0, 0);
    }
}

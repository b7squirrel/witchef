using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Roll", menuName = "RollSO")]
public class RollSO : ScriptableObject
{
    public Roll.rollType rollType;
    public Sprite rollSprite;
    public GameObject[] rollPrefab;
}

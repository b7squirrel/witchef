using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Flavor", menuName = "FlavorSO")]
public class FlavorSo : ScriptableObject
{
    public Flavor.flavorType flavorType;
    public Sprite flavorSprite;
    public GameObject[] flavorPrefab;
}

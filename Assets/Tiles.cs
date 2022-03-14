using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Tiles : MonoBehaviour
{
    public Tilemap tileMap;
    public GameObject tempDebug;
    private void Start()
    {
        tileMap = GetComponent<Tilemap>();
    }
    public void RemoveTile(Vector2 _point)
    {
        Debug.Log("Remove Tiles Method");
        Vector3Int _cellPosition = tileMap.WorldToCell(_point);
        tileMap.SetTile(_cellPosition, null);
    }
}

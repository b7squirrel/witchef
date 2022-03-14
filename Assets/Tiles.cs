using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Tiles : MonoBehaviour
{
    public Tilemap tileMap;
    [Header("Debris")]
    public GameObject debris;
    public GameObject debrisDirt;
    public float debrisOffsetPosition;

    private void Start()
    {
        tileMap = GetComponent<Tilemap>();
    }
    public void RemoveTile(Vector2 _point)
    {
        Vector3Int _cellPosition = tileMap.WorldToCell(_point);

        GenerateDebris(_point);

        tileMap.SetTile(_cellPosition, null);
    }

    private void GenerateDebris(Vector2 _point)
    {
        Instantiate(debris, _point +
            new Vector2(Random.Range(-debrisOffsetPosition, debrisOffsetPosition), Random.Range(-debrisOffsetPosition, debrisOffsetPosition)),
            Quaternion.identity);
        Instantiate(debrisDirt, _point +
            new Vector2(Random.Range(-debrisOffsetPosition, debrisOffsetPosition), Random.Range(-debrisOffsetPosition, debrisOffsetPosition)),
            Quaternion.identity);
        Instantiate(debrisDirt, _point +
            new Vector2(Random.Range(-debrisOffsetPosition, debrisOffsetPosition), Random.Range(-debrisOffsetPosition, debrisOffsetPosition)),
            Quaternion.identity);
    }
}

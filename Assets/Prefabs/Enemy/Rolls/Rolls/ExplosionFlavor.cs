using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionFlavor : MonoBehaviour
{
    public LayerMask groundLayer;
    public LayerMask enemyLayer;
    public GameObject[] explosionEffect = new GameObject[3];

    private BoxCollider2D boxCol_1;  // 1Â÷ Æø¹ß
    private BoxCollider2D boxCol_2;  // 2Â÷ Æø¹ß
    private BoxCollider2D boxCol_3;  // 3Â÷ Æø¹ß

    private Vector2 centerOffset_0;
    private Vector2 centerOffset_1;

    private Vector2 centerCore;
    private Vector2[,] centerOutside = new Vector2[2, 4];

    private Vector2 boxSizeCore;
    private Vector2[] boxSize = new Vector2[2];

    [Header("OffsetScale")]
    public float scale_1;
    public float scale_2;
    public float scale_3;

    [Header("Debris")]
    public GameObject debris;
    public GameObject debrisDirt;
    public GameObject debrisParticleEffect;

    [Header("Delay")]
    private bool isDelaying;
    public float timeToDelay;
    private float delayCounter;
    bool isLastExplosion;

    [Header("Debug")]
    public GameObject debugDot;
    public float dotAlpha = .5f;


    public int numberOfFlavors;

    private Vector2[] explosionSize = new Vector2[3];


    private void Start()
    {
        isLastExplosion = false;
        InitiateExplosionMatrix(numberOfFlavors);
        Instantiate(explosionEffect[0], centerCore, Quaternion.identity);
        Explode(centerCore, boxSizeCore, 0);
       
    }

    private void Update()
    {
        if (delayCounter < timeToDelay)
        {
            delayCounter += Time.deltaTime;
        }
        else
        {
            if (!isLastExplosion)
            {
                DelayExplosion(0, 1);
                isLastExplosion = true;
                delayCounter = 0;
            }
            else
            {
                DelayExplosion(1, 2);
                isLastExplosion = false;
                Destroy(gameObject);
            }
        }
    }
    void DelayExplosion(int _indexOfSize, int _explosionIndex)
    {
        
        for (int indexOfCenter = 0; indexOfCenter < 4; indexOfCenter++)
        {
            Instantiate(explosionEffect[_explosionIndex], centerOutside[_indexOfSize, indexOfCenter], Quaternion.identity);
            Explode(centerOutside[_indexOfSize, indexOfCenter], boxSize[_indexOfSize], _explosionIndex);
        }
    }

    void Explode (Vector2 _center, Vector2 _size, int _explosionIndex)
    {
        for (int i = (int)-_size.x / 2; i <= _size.x / 2; i++)
        {
            for (int j = (int)-_size.y / 2; j <= (int)_size.y / 2; j++)
            {
                Vector3 _cellPosition =
                    new Vector3(_center.x + i, _center.y + j, 0);

                //Instantiate(debugDot, _cellPosition, Quaternion.identity);
                
                Collider2D _hitground = Physics2D.OverlapCircle(_cellPosition, .02f, groundLayer);
                if (_hitground != null)
                {
                    _hitground.GetComponent<Tiles>().RemoveTile(_cellPosition);
                    GenerateDebris(transform.position, _cellPosition);
                }
            }
        }
    }

    private void GenerateDebris(Vector2 _expPoint, Vector2 _DebrisPoint)
    {
        Instantiate(debrisParticleEffect, _DebrisPoint, Quaternion.identity);
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(1, 0, 0, dotAlpha);
        Gizmos.DrawCube(centerCore, boxSizeCore);

        Gizmos.color = new Color(0, 1, 0, dotAlpha);
        Gizmos.DrawCube(centerOutside[0, 0], boxSize[0]);
        Gizmos.DrawCube(centerOutside[0, 1], boxSize[0]);
        Gizmos.DrawCube(centerOutside[0, 2], boxSize[0]);
        Gizmos.DrawCube(centerOutside[0, 3], boxSize[0]);

        Gizmos.color = new Color(0, 0, 1, dotAlpha);
        Gizmos.DrawCube(centerOutside[1, 0], boxSize[1]);
        Gizmos.DrawCube(centerOutside[1, 1], boxSize[1]);
        Gizmos.DrawCube(centerOutside[1, 2], boxSize[1]);
        Gizmos.DrawCube(centerOutside[1, 3], boxSize[1]);
    }
    void InitiateExplosionMatrix(int _numberOfFlavors)
    {
        boxSizeCore = Vector3.one * _numberOfFlavors * scale_1;
        boxSize[0] = boxSizeCore * scale_2;
        boxSize[1] = boxSize[0] * scale_3;

        centerOffset_0 = boxSizeCore * .5f + boxSize[0] * .5f;
        centerOffset_1 = boxSize[0] * .5f + boxSize[1] * .5f;

        centerCore = transform.position;

        centerOutside[0, 0] = centerCore + new Vector2(centerOffset_0.x, 0);
        centerOutside[0, 1] = centerCore + new Vector2(-centerOffset_0.x, 0);
        centerOutside[0, 2] = centerCore + new Vector2(0, centerOffset_0.y);
        centerOutside[0, 3] = centerCore + new Vector2(0, -centerOffset_0.y);

        centerOutside[1, 0] = centerOutside[0, 0] + new Vector2(centerOffset_1.x, 0);
        centerOutside[1, 1] = centerOutside[0, 1] + new Vector2(-centerOffset_1.x, 0);
        centerOutside[1, 2] = centerOutside[0, 2] + new Vector2(0, centerOffset_1.y);
        centerOutside[1, 3] = centerOutside[0, 3] + new Vector2(0, -centerOffset_1.y);
    }
}

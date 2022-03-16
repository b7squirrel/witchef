using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// enemy의 경우에는 enemy 내에서 충돌을 감지해서 boxCollider에 닿으면 데미지를 입도록 했다. 
/// 이 스크립트가 붙어있는 explosion 프리팹의 tag는 playerAttackBox로 했다. 
/// </summary>

public class explosion : MonoBehaviour
{
    public BoxCollider2D boxCol;
    public LayerMask whatToDestroy;
    public GameObject explosionEffect;

    [Header("Debris")]
    public GameObject debris;
    public GameObject debrisDirt;
    public GameObject debrisParticleEffect;
    public float explosionForce;
    public float debrisOffsetPosition;

    public void DestroyArea(int _size)
    {
        Instantiate(explosionEffect, transform.position, Quaternion.identity);
        

        Vector2Int _boxColSize = new Vector2Int(Mathf.RoundToInt(Mathf.Pow(_size, 2) * 2), 
            Mathf.RoundToInt(Mathf.Pow(_size, 2) * 2 - _size));
        boxCol.size = new Vector2(_boxColSize.x, _boxColSize.y);
        
        for (int i = -_boxColSize.x/2; i <= _boxColSize.x/2; i++)
        {
            for (int j = -_boxColSize.y/2; j <= _boxColSize.y/2; j++)
            {
                Vector3 _cellPosition = 
                    new Vector3(transform.position.x + i, transform.position.y + j, 0);
                

                Collider2D _hit = Physics2D.OverlapCircle(_cellPosition, .01f, whatToDestroy);
                if (_hit != null)
                {
                    if(_hit.gameObject.layer == 8)  // ground layer
                    {
                        _hit.GetComponent<Tiles>().RemoveTile(_cellPosition);
                        GenerateDebris(transform.position, _cellPosition);
                    }
                }
            }
        }
    }
    private void GenerateDebris(Vector2 _expPoint, Vector2 _DebrisPoint)
    {
        Instantiate(debrisParticleEffect, _DebrisPoint, Quaternion.identity);

        //Vector2 _DebrisOffsetPosition = _DebrisPoint +
        //    new Vector2(Random.Range(-debrisOffsetPosition, debrisOffsetPosition),
        //    Random.Range(-debrisOffsetPosition, debrisOffsetPosition));

        //var clone1 = Instantiate(debris, _DebrisOffsetPosition, Quaternion.identity);
        //Vector2 debrisDirection = _DebrisPoint - _expPoint;
        //clone1.GetComponent<Rigidbody2D>().AddForce(debrisDirection * explosionForce);

        //_DebrisOffsetPosition = _DebrisPoint +
        //    new Vector2(Random.Range(-debrisOffsetPosition, debrisOffsetPosition),
        //    Random.Range(-debrisOffsetPosition, debrisOffsetPosition));

        //var clone2 = Instantiate(debrisDirt, _DebrisOffsetPosition, Quaternion.identity);
        //debrisDirection = _DebrisPoint - _expPoint;
        //clone1.GetComponent<Rigidbody2D>().AddForce(debrisDirection * explosionForce);

        //_DebrisOffsetPosition = _DebrisPoint +
        //    new Vector2(Random.Range(-debrisOffsetPosition, debrisOffsetPosition),
        //    Random.Range(-debrisOffsetPosition, debrisOffsetPosition));

        //var clone3 = Instantiate(debrisDirt, _DebrisOffsetPosition, Quaternion.identity);
        //debrisDirection = _DebrisPoint - _expPoint;
        //clone1.GetComponent<Rigidbody2D>().AddForce(debrisDirection * explosionForce);

    }


    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(boxCol.bounds.center, boxCol.size);
    }
}

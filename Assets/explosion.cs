using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class explosion : MonoBehaviour
{
    public CircleCollider2D _circleCollider;
    public LayerMask whatToDestroy;

    private void Start()
    {
        _circleCollider = GetComponent<CircleCollider2D>();
    }

    public void DestroyArea()
    {
        int _radiusInt = Mathf.RoundToInt(_circleCollider.radius);
        for (int i = -_radiusInt; i <= _radiusInt; i++)
        {
            for (int j = -_radiusInt; j <= _radiusInt; j++)
            {
                Vector3 _cellPosition = 
                    new Vector3(transform.position.x + i, transform.position.y + j, 0);
                

                float _distance = Vector3.Distance(transform.position, _cellPosition);

                if(_distance <= _radiusInt)
                {
                    Collider2D _hit = Physics2D.OverlapCircle(_cellPosition, .01f, whatToDestroy);
                    if(_hit != null)
                    {
                        
                        _hit.GetComponent<Tiles>().RemoveTile(_cellPosition);
                    }
                }
            }
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, _circleCollider.radius);
    }
}

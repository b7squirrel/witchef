using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFlavor : MonoBehaviour
{
    public LayerMask roll;

    private void Awake()
    {
        Collider2D hit = Physics2D.OverlapCircle(transform.position, .2f, roll);
        if(hit != null)
        {
            transform.parent = hit.transform;
        }
    }
    
}

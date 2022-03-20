using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 인스펙터에서 편하게 드래그해서 넣으려고 Flavor prefab 폴더에 스크립트가 있음
/// </summary>
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

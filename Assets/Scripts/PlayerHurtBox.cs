using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHurtBox : MonoBehaviour
{
    private Animator anim;
    public int whichSideToBeHit; // dieEffect�� ���ư��� ������ ���ϱ� ����
    private bool isHitAfterParrying;
    private bool assumingBeingHit;
    private bool isAttackBoxParried; // projectile�� isAttackBoxParried�� �������� ���� �÷���

    private float waitingTime;

    private void Start()
    {
        anim = GetComponentInParent<Animator>();
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("ProjectileEnemy"))
        {
            float distance = transform.position.x - collision.transform.position.x;

            if (distance < 0)
            {
                whichSideToBeHit = 0;
            }
            else if (distance > 0) 
            {
                whichSideToBeHit = 180;
            }

            PlayerHealthController.instance.isDead = true;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private Animator anim;

    public GameObject attackBox;
    public float attackCoolTime;
    private float attackTimer;

    public float parryDuration;
    [HideInInspector] public float parryTimer;

    private bool isAttacking; // ���� �ִϸ��̼��� ����Ǵ� ������ �ٸ� ���� �ִϸ��̼��� ������� �ʵ��� �ϴ� �÷���

    public Inventory inventory;

    void Start()
    {
        anim = GetComponent<Animator>();
        attackBox.SetActive(false);
    }

    void Update()
    {
        if(attackTimer > 0f)
        {
            attackTimer -= Time.deltaTime;
        }

        //���� ������ �ε����ϰ� ���߿� ��ҵǾ� attackboxOff�� ������� �ʾ��� ��츦 ���
        if (!anim.GetCurrentAnimatorStateInfo(0).IsName("Player_Attack"))
        {
            AttackBoxOff();
        }

        if (Input.GetKeyDown(KeyCode.Z))
        {
            if(inventory.InputSlots[0].GetRoll().rollSo.rollType == Roll.rollType.none)  // �д� ���� ���� PanThrowing �� �ߵ��Ǿ� �ϴϱ� ������ ������ �ʰ�
            {
                if(attackTimer <= 0f)
                {
                    anim.Play("Player_Attack");
                    AudioManager.instance.Play("whoosh_01");
                    attackTimer = attackCoolTime;
                }
            }
        }
    }
    // animation event
    void AttackBoxOn()
    {
        attackBox.SetActive(true);
        parryTimer = parryDuration;
    }
    void AttackBoxOff()
    {
        attackBox.SetActive(false);
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPanAttack : MonoBehaviour
{
    public static PlayerPanAttack instance;
    public Animator anim;
    public Transform attackPoint;
    public float attackRange;
    public LayerMask enemyLayers;
    public LayerMask rollLayers;

    public Transform panPoint;
    public Transform hittingRollPoint;

    private PlayerCaptureBox playerCaptureBox;

    public float captureDuration;
    private float captureTimer;

    public Inventory inventory;
    public RollSO rollso;
    public FlavorSo flavorSo;
    
    public float CaptureTimer
    {
        get { return captureTimer; }
        set { captureTimer = value; }
    }

    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        anim = GetComponent<Animator>();
        playerCaptureBox = GetComponentInChildren<PlayerCaptureBox>();
    }
    void Update()
    {
        if (captureTimer > 0f)
        {
            captureTimer -= Time.deltaTime;
        }

        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            //���� ĸ�� ����
            anim.Play("Player_Capture");
            captureTimer = captureDuration;
        }
        
        if (Input.GetKeyDown(KeyCode.Z))
        {
            if(inventory.InputSlots[0].GetRoll().rollSo.rollType != Roll.rollType.none)
            {
                anim.Play("Player_HitRoll");  // throwingAntic�� ������ throwing���� �Ѿ.
            }
        }
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            anim.Play("Player_Parrying");
        }
        if (Input.GetKeyDown(KeyCode.L))
        {
            Debug.Log("Tryging to Reset Inventory.");
            inventory.InputSlots[0].InitSlot();
            //inventory.AcquireFlavor(flavorSo);
            //inventory.AcquireRoll(rollso);
        }
    }

    // enemy�� overlapBox�� ĸ���ϰ� projectile�� playercaptureBox���� ontriggerenter2d�� �����ؼ� ĸ����
    // �������� �ʹ� �����̶� projectile�� ��⿡ �������� ����
    void Capture() 
    {
        Collider2D[] hits = Physics2D.OverlapBoxAll(playerCaptureBox.boxCol.bounds.center, playerCaptureBox.boxCol.bounds.size, 0, enemyLayers);
        if(hits != null)
        {
            foreach (Collider2D enemy in hits)
            {
                if (inventory.numberOfRolls < 3)
                {
                    TakeDamage takeDmg = enemy.GetComponent<TakeDamage>();
                    if (takeDmg != null)
                    {
                        
                        takeDmg.isCaptured = true;
                    }
                }
            }
        }
    }
    
    void HitRoll()
    {
        Collider2D[] hitRolls = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, rollLayers);

        foreach (Collider2D roll in hitRolls)
        {
            EnemyRolling enemyRolling = roll.GetComponent<EnemyRolling>();
            if (enemyRolling != null)
            {
                enemyRolling.transform.position = hittingRollPoint.position;
                enemyRolling.BeingHit();
                AudioManager.instance.Play("fire_explosion_01");
                AudioManager.instance.Play("pan_hit_03");
                PlayerController.instance.ResetWeight();
                inventory.ResetInventory();
            }
        }
        
    }
    void Panning()
    {
        // Capture�� ������ �����ӿ��� �ִϸ��̼� �̺�Ʈ�� ����
        if (!anim.GetCurrentAnimatorStateInfo(0).IsName("Player_Panning"))
        {
            if (inventory.InputSlots[0].GetRoll().rollSo.rollType != Roll.rollType.none)
            {
                anim.Play("Player_Panning");
            }
        }
    }
}

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
    public LayerMask projectileLayers;
    public LayerMask rollLayers;

    public Transform panPoint;
    public Transform hittingRollPoint;

    private PlayerCaptureBox playerCaptureBox;

    public int capturableAmount;
    private int captureCounter;
    
    public float captureDuration;
    private float captureTimer;

    public Inventory inventory;
    public Slot outputSlot;
    
    public int CaptureCounter
    {
        get { return captureCounter; }
        set { captureCounter = value; }
    }

    public float CaptureTimer
    {
        get { return captureTimer; }
        set { captureTimer = value; }
    }

    public bool enemyLoaded;

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
            //무한 캡쳐 가능
            anim.Play("Player_Capture");
            captureTimer = captureDuration;
            //if (captureCounter < capturableAmount)
            //{
            //    anim.Play("Player_Capture");
            //    captureTimer = captureDuration;
            //}
        }
        
        if (Input.GetKeyDown(KeyCode.Z))
        {
            if(enemyLoaded)
            {
                anim.Play("Player_HitRoll");  // throwingAntic이 끝나면 throwing으로 넘어감.
            }
        }
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            anim.Play("Player_Parrying");
        }
    }

    void Capture()
    {
        Collider2D[] hits = Physics2D.OverlapBoxAll(playerCaptureBox.boxCol.bounds.center, playerCaptureBox.boxCol.bounds.size, 0, enemyLayers);
        if(hits != null)
        {
            foreach (Collider2D enemy in hits)
            {
                if (captureCounter < capturableAmount)
                {
                    TakeDamage takeDmg = enemy.GetComponent<TakeDamage>();
                    if (takeDmg != null)
                    {
                        enemyLoaded = true;
                        takeDmg.isCaptured = true;
                        captureCounter++;
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
                captureCounter = 0;
                PlayerController.instance.ResetWeight();
                inventory.ClearInventory();
                outputSlot.ClearSlot();
            }
        }
        enemyLoaded = false;
    }
    void Panning()
    {
        // Capture의 마지막 프레임에서 
        if (!anim.GetCurrentAnimatorStateInfo(0).IsName("Player_Panning"))
        {
            if (enemyLoaded)
            {
                anim.Play("Player_Panning");
            }
        }
    }
}

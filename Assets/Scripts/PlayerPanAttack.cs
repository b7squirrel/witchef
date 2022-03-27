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
            //무한 캡쳐 가능
            anim.Play("Player_Capture");
            captureTimer = captureDuration;
        }

        if (Input.GetKeyDown(KeyCode.Z))
        {
            if (inventory.InputSlots[0].GetRoll().rollSo.rollType != Roll.rollType.none)
            {
                anim.Play("Player_HitRoll");  // throwingAntic이 끝나면 throwing으로 넘어감.
            }
        }
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            anim.Play("Player_Parrying");
        }
    }

    // enemy는 overlapBox로 캡쳐하고 projectile은 playercaptureBox에서 ontriggerenter2d로 감지해서 캡쳐함
    // 오버랩은 너무 순간이라서 projectile을 잡기에 적합하지 않음
    // 캡쳐 함수는 애니메이션 이벤트로 실행
    void Capture()
    {
        Collider2D[] hits = Physics2D.OverlapBoxAll(playerCaptureBox.boxCol.bounds.center, playerCaptureBox.boxCol.bounds.size, 0, enemyLayers);
        if (hits != null)
        {
            foreach (Collider2D enemy in hits)
            {
                if(enemy.gameObject.CompareTag("Enemy"))
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
    }
    void Panning()
    {
        // Capture의 마지막 프레임에서 애니메이션 이벤트로 실행
        if (!anim.GetCurrentAnimatorStateInfo(0).IsName("Player_Panning"))
        {
            if (inventory.InputSlots[0].GetRoll().rollSo.rollType != Roll.rollType.none)
            {
                anim.Play("Player_Panning");
            }
        }
    }
    void HitRoll()
    {
        Roll.rollType _roll = inventory.InputSlots[0].GetRoll().rollSo.rollType;
        int _rollNumber = inventory.numberOfRolls;
        int _flavorNumber = inventory.numberOfFlavors;

        Vector3 _hitPoint = panPoint.position + new Vector3(PlayerController.instance.staticDirection * 2.2f, 0);

        // Roll, Flavor 생성
        GameObject _rollPrefab = Instantiate(CookingSystem.instance.outputRoll.rollPrefab[inventory.numberOfRolls - 1], _hitPoint, panPoint.rotation);
        _rollPrefab.GetComponent<EnemyRolling>().numberOfFlavor = inventory.numberOfFlavors;
        _rollPrefab.GetComponent<EnemyRolling>().theFlavorSo = CookingSystem.instance.outputFlavor; // flavor액션을 가져오기 위해
        if (CookingSystem.instance.outputFlavor.flavorType != Flavor.flavorType.none)
        {
            GameObject _flavorPrefab = Instantiate(CookingSystem.instance.outputFlavor.flavorParticle, _hitPoint, panPoint.rotation);
            _flavorPrefab.transform.parent = _rollPrefab.transform;
            _flavorPrefab.GetComponent<ParticleController>().numberOfFlavors = inventory.numberOfFlavors;
            _flavorPrefab.transform.localEulerAngles = new Vector3(-90, 0, 0);
        }

        _rollPrefab.GetComponent<EnemyRolling>().BeingHit();



        AudioManager.instance.Play("fire_explosion_01");
        AudioManager.instance.Play("pan_hit_03");
        PlayerController.instance.ResetWeight();

        inventory.ResetInventory();
        CookingSystem.instance.ResetOutputs();
    }
    
}
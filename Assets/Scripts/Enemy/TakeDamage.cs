using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakeDamage : MonoBehaviour
{
    public int currentHP;
    public int maxHP;

    public bool isStunned; 
    public bool isRolling;

    public GameObject dieEffect;

    [Header("Rolling")]
    public bool isCaptured;  // �� ������ capture�Ǿ����� ���� �ް� GetRolled�� �����Ŵ

    [Header("White Flash")]
    public Material whiteMat;
    private Material initialMat;
    public GameObject mSprite;
    private SpriteRenderer theSR;
    public float blinkingDuration;

    [Header("Roll Type")]
    public Rolls rolls;

    [Header("Inventory")]
    public Inventory inventory;
    public CookingSystem cookingSystem;

    private void Start()
    {
        currentHP = maxHP;
        theSR = mSprite.GetComponent<SpriteRenderer>();
        initialMat = theSR.material;
        cookingSystem = FindObjectOfType<Inventory>().GetComponent<CookingSystem>();

    }
    private void Update()
    {
        if (isCaptured)
        {
            GetRolled();
        }
    }
    // hp�� 0�� �Ǹ� ���� �ִϸ��̼� ���
    // ���� ���¿����� hp�� ������ ����
    // ���� ���¿��� PanAttack�� ������ GetRolled ����
    // ���� ������ �ϴ� distroy
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("AttackBoxPlayer"))
        {
            if(!isStunned)
            {
                currentHP--;
                AudioManager.instance.Play("pan_hit_04");
                //StartCoroutine(WhiteFlash());
                //�ð� ���߰� ī�޶���ũ
                GameManager.instance.StartCameraShake(4, .5f);
                GameManager.instance.TimeStop(.02f);

                if (currentHP <= 0f)
                {
                    AudioManager.instance.Play("pan_hit_05");
                    //isStunned = true;
                    //currentHP = maxHP;
                    Die();
                }
            }
        }

        if(collision.CompareTag("Rolling"))
        {
            if(!collision.GetComponent<EnemyRolling>().isRolling)
            {
                Die();

            }
        }
    }

    public void TempDebug()
    {
        Debug.Log("Take Damage");
    }

    public void GetRolled()  // ���� �����ϰ� �κ��丮�� ��Ÿ���� ǥ��
    {
        AudioManager.instance.Play("GetRolled_01");
        GameObject roll = Instantiate(rolls.rollPrefab, PlayerPanAttack.instance.panPoint.position, transform.rotation);
        inventory.GetSlotsReady(rolls);
        cookingSystem.Cook();
        isStunned = false;
        isCaptured = false;
        PlayerController.instance.weight++;
        PlayerController.instance.WeightCalculation();
        HideEnemy();
    }
    public void Die()
    {
        //StartCoroutine(WhiteFlash());
        Instantiate(dieEffect, transform.position, transform.rotation);
        AudioManager.instance.Play("Goul_Die_01");
        AudioManager.instance.Stop("Energy_01");
        currentHP = maxHP;
        isStunned = false;
        isCaptured = false;
        transform.parent.gameObject.SetActive(false);
        
    }
    void HideEnemy()
    {
        currentHP = maxHP;
        isStunned = false;
        isCaptured = false;
        transform.parent.gameObject.SetActive(false);
    }
    IEnumerator WhiteFlash()
    {
        theSR.material = whiteMat;
        yield return new WaitForSecondsRealtime(blinkingDuration);
        theSR.material = initialMat;
    }
}

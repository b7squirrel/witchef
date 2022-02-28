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
    public GameObject rolling;
    public bool isCaptured;  // �� ������ capture�Ǿ����� ���� �ް� GetRolled�� �����Ŵ

    [Header("White Flash")]
    public Material whiteMat;
    private Material initialMat;
    public GameObject mSprite;
    private SpriteRenderer theSR;
    public float blinkingDuration;

    private void Start()
    {
        currentHP = maxHP;
        theSR = mSprite.GetComponent<SpriteRenderer>();
        initialMat = theSR.material;
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
                StartCoroutine(WhiteFlash());
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

    public void GetRolled()
    {
        AudioManager.instance.Play("GetRolled_01");
        Instantiate(rolling, PlayerPanAttack.instance.panPoint.position, transform.rotation);
        isStunned = false;
        isCaptured = false;
        HideEnemy();
    }
    void Die()
    {
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

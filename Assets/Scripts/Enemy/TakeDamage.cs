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
    public bool isCaptured;  // 이 변수로 capture되었음을 전달 받고 GetRolled를 진행시킴

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
    // hp가 0이 되면 스턴 애니메이션 재생
    // 스턴 상태에서는 hp가 깎이지 않음
    // 스턴 상태에서 PanAttack이 들어오면 GetRolled 실행
    // 롤을 맞으면 일단 distroy
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("AttackBoxPlayer"))
        {
            if(!isStunned)
            {
                currentHP--;
                AudioManager.instance.Play("pan_hit_04");
                //StartCoroutine(WhiteFlash());
                //시간 멈추고 카메라쉐이크
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

    public void GetRolled()  // 롤을 생성하고 인벤토리에 롤타입을 표시
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

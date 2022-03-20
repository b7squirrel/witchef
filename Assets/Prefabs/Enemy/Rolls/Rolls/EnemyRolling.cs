using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRolling : MonoBehaviour
{
    private enum rollingState { rolling, shooting, flying, dropped };
    private rollingState currentState;

    public float followingPanSpeed;
    private Rigidbody2D theRB;

    public bool isRolling; // �� ������ ���� �ִ� ���
    public bool beingHit;  // player pan attack���� ����

    private float direction;
    public float initForce_x, initForce_y;

    public GameObject hitEffect;
    public Transform hitEffectPoint;

    

    void Start()
    {
        theRB = GetComponent<Rigidbody2D>();
        currentState = rollingState.rolling;
        isRolling = true;
    }

    void Update()
    {
        SetDirection();

        if(PlayerHealthController.instance.isDead)
        {
            currentState = rollingState.dropped;
        }

        switch (currentState)
        {
            case rollingState.rolling:

                theRB.gravityScale = 0;
                transform.position = Vector2.Lerp(transform.position, PlayerPanAttack.instance.panPoint.position, followingPanSpeed * Time.deltaTime);
                break;

            case rollingState.shooting:

                if (theRB.gravityScale != 1)
                {
                    theRB.gravityScale = 3;
                }
                theRB.velocity = new Vector2(direction * initForce_x, initForce_y);
                currentState = rollingState.flying;
                break;

            case rollingState.flying:
                break;

            case rollingState.dropped:
                DestroyPrefab();
                //transform.parent = null;
                //theRB.gravityScale = 5;
                break;

                
        }
    }

    void SetDirection()
    {
        direction = PlayerController.instance.staticDirection;
        if(direction > 0)
        {
            transform.eulerAngles = new Vector3(0, 0, 0);
        }
        else
        {
            transform.eulerAngles = new Vector3(0, 180, 0);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Ground") || collision.CompareTag("Enemy"))
        {
            if(!isRolling)
            {
                // �� Ÿ�Կ� ���� ������ ������ �Ը� �޶����� �ڵ带 �־�� ��
                Destroy(gameObject);
            }
        }
    }
    public void BeingHit()
    {
        currentState = rollingState.shooting;
        beingHit = false;
        isRolling = false;
        GameObject clone = Instantiate(hitEffect, hitEffectPoint.position, hitEffectPoint.rotation);
        //�ð� ���߰� ī�޶���ũ
        GameManager.instance.StartCameraShake(8, .8f);
        GameManager.instance.TimeStop(.1f);
    }
    public void DestroyPrefab()
    {
        Destroy(gameObject);
    }
}

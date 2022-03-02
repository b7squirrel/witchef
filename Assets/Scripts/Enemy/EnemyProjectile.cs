using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    private Transform target;
    public float moveSpeed;
    private Vector2 moveDirection;
    private Rigidbody2D theRB;

    private Vector2 initialPoint; // parry �Ǿ��� �� �ٽ� �ǵ��� ���� ���� ��ġ��
    public Vector2 contactPoint; // parry �� ������ ���������� �ϱ� ���� ����
    public bool isParried; // projectile�� �߻�Ǿ����� parry �Ǿ����� �Ǵ�
    public float homingTime; // �ݻ�Ǿ Ÿ�ٿ� �����ϱ���� �ɸ��� �ð�
    private bool isFlying; // parry �Ǿ ���ư��� ����. �ƹ��͵� ����. 

    public bool isCaptured; // ĸ�ĵǾ����� ���� �ް� �� ��ũ��Ʈ���� getRolled�� ����

    public Inventory inventory;
    public Rolls rolls;
    public GameObject sparkEffect;
    public GameObject smokeRed;

    private bool hit;
    public GameObject cube;

    void Start()
    {
        theRB = GetComponent<Rigidbody2D>();
        moveDirection = (PlayerController.instance.transform.position - transform.position).normalized * moveSpeed;
        initialPoint = new Vector2(transform.position.x, transform.position.y - 1f);
        isParried = false;
        isFlying = false;
        inventory = FindObjectOfType<Inventory>().GetComponent<Inventory>();
    }

    void Update()
    {
        if(isCaptured)
        {
            GetRolled();
        }
        else
        {
            if (!isFlying)
            {
                if (!isParried)
                {
                    theRB.velocity = new Vector2(moveDirection.x, moveDirection.y);
                    Instantiate(smokeRed, transform.position, Quaternion.identity);
                }
                else
                {
                    if (!hit)
                    {
                        Color color = new Color(0, 1, 0, 1f);
                        Gizmos.color = color;
                        Instantiate(cube, transform.position, transform.rotation);
                        hit = true;
                    }
                    isFlying = true;
                    theRB.gravityScale = 1f;
                    //Deflection();
                    Temp();
                    // �ð� ���߰� ī�޶� ����ũ
                    GameManager.instance.StartCameraShake(6, 1.3f);
                    GameManager.instance.TimeStop(.08f);
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("HurtBoxPlayer"))
        {
            Destroy(gameObject);
        }
        if (collision.CompareTag("Ground"))
        {
            Destroy(gameObject);
        }
        if (collision.CompareTag("Enemy"))
        {
            if (this.gameObject.tag == "ProjectileDeflected")
            {
                Destroy(gameObject);
            }
        }
    }
    Vector2 CalculateVelecity(Vector2 _target, Vector2 _origin, float time)
    {
        Vector2 distance = _target - _origin;

        float Vx = distance.x / time;
        float Vy = distance.y / time + 0.5f * Mathf.Abs(Physics2D.gravity.y) * time;

        Vector2 result;
        result.x = Vx;
        result.y = Vy;

        return result;
    }

    void Deflection()
    {
        this.gameObject.tag = "ProjectileDeflected";
        
        Transform effectPoint = transform;
        effectPoint.position += new Vector3(2f, .7f, 0f);
        effectPoint.eulerAngles = new Vector3(transform.rotation.x, PlayerController.instance.transform.rotation.y, -10f);
        //Instantiate(sparkEffect, effectPoint.position, effectPoint.rotation);

        theRB.velocity = CalculateVelecity(initialPoint, (Vector2)contactPoint, homingTime);
    }

    void Temp()
    {
        float tempDirection = transform.position.x - initialPoint.x;
        int direc = 0;

        if(tempDirection > 0)
        {
            direc = -1;
        }
        else if(tempDirection < 0)
        {
            direc = 1;
        }

        theRB.velocity = new Vector2(direc * moveSpeed / 12, 12f);
    }

    void GetRolled()
    {
        AudioManager.instance.Play("GetRolled_01");
        Instantiate(rolls.rollPrefab, PlayerPanAttack.instance.panPoint.position, transform.rotation);
        inventory.AcquireRolls(rolls);
        HideEnemy();
    }

    private void HideEnemy()
    {
        gameObject.SetActive(false);
    }
}

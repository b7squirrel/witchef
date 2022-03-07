using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRolling : MonoBehaviour
{
    private enum rollingState { rolling, shooting, flying };
    private rollingState currentState;
    public Rolls roll;

    public float followingPanSpeed;
    private Rigidbody2D theRB;

    public bool isRolling; // 팬 위에서 돌고 있는 경우
    public bool beingHit;  // player pan attack에서 참조

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
                Destroy(gameObject);
            }
        }
    }
    public void Test()
    {
        Debug.Log("Debugging Test");
    }

    public void BeingHit()
    {
        currentState = rollingState.shooting;
        beingHit = false;
        isRolling = false;
        GameObject clone = Instantiate(hitEffect, hitEffectPoint.position, hitEffectPoint.rotation);
        //시간 멈추고 카메라쉐이크
        GameManager.instance.StartCameraShake(8, .8f);
        GameManager.instance.TimeStop(.1f);
    }

    public void DestroyPrefab()
    {
        Destroy(gameObject);
    }
}

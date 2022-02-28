using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoulFighter : MonoBehaviour
{
    private enum enemyState { idle, follow, attack, stunned };
    [SerializeField] private enemyState currentState;

    private Animator anim;
    private Rigidbody2D theRB;

    [Header("Detecting")]
    public float distance; // raycast �Ÿ� ����
    public Transform castPoint;
    public LayerMask action;

    private bool canSeePlayer;

    private bool isFacingLeft;
    private bool isPlayerToLeft, wasPlayerToLeft;

    [Header("Follow")]
    public float moveSpeed;
    public float timeToStopFollowing;
    private bool isDetecting; // �þ߿��� ����� �÷��̾ �i�� ������ ���� �÷���
    private bool isSearching; // �÷��̾ �þ߿��� ������� isDetecting�� false�� �� stopFollowingPlayer �Լ��� ��� ȣ���Ϸ� ���� ���ϵ��� �ϴ� �÷���
    private bool isChangingDirection;

    [Header("Stunned")]
    private TakeDamage takeDamage;
    public bool isParried;

    [Header("Attack")]
    public float attackDistnace;
    public float attackCoolTime;
    private float attackCounter;
    public float attackRange;

    [Header("HitBox")]
    public GameObject attackBox;

    private void Start()
    {
        anim = GetComponent<Animator>();
        theRB = GetComponent<Rigidbody2D>();
        takeDamage = GetComponentInChildren<TakeDamage>();
        currentState = enemyState.follow;
        isDetecting = false;
        isFacingLeft = true;
        wasPlayerToLeft = isPlayerToLeft;
        attackBox.gameObject.SetActive(false);
    }

    private void Update()
    {
        IsStunned();

        switch (currentState)
        {
            case enemyState.attack:

                if (CanAttackPlayer())
                {
                    if (!anim.GetCurrentAnimatorStateInfo(0).IsName("Goul_Fighter_Attack"))
                    {
                        if (attackCounter <= 0f)
                        {
                            attackCounter = attackCoolTime;

                            Attack();
                        }
                        else
                        {
                            attackCounter -= Time.deltaTime;
                        }
                    }
                }
                else
                {
                    if (!anim.GetCurrentAnimatorStateInfo(0).IsName("Goul_Fighter_Attack"))
                    {
                        // ���� ��� ���̶�� �� ����� ���� �� ������ ��ٸ�
                        // Attack ����� ���̳��� walk ������� ��.
                        // walk����� ����ǰ� ������ Follow�� �ƴϹǷ� ���ڸ����� �ȴµ� �÷��̾�� ��ĥ��ŭ �ٰ����� �ʱ� ������ ����
                        currentState = enemyState.follow;
                    }
                    else
                    {
                        currentState = enemyState.attack;
                    }
                }

                break;

            case enemyState.follow:

                attackCounter = 0f; // canAttack ���°� �Ǿ��� �� �ٷ� ������ �� �ֵ��� �̸� �ʱ�ȭ ���ѵ�

                if (canSeePlayer)
                {
                    isDetecting = true;
                }
                else
                {
                    if (isDetecting)  // �÷��̾ �þ߿��� ��������� ���� �÷��̾ ������ �ִٸ�
                    {
                        if (!isSearching)
                        {
                            // �÷��̾ �þ߿��� ��������� ��а��� �÷��̾ �i�ƴٴϵ���
                            isSearching = true;  // stopFollowingPlayer �ڷ�ƾ���� ��� �������� ���� ����
                            StartCoroutine(StopFollowingPlayer());
                        }
                    }
                }

                if (isDetecting)
                {
                    FollowPlayer();
                }

                if (CanAttackPlayer())
                {
                    currentState = enemyState.attack;
                }

                break;

            case enemyState.stunned:

                if (!anim.GetCurrentAnimatorStateInfo(0).IsName("Goul_Fighter_Stunned"))
                {
                    currentState = enemyState.follow;
                }

                break;
        }
    }

    void CheckIsFollowing()
    {
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("Goul_Fighter_Walk"))
        {
            currentState = enemyState.follow;
        }
    }

    void IsStunned()
    {
        if (takeDamage.isStunned)
        {
            if (!anim.GetCurrentAnimatorStateInfo(0).IsName("Goul_Fighter_Stunned"))
            {
                anim.Play("Goul_Fighter_Stunned");
            }
            currentState = enemyState.stunned;
        }

        if (isParried)
        {
            if (!anim.GetCurrentAnimatorStateInfo(0).IsName("Goul_Fighter_Stunned"))
            {
                anim.Play("Goul_Fighter_Stunned");
            }
            currentState = enemyState.stunned;
        }
    }

    //animation events
    void ResetStunned()
    {
        // playerParryBox�� ���� untagged�� �ٲ���� ��ũ�� �ٽ� AttackBoxEnemy�� �ٲ�
        takeDamage.isStunned = false;
        takeDamage.isRolling = false;
        isParried = false;
        attackBox.gameObject.tag = "AttackBoxEnemy";

    }

    bool CanAttackPlayer()
    {
        float _castDistance = attackDistnace;
        bool _canAttackPlayer = false;

        if (isFacingLeft)
        {
            // ��������Ʈ�� �������� Line�� �ݴ� �������� ���
            _castDistance = -_castDistance;
        }

        Vector2 _endPosition = castPoint.position + Vector3.right * _castDistance;

        RaycastHit2D hit = Physics2D.Linecast(castPoint.position, _endPosition, action);
        if (hit.collider != null)
        {
            if (hit.collider.gameObject.CompareTag("HurtBoxPlayer"))
            {
                _canAttackPlayer = true;
            }
            else
            {
                _canAttackPlayer = false;
            }

            Debug.DrawLine(castPoint.position, hit.point, Color.red); // �ڽ��� �տ� �����ΰ��� �����ϸ� yellow
        }
        else
        {
            Debug.DrawLine(castPoint.position, _endPosition, Color.yellow); // �ƹ��͵� �������� ���� ���� blue
        }

        return _canAttackPlayer;
    }

    void Attack()
    {
        if (!anim.GetCurrentAnimatorStateInfo(0).IsName("Goul_Fighter_Stunned"))
        {
            anim.Play("Goul_Fighter_Attack");
        }
    }

    void AttackBoxOn()
    {
        attackBox.gameObject.SetActive(true);
    }

    void AttackBoxOff()
    {
        attackBox.gameObject.SetActive(false);
    }

    void FollowPlayer()
    {
        if (transform.position.x - PlayerController.instance.transform.position.x > 0)
        {
            isPlayerToLeft = true;
        }
        else if (transform.position.x - PlayerController.instance.transform.position.x < 0)
        {
            isPlayerToLeft = false;
        }

        if (isPlayerToLeft != wasPlayerToLeft)
        {
            isChangingDirection = true;
        }

        if (isChangingDirection)
        {
            StartCoroutine(DirectionChange());
        }
        else
        {
            if (transform.position.x < PlayerController.instance.transform.position.x)
            {
                // �÷��̾��� ���ʿ� �����Ƿ� ���������� �̵��ؾ� ��, ���������� ���ƺ�����, ���������� ���� �ǹǷ� isFacingLeft = false
                isFacingLeft = false;
                anim.Play("Goul_Fighter_Walk");
                theRB.velocity = new Vector2(moveSpeed, theRB.velocity.y);
                transform.eulerAngles = new Vector3(0, 180, 0);
            }
            else if (transform.position.x > PlayerController.instance.transform.position.x)
            {
                //�÷��̾��� �����ʿ� �����Ƿ� �������� �̵��ؾ� ��, �������� ���ƺ�����
                isFacingLeft = true;
                anim.Play("Goul_Fighter_Walk");
                theRB.velocity = new Vector2(-moveSpeed, theRB.velocity.y);
                transform.eulerAngles = new Vector3(0, 0, 0);
            }
        }
        wasPlayerToLeft = isPlayerToLeft;
    }

    IEnumerator DirectionChange()
    {
        //anim.Play("Goul_Turn");
        yield return new WaitForSeconds(.5f);
        isChangingDirection = false;
    }

    IEnumerator StopFollowingPlayer()
    {
        yield return new WaitForSeconds(timeToStopFollowing);
        theRB.velocity = new Vector2(0, theRB.velocity.y);
        anim.Play("Goul_Fighter_Idle");
        isDetecting = false;
        isSearching = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("HurtBoxPlayer"))
        {
            canSeePlayer = true;
        }
        else
        {
            canSeePlayer = false;
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("HurtBoxPlayer"))
        {
            canSeePlayer = true;
        }
        else
        {
            canSeePlayer = false;
        }
    }
}

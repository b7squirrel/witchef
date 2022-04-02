using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoblinBolt : MonoBehaviour
{
    public float moveSpeed;
    [HideInInspector]
    public bool isParried;
    public Transform groundCheckPoint;
    public LayerMask groundLayer;
    public Transform wallDetectingPoint;
    
    Rigidbody2D _theRB;
    Animator _anim;
    bool _isGrounded;
    [SerializeField]
    int _direction;
    bool _detectingWall;

    private void Start()
    {
        _theRB = GetComponent<Rigidbody2D>();
        _anim = GetComponent<Animator>();
    }

    private void Update()
    {
        _isGrounded = Physics2D.OverlapCircle(groundCheckPoint.position, .2f, groundLayer);
        _detectingWall = Physics2D.OverlapCircle(wallDetectingPoint.position, .2f, groundLayer);


        if (_detectingWall)
        {
            ChangeDirection();
        }
        Direction();
        Run();
    }

    void ChangeDirection()
    {
        if (_direction == 1)
        {
            transform.eulerAngles = new Vector3(0, 0, 0);
        }
        else if(_direction == -1)
        {
            Debug.Log("HERE");
            transform.eulerAngles = new Vector3(0, 180, 0);
        }
    }

    void Direction()
    {
        if (transform.rotation.y == 0)
        {
            Debug.Log("0");
            _direction = -1;
        }
        else
        {
            Debug.Log("180");
            _direction = 1;
        }
    }
    void Run()
    {
        _theRB.velocity = new Vector2(_direction * moveSpeed, _theRB.velocity.y);
    }

}

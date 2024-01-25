using System;
using Enums;
using Interfaces;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerController : MonoBehaviour, IDieAble, IDamageAble
{
    public float moveSpeed;


    public Facing facing;

    private Vector2 _input;
    private Rigidbody2D _rb;
    private Animator _animator;

    private void Start()
    {
        Health = 100;
        _rb = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        _input.x = Input.GetAxisRaw(InputType.Horizontal.ToString());
        _input.y = Input.GetAxisRaw(InputType.Vertical.ToString());

        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log(Input.mousePosition);
        }

        Moving();
    }

    private void Moving()
    {
        _animator.SetFloat(InputType.Horizontal.ToString(), _input.x);
        _animator.SetFloat(InputType.Vertical.ToString(), _input.y);


        _animator.SetFloat($"Speed", _input.sqrMagnitude);
    }

    private void FixedUpdate()
    {
        _rb.velocity = new Vector2(_input.x * moveSpeed, _input.y * moveSpeed);
    }


    private void OnCollisionEnter2D(Collision2D other)
    {
        if (!other.gameObject.CompareTag($"Enemy")) return;


        if (other.gameObject.TryGetComponent(out IDamageAble hit))
        {
            hit.TakeDamage(18);
        }
    }


    public float Health { get; set; }

    public void Die(float waitBeforeDestroy)
    {
        Destroy(gameObject, waitBeforeDestroy);
    }

    public void TakeDamage(float damageTaken)
    {
        if (Health - damageTaken < 0)
        {
            Die(1f);
            Debug.Log("player died!");
        }

        Health -= damageTaken;
    }
    //
    // public void AttackPosition(Direction direction)
    // {
    //     switch (direction)
    //     {
    //         case Direction.Right:
    //             _attackPos = new Vector2
    //             {
    //                 x = 0.07f,
    //                 y = 0.0f
    //             };
    //             _attackTransform.position = _attackPos;
    //             break;
    //         case Direction.Left:
    //             _attackPos = new Vector2();
    //             _attackPos.x = -0.07f;
    //             _attackPos.y = 0.00f;
    //             _attackTransform.position = _attackPos;
    //             break;
    //         case Direction.Top:
    //             _attackPos = new Vector2();
    //             _attackPos.x = 0.0f;
    //             _attackPos.y = 0.07f;
    //             _attackTransform.position = _attackPos;
    //             break;
    //         case Direction.Bottom:
    //             _attackPos = new Vector2();
    //             _attackPos.x = 0.0f;
    //             _attackPos.y = -0.07f;
    //             _attackTransform.position = _attackPos;
    //             break;
    //         default:
    //             throw new ArgumentOutOfRangeException(nameof(direction), direction, null);
    //     }
    // }
}
using System;
using Enums;
using Interfaces;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerController : MonoBehaviour, IDieAble, IDamageAble
{
    public float moveSpeed;
    private bool _isMoving;

    private Vector2 _input;

    private Rigidbody2D _rb;


    private Animator _animator;

    private Transform _attackTransform;


    private void Start()
    {
        Health = 100;
        _rb = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();

        _attackTransform = transform.GetChild(0);
    }

    private void Update()
    {
        _input.x = Input.GetAxisRaw(InputType.Horizontal.ToString());
        _input.y = Input.GetAxisRaw(InputType.Vertical.ToString());


        Facing();
    }

    private void Facing()
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

    public void AttackPosition(Direction direction)
    {
        var attackposition = transform.position + _attackTransform.position;
        switch (direction)
        {
            case Direction.Right:
                attackposition.x = 0.07f;
                attackposition.y = 0.0f;
                _attackTransform.position = attackposition;
                break;
            case Direction.Left:
                attackposition.x = -0.07f;
                attackposition.y = 0.00f;
                _attackTransform.position = attackposition;
                break;
            case Direction.Top:
                attackposition.x = 0.0f;
                attackposition.y = 0.07f;
                _attackTransform.position = attackposition;
                break;
            case Direction.Bottom:
                attackposition.x = 0.0f;
                attackposition.y = -0.07f;
                _attackTransform.position = attackposition;
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(direction), direction, null);
        }
    }
}
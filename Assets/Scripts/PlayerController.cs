using Enums;
using UnityEngine;
using static Enums.Facing;

public class PlayerController : MonoBehaviour
{
    public float _moveSpeed;
    private bool _isMoving;

    private Vector2 _input;

    private Rigidbody2D _rb;


    private Animator _animator;


    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
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
        _rb.velocity = new Vector2(_input.x * _moveSpeed, _input.y * _moveSpeed);
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopDownMovement : MonoBehaviour // 캐릭터의 움직임
{
    private TopDownCharacterController _controller;
    private CharacterStatsHandler _Stats; //여기는 필드?

    private Vector2 _moveDirection = Vector2.zero;
    private Rigidbody2D _rigidbody;

    private Vector2 _knockBack = Vector2.zero;
    private float knockbackDuration = 0.0f;
    private void Awake()
    {
        _controller = GetComponent<TopDownCharacterController>();
        _rigidbody = GetComponent<Rigidbody2D>();
        _Stats = GetComponent<CharacterStatsHandler>();
    }

    private void Start()
    {
        _controller.OnMoveEvent += Move;
    }

    private void FixedUpdate()
    {
        ApplyMove(_moveDirection);

        if (knockbackDuration > 0)
        {
            knockbackDuration -= Time.fixedDeltaTime;
        }
    }

    public void ApplyKnockback(Transform other, float power, float duration)
    {
        knockbackDuration = duration;
        _knockBack = -(other.position - transform.position).normalized * power;
    }

    private void ApplyMovement(Vector2 direction)
    {
        direction = direction * _Stats.CurrentStats.speed;
        if (knockbackDuration > 0f)
        {
            direction += _knockBack;
        }
        _rigidbody.velocity = direction;
    }

    private void Move(Vector2 direction)
    {
        _moveDirection = direction;
    }

    private void ApplyMove(Vector2 direction)
    {
        if (knockbackDuration > 0.0f)
        {
            direction += _knockBack;
        }
        //_rigidbody.velocity = direction * 5;
        _rigidbody.velocity = direction * _Stats.CurrentStats.speed;
    }
}

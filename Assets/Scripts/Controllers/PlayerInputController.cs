using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputController : TopDownCharacterController
{
    private Camera _camera;

    protected override void Awake()
    {
        base.Awake();
        _camera = Camera.main;
    }

    public void OnMove(InputValue value)
    {
        Vector2 movePositionInput = value.Get<Vector2>().normalized; // 입력값의 vector2값을 normalize해서 할당
        CallMoveEvent(movePositionInput); // 받은 값(vector2 형식)으로 CallMoveEvent이벤트 실행
    }

    public void OnLook(InputValue value)
    {
        Vector2 newAim = value.Get<Vector2>(); // 입력값의 vector2값을 할당(현재 마우스 위치)
        // 현재 스크린에 비치는 마우스의 위치를 월드 좌표로 계산해 할당
        Vector2 worldPos = _camera.ScreenToWorldPoint(newAim); 
        // (현재 마우스 좌표 - 플레이어(이 스크립트를 들고 있는 오브젝트)의 위치 좌표)를 normalize해서 할당
        newAim = (worldPos - (Vector2)transform.position).normalized;

        if (newAim.magnitude >= 0.9f) // ?if문? 그냥 방어코든가?
        {
            CallLookEvent(newAim);
        }
    }

    public void OnFire(InputValue value)
    {
        isAttacking = value.isPressed;
    }

    //[SerializeField] private float speed = 5.0f;
    //void Update()
    //{
    //    float x = Input.GetAxisRaw("Horizontal");
    //    float y = Input.GetAxisRaw("Vertical");

    //    transform.position += new Vector3(x, y, 0) * speed * Time.deltaTime;
    //}
}

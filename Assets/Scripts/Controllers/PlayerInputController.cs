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
        Vector2 movePositionInput = value.Get<Vector2>().normalized; // �Է°��� vector2���� normalize�ؼ� �Ҵ�
        CallMoveEvent(movePositionInput); // ���� ��(vector2 ����)���� CallMoveEvent�̺�Ʈ ����
    }

    public void OnLook(InputValue value)
    {
        Vector2 newAim = value.Get<Vector2>(); // �Է°��� vector2���� �Ҵ�(���� ���콺 ��ġ)
        // ���� ��ũ���� ��ġ�� ���콺�� ��ġ�� ���� ��ǥ�� ����� �Ҵ�
        Vector2 worldPos = _camera.ScreenToWorldPoint(newAim); 
        // (���� ���콺 ��ǥ - �÷��̾�(�� ��ũ��Ʈ�� ��� �ִ� ������Ʈ)�� ��ġ ��ǥ)�� normalize�ؼ� �Ҵ�
        newAim = (worldPos - (Vector2)transform.position).normalized;

        if (newAim.magnitude >= 0.9f) // ?if��? �׳� ����ڵ簡?
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

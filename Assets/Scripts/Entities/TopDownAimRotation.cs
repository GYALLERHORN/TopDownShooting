using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopDownAimRotation : MonoBehaviour
{
    private TopDownCharacterController _controller;

    [SerializeField] private SpriteRenderer characterRenderer;
    [SerializeField] private SpriteRenderer weaponRenderer;
    [SerializeField] private Transform weaponPivot;

    private void Awake()
    {
        _controller = GetComponent<TopDownCharacterController>();
    }

    void Start()
    {
        _controller.OnLookEvent += OnAim;
    }

    void OnAim(Vector2 newAimDirection)
    {
        RotateWeapon(newAimDirection);
    }

    void RotateWeapon(Vector2 direction)
    {
        float rotationZ = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        // Debug.Log(rotationZ.ToString());

        weaponRenderer.flipY = Mathf.Abs(rotationZ) > 90.0f;
        characterRenderer.flipX = weaponRenderer.flipY;

        weaponPivot.rotation = Quaternion.Euler(0, 0, rotationZ);
    }
}

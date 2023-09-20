using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopDownShooting : MonoBehaviour
{
    private ProjectileManager _projectileManager;
    
    private TopDownCharacterController _controller;

    [SerializeField] private Transform projectileSpawnPosition;
    private Vector2 _aimDirection = Vector2.right;


    private void Awake()
    {
        _controller = GetComponent<TopDownCharacterController>();
    }

    private void Start()
    {
        _projectileManager = ProjectileManager.instance;
        _controller.OnLookEvent += OnAim;
        _controller.OnAttackEvent += OnShoot;
    }

    void OnAim(Vector2 aimDirection)
    {
        _aimDirection = aimDirection;
    }

    void OnShoot(AttackSO attackSO)
    {
        RangedAttackData rangedAttackData = attackSO as RangedAttackData;
        float projectileAngleSpace = rangedAttackData.multipleProjectileAngle;
        int numberOfProjectilePerShot = rangedAttackData.numberofProjectilesPerShot;

        // �߻�ü�� �ټ��� ��, �߻� ������ �������� �߻�ü ������Ʈ���� ���� ��Ī�� �̷�� �ϴ� ��
        float minAngle = - (numberOfProjectilePerShot / 2f) * projectileAngleSpace + 0.5f * rangedAttackData.multipleProjectileAngle;

        for (int i = 0; i < numberOfProjectilePerShot; i++)
        {
            float angle = minAngle + projectileAngleSpace * i;
            float randomSpread = Random.Range(-rangedAttackData.spread, rangedAttackData.spread);
            angle += randomSpread;

            CreateProjectile(rangedAttackData, angle);
        }

    }

    void CreateProjectile(RangedAttackData rangedAttackData, float angle)
    {
        // ����ü ���� ��ũ��Ʈ �ۼ� �ʿ�
        _projectileManager.ShootBullet(
            projectileSpawnPosition.position,
            RotateVector(_aimDirection, angle),
            rangedAttackData);
    }

    private static Vector2 RotateVector(Vector2 v, float degree)
    {
        return Quaternion.Euler(0, 0, degree) * v;
    }
}

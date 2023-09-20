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

        // 발사체가 다수일 때, 발사 방향을 기준으로 발사체 오브젝트들의 각이 대칭을 이루게 하는 식
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
        // 투사체 관리 스크립트 작성 필요
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

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum StatsChangeType
{
    Add,
    Multiple,
    Override
}

[Serializable]
public class CharacterStats // monobehavior 삭제
{
    public StatsChangeType statChangeType;

    [Range(1, 100)] public int maxHealth;
    [Range(1f, 100f)] public float speed;

    // 공격 데이터
    public AttackSO attackSO;
}



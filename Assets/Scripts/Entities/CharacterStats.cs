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
public class CharacterStats // monobehavior ����
{
    public StatsChangeType statChangeType;

    [Range(1, 100)] public int maxHealth;
    [Range(1f, 100f)] public float speed;

    // ���� ������
    public AttackSO attackSO;
}



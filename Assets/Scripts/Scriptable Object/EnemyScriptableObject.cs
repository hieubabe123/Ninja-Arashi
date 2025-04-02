using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Enemy Data", menuName = "Data/Enemy")]
public class EnemyScriptableObject : ScriptableObject
{
    [SerializeField] private int lifeCount;
    public int LifeCount { get => lifeCount; private set => lifeCount = value; }

    [SerializeField] private float moveSpeedNormal;
    public float MoveSpeedNormal { get => moveSpeedNormal; private set => moveSpeedNormal = value; }

    [SerializeField] private float moveSpeedDetected;
    public float MoveSpeedDetected { get => moveSpeedDetected; private set => moveSpeedDetected = value; }

    [SerializeField] private int detectionDistance;
    public int DetectionDistance { get => detectionDistance; private set => detectionDistance = value; }

    [SerializeField] private int attackDistance;
    public int AttackDistance { get => attackDistance; private set => attackDistance = value; }
}

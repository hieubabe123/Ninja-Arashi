using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Heal And Shield Upgrade Data", menuName = "Data/Skill Upgrade/Heal & Shield")]
public class HealAndShieldScriptableObject : SkillUpgradeScriptableObject
{
    [SerializeField] private int lifeCount;
    public int LifeCount { get => lifeCount; private set => lifeCount = value; }

    [SerializeField] private int shieldDuration;
    public int ShieldDuration { get => shieldDuration; private set => shieldDuration = value; }
}

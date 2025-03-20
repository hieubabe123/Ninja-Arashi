using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[CreateAssetMenu(fileName = "Throw Shuriken Upgrade Data", menuName = "Data/Skill Upgrade/Throw Shuriken")]
public class ThrowShurikenScriptableObject : SkillUpgradeScriptableObject
{
    [SerializeField] private float cooldown;
    public float Cooldown { get => cooldown; private set => cooldown = value; }

    [SerializeField] private int damageShuriken;
    public int DamageShuriken { get => damageShuriken; private set => damageShuriken = value; }

    [SerializeField] private int criticalChance;
    public int CriticalChance { get => criticalChance; private set => criticalChance = value; }
}

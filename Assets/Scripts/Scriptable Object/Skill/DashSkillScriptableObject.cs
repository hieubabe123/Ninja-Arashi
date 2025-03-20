using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Dash Skill Upgrade Data", menuName = "Data/Skill Upgrade/Dash")]
public class DashSkillScriptableObject : SkillUpgradeScriptableObject
{

    [SerializeField] private float cooldown;
    public float Cooldown { get => cooldown; private set => cooldown = value; }

    [SerializeField] private bool canDestroyBearTrap;
    public bool CanDestroyBearTrap { get => canDestroyBearTrap; private set => canDestroyBearTrap = value; }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Camouflage Skill Upgrade Data", menuName = "Data/Skill Upgrade/Camouflage")]
public class CamouflageScriptableObject : SkillUpgradeScriptableObject
{
    [SerializeField] private int cooldown;
    public int Cooldown { get => cooldown; private set => cooldown = value; }

    [SerializeField] private int diguiseDuration;
    public int DiguiseDuration { get => diguiseDuration; private set => diguiseDuration = value; }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Skill Upgrade Data", menuName = "Data/Skill Upgrade")]
public class SkillUpgradeScriptableObject : ScriptableObject
{
    [SerializeField] private int moneyToUpgrade;
    public int MoneyToUpgrade { get => moneyToUpgrade; private set => moneyToUpgrade = value; }

    [SerializeField] private int gemToUpgrade;
    public int GemToUpgrade { get => gemToUpgrade; private set => gemToUpgrade = value; }

    [SerializeField] private string skillDescription;
    public string SkillDescription { get => skillDescription; private set => skillDescription = value; }

}

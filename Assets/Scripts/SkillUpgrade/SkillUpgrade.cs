using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SkillUpgrade : MonoBehaviour
{
    [SerializeField] private TMP_Text moneyText;
    [SerializeField] private TMP_Text gemText;

    [SerializeField] private TMP_Text description;
    [SerializeField] private SkillUpgradeScriptableObject skillData;
    // Start is called before the first frame update
    void Start()
    {
        description.text = skillData.SkillDescription;
        moneyText.text = skillData.MoneyToUpgrade.ToString();
        gemText.text = skillData.GemToUpgrade.ToString();
    }
}

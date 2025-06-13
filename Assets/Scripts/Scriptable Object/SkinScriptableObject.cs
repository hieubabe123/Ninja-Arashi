using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Skin Data", menuName = "Data/Skin")]
public class SkinScriptableObject : ScriptableObject
{
    [SerializeField] private string skinName;
    public string SkinName { get => skinName; private set => skinName = value; }

    [SerializeField] private int cost;
    public int Cost { get => cost; private set => cost = value; }

    [SerializeField] private Sprite skinSprite;
    public Sprite SkinSprite { get => skinSprite; private set => skinSprite = value; }
}

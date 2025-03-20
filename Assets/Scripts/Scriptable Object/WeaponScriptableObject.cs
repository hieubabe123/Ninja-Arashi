using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Weapon Data", menuName = "Data/Weapon")]
public class WeaponScriptableObject : ScriptableObject
{
    [SerializeField] private float timeToDestroy;
    public float TimeToDestroy { get => timeToDestroy; private set => timeToDestroy = value; }

    [SerializeField] private float speed;
    public float Speed { get => speed; private set => speed = value; }
}

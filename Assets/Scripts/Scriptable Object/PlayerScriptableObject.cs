using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Player Data", menuName = "Data/Player")]
public class PlayerScriptableObject : ScriptableObject
{

    [SerializeField] private GameObject shurikenPrefab;
    public GameObject ShurikenPrefab { get => shurikenPrefab; private set => shurikenPrefab = value; }

    [SerializeField] private float moveSpeed;
    public float MoveSpeed { get => moveSpeed; private set => moveSpeed = value; }

    [SerializeField] private float moveSpeedWhenCamouflaging;
    public float MoveSpeedWhenCamouflaging { get => moveSpeedWhenCamouflaging; private set => moveSpeedWhenCamouflaging = value; }

    [SerializeField] private float jumpForce;
    public float JumpForce { get => jumpForce; private set => jumpForce = value; }

    [SerializeField] private int money;
    public int Money { get => money; set => money = value; }

    [SerializeField] private int gem;
    public int Gem { get => gem; set => gem = value; }

    [SerializeField] private int scrollPaper;
    public int ScrollPaper { get => scrollPaper; private set => scrollPaper = value; }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private PlayerMovement player;
    public Animator animator;


    private void Awake()
    {
        player = GetComponent<PlayerMovement>();
        animator = GetComponent<Animator>();
    }


    private void Update()
    {
        CheckBoolMoving();
        CheckBoolIsGround();
        CheckBoolKeepingWall();
        CheckBoolAttacking();
        CheckBoolDashing();
        SetFloatJumpStatus();
        CheckBeKilled();
    }

    private void CheckBoolMoving()
    {
        animator.SetBool("isMoving", player.isMoving);
    }

    private void CheckBoolIsGround()
    {
        animator.SetBool("isGround", player.isGround);
    }

    private void CheckBoolKeepingWall()
    {
        animator.SetBool("isWalling", player.isWalling);
    }

    private void CheckBoolAttacking()
    {
        animator.SetBool("canAttack", player.canAttack);
    }

    private void CheckBoolDashing()
    {
        animator.SetBool("isDashing", player.isDashing);
    }

    private void SetFloatJumpStatus()
    {
        animator.SetFloat("jumpStatus", player.jumpStatus);
    }

    private void CheckBeKilled()
    {
        animator.SetBool("isKilled", player.isKilled);
    }

}

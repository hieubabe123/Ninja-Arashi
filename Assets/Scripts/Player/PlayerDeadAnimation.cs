using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeadAnimation : MonoBehaviour
{
    private PlayerMovement player;
    private Animator animator;

    private void Awake()
    {
        player = FindObjectOfType<PlayerMovement>();
        animator = GetComponent<Animator>();
    }
    private void Update()
    {
        CheckAnimDead();
    }

    void OnDisable()
    {
        animator.ResetTrigger("DeadByEnemies");
        animator.ResetTrigger("DeadByElectric");
        animator.ResetTrigger("DeadByPoison");
        animator.ResetTrigger("DeadByFire");

    }

    private void CheckAnimDead()
    {
        if (player.isDeadByEnemies)
        {
            animator.SetTrigger("DeadByEnemies");
        }
        else if (player.isDeadByElectric)
        {
            animator.SetTrigger("DeadByElectric");
        }
        else if (player.isDeadByPoison)
        {
            animator.SetTrigger("DeadByPoison");
        }
        else if (player.isDeadByFire)
        {
            animator.SetTrigger("DeadByFire");
        }
    }
}


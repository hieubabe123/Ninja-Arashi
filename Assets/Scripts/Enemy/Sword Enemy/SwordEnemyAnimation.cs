using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordEnemyAnimation : MonoBehaviour
{
    private Animator animator;
    public SwordEnemy enemy;
    public bool isDead;

    void Awake()
    {
        enemy = GetComponent<SwordEnemy>();
        animator = GetComponentInChildren<Animator>();
    }
    void Update()
    {
        IsDead();
        CheckDetectingPlayer();
        CheckAttackPlayer();

    }

    private void IsDead()
    {
        if (enemy.isDead)
        {
            animator.SetTrigger("Dead");
        }
    }

    private void CheckDetectingPlayer()
    {
        animator.SetBool("isDetecting", enemy.isDetecting);
    }

    private void CheckAttackPlayer()
    {
        animator.SetBool("canAttack", enemy.canAttack);
    }
}

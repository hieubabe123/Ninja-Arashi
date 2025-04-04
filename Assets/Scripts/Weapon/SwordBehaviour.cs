using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordBehaviour : MonoBehaviour
{
    private EnemyStats enemy;
    void Awake()
    {
    }
    void Start()
    {
        enemy = GetComponentInParent<EnemyStats>();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && enemy.isDetecting && enemy.canAttack)
        {
            PlayerMovement player = FindObjectOfType<PlayerMovement>();
            if (player != null && !player.haveShield)
            {
                player.Kill();
                player.isDeadByEnemies = true;
                AudioManager.instance.PlayListSFX(AudioManager.instance.playerDeadByEnemySFX);
            }
            else
            {
                return;
            }
        }
    }
}

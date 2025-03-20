using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordBehaviour : MonoBehaviour
{
    private EnemyStats enemy;
    // Start is called before the first frame update
    void Start()
    {
        enemy = GetComponentInParent<EnemyStats>();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && enemy.isDetecting && enemy.canAttack)
        {
            PlayerMovement player = FindObjectOfType<PlayerMovement>();
            if (!player.isImmortal)
            {
                player.Kill();
            }
            else
            {
                return;
            }
        }
    }
}

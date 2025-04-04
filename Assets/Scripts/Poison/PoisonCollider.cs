using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoisonCollider : MonoBehaviour
{
    private PlayerMovement player;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            player = FindObjectOfType<PlayerMovement>();
            if (player.haveShield == false)
            {
                player.Kill();
                player.isDeadByPoison = true;
                AudioManager.instance.PlayListSFX(AudioManager.instance.playerDeadByPoisonSFX);
            }
            else
            {
                return;
            }
        }
    }
}

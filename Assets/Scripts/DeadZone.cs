using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadZone : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerMovement player = FindObjectOfType<PlayerMovement>();
        if (collision.gameObject.CompareTag("Player"))
        {
            if (player != null)
            {
                player.Kill();
                AudioManager.instance.PlaySFX(AudioManager.instance.playerDeadByJumpSFX);
            }
        }
    }
}

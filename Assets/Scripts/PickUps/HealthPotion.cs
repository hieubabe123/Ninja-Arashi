using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPotion : PickUps
{
    public int healthToHealth;

    public override void Collect()
    {
        if (hasBeenCollected)
        {
            return;
        }
        else
        {
            base.Collect();
        }
        PlayerMovement player = FindObjectOfType<PlayerMovement>();
        player.RestoreHealth(healthToHealth);
    }
}

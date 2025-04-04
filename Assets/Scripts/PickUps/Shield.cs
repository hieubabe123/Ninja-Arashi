using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : PickUps
{
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
        player.TakeShield();
    }
}

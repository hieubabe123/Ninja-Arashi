using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollPaper : PickUps
{
    public int scrollPaperToEarn;

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
        player.GetScrollPaper(scrollPaperToEarn);
    }
}

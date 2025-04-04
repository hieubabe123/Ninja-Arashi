using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class TrapRotation : MonoBehaviour
{
    private Vector3 minRotation = new Vector3(0, 0, -265);
    private Vector3 maxRotation = new Vector3(0, 0, -110);
    // Start is called before the first frame update
    void Start()
    {
    }

    private void RotateTrap()
    {
        transform.DORotate(maxRotation, 2f).SetEase(Ease.InOutSine).OnComplete(() => { transform.DORotate(minRotation, 2f).SetEase(Ease.InOutSine).OnComplete(RotateTrap); });
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerMovement player = FindObjectOfType<PlayerMovement>();
            if (!player.haveShield)
            {
                player.Kill();
                player.isDeadByEnemies = true;
            }
            else
            {
                return;
            }
        }
    }

}

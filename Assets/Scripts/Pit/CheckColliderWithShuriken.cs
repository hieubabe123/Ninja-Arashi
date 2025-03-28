using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckColliderWithShuriken : MonoBehaviour
{
    [SerializeField] private GameObject hidePit;
    [SerializeField] private GameObject nextPieceOfWallPPit;
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Weapon"))
        {
            gameObject.SetActive(false);
            if (nextPieceOfWallPPit != null)
            {
                nextPieceOfWallPPit.SetActive(true);
            }
            if (hidePit != null)
            {
                Destroy(hidePit);
            }
        }
    }
}

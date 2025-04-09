using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapRockRoll : MonoBehaviour
{
    [SerializeField] private GameObject rockRoll;
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            rockRoll.SetActive(true);
        }
    }
}

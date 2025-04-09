using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckTrapSpear : MonoBehaviour
{
    private TrapSpear trapSpear;

    void Start()
    {
        trapSpear = GetComponentInParent<TrapSpear>();
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            trapSpear.SetAnimatorTriggerActive();
        }
    }
}

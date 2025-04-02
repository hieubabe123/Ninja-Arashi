using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectDestroy : MonoBehaviour
{
    public GameObject secretGift;
    [SerializeField] private ParticleSystem brokenParticalSystem;
    private PlayerMovement player;

    void Awake()
    {
        player = GetComponent<PlayerMovement>();
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Weapon"))
        {
            DestroyObject();
        }
    }

    public void DestroyObject()
    {

        Destroy(gameObject);
        Instantiate(secretGift, transform.position, Quaternion.identity);
        Instantiate(brokenParticalSystem, transform.position, Quaternion.identity);
    }
}

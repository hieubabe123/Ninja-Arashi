using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class ShurikenBehaviour : MonoBehaviour
{
    [SerializeField] private WeaponScriptableObject weaponData;
    [SerializeField] private PlayerScriptableObject playerData;
    public SwordEnemy[] enemies;
    public float lastMoveDirX;

    private int currentDamage;
    private float currentSpeed;
    private float currentTimeDestroy;

    private float direction;
    private void OnDisable()
    {
        currentDamage = DataManager.instance.currentThrowShurikenData.DamageShuriken;
        currentSpeed = weaponData.Speed;
        currentTimeDestroy = weaponData.TimeToDestroy;
        enemies = FindObjectsOfType<SwordEnemy>();

    }

    private void OnEnable()
    {
        direction = FindObjectOfType<PlayerMovement>().lastMoveDirX;
    }

    private void Update()
    {
        ShootAndRotate();
        currentTimeDestroy -= Time.deltaTime;
        if (currentTimeDestroy <= 0)
        {
            gameObject.SetActive(false);
        }
    }



    private void ShootAndRotate()
    {
        transform.position += new Vector3(direction * Time.deltaTime * currentSpeed, 0, 0);
        transform.Rotate(0, 0, 360 * 4 * Time.deltaTime);
    }

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            if (collision.gameObject.TryGetComponent(out SwordEnemy enemy))
            {
                enemy.TakeDamage(currentDamage);
            }
            gameObject.SetActive(false);
        }
        else if (collision.gameObject.CompareTag("Wall") || collision.gameObject.CompareTag("Wood") || collision.gameObject.CompareTag("Ground"))
        {
            gameObject.SetActive(false);
        }
    }





}

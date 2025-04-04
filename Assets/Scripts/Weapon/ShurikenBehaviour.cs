using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class ShurikenBehaviour : MonoBehaviour
{
    [Header("------------------- Data -------------------")]
    [SerializeField] private WeaponScriptableObject weaponData;
    [SerializeField] private PlayerScriptableObject playerData;

    [Header("------------------- Effect -------------------")]
    [SerializeField] private ParticleSystem hitGroundAndWallEffect;
    [SerializeField] private ParticleSystem hitEnemyEffect;
    [SerializeField] private ParticleSystem hitTrapEffect;


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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            if (collision.gameObject.TryGetComponent(out EnemyStats enemy))
            {
                enemy.TakeDamage(currentDamage);
                Instantiate(hitEnemyEffect, transform.position, Quaternion.identity);
                AudioManager.instance.PlayListSFX(AudioManager.instance.shurikenHitEnemySFX);
            }
            gameObject.SetActive(false);
        }
        else if (collision.gameObject.CompareTag("Wall") || collision.gameObject.CompareTag("Wood") || collision.gameObject.CompareTag("Ground") || collision.gameObject.CompareTag("Crystal"))
        {
            Instantiate(hitGroundAndWallEffect, this.transform.position, Quaternion.identity);
            gameObject.SetActive(false);
            if (collision.gameObject.CompareTag("Wall") || collision.gameObject.CompareTag("Ground"))
            {
                AudioManager.instance.PlaySFX(AudioManager.instance.shurikenHitSFX);
            }
            else if (collision.gameObject.CompareTag("Wood"))
            {
                AudioManager.instance.PlaySFX(AudioManager.instance.shurikenHitWoodSFX);
            }
            else if (collision.gameObject.CompareTag("Crystal"))
            {
                AudioManager.instance.PlayListSFX(AudioManager.instance.shurikenHitCrystalSFX);
            }
        }
        else if (collision.gameObject.CompareTag("Trap"))
        {
            Instantiate(hitTrapEffect, this.transform.position, Quaternion.identity);
            gameObject.SetActive(false);
            AudioManager.instance.PlaySFX(AudioManager.instance.shurikenHitTrapSFX);
        }
    }





}

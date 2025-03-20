using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShurikenBehaviour : MonoBehaviour
{
    [SerializeField] private WeaponScriptableObject weaponData;
    [SerializeField] private PlayerScriptableObject playerData;
    [SerializeField] private EnemyStats enemy;
    public float lastMoveDirX;

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private CircleCollider2D cc;

    private int currentDamage;
    private float currentSpeed;
    private float currentTimeDestroy;

    private float direction;

    private void Awake()
    {
        cc = GetComponent<CircleCollider2D>();
        rb = GetComponent<Rigidbody2D>();

        enemy = FindObjectOfType<EnemyStats>();
    }


    private void OnDisable()
    {
        currentDamage = SkillUpgradeManager.instance.currentThrowShurikenData.DamageShuriken;
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
        if (collision.gameObject.CompareTag("Enemy"))
        {
            enemy.TakeDamage(currentDamage);
            gameObject.SetActive(false);
        }
        else if (collision.gameObject.CompareTag("Wall") || collision.gameObject.CompareTag("Wood"))
        {
            gameObject.SetActive(false);
        }
    }



}

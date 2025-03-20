using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyStats : MonoBehaviour
{
    private Rigidbody2D rb;
    [SerializeField] private EnemyScriptableObject enemyData;
    [SerializeField] private GameObject player;
    [SerializeField] private Transform waypoint_1;
    [SerializeField] private Transform waypoint_2;
    private Vector2 rayDetectingDirection;
    private Vector2 rayAttackingDirection;


    [Header("-------------------Detection Settings-------------------")]
    public LayerMask playerLayer;
    public bool isDetecting = false;
    public bool canAttack = false;

    private Transform currentTarget;
    private int currentLifeCount;
    private float currentmoveSpeed;
    private int currentDamage;
    private int currentDetectionDistance;
    private int currentAttackDistance;

    public bool isDead = false;

    private void Awake()
    {
        rb = GetComponentInChildren<Rigidbody2D>();
    }

    public virtual void Start()
    {
        currentLifeCount = enemyData.LifeCount;
        currentmoveSpeed = enemyData.MoveSpeedNormal;
        currentDamage = enemyData.Damage;
        currentDetectionDistance = enemyData.DetectionDistance;
        currentAttackDistance = enemyData.AttackDistance;

        currentTarget = waypoint_1;
    }

    public virtual void Update()
    {
        if (isDead)
        {
            return;
        }
        MoveToNextWaypoint();
        DetectingPlayer();
    }

    public void MoveToNextWaypoint()
    {
        transform.position = Vector3.MoveTowards(transform.position, currentTarget.position, currentmoveSpeed * Time.deltaTime);
        FlipSprite();

        if (Vector3.Distance(transform.position, currentTarget.position) < 0.1f)
        {
            if (currentTarget == waypoint_1)
            {
                currentTarget = waypoint_2;
            }
            else
            {
                currentTarget = waypoint_1;
            }
        }
    }

    private void FlipSprite()
    {
        if (currentTarget == waypoint_1)
        {
            this.transform.localScale = new Vector2(1, 1);
        }
        else
        {

            this.transform.localScale = new Vector2(-1, 1);
        }
    }

    public void TakeDamage(int dmg)
    {
        currentLifeCount -= dmg;
        if (currentLifeCount <= 0)
        {
            Dead();
        }
    }

    private void Dead()
    {
        Debug.Log("Deadd");
        isDead = true;
        rb.bodyType = RigidbodyType2D.Static;
        rb.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezePositionY;

    }

    private void DetectingPlayer()
    {
        if (this.transform.localScale.x == 1)
        {
            rayDetectingDirection = Vector2.right;
            rayAttackingDirection = Vector2.right;
        }
        else
        {
            rayDetectingDirection = Vector2.left;
            rayAttackingDirection = Vector2.left;
        }


        RaycastHit2D attackHit = Physics2D.Raycast(transform.position, rayAttackingDirection, currentAttackDistance, playerLayer);
        RaycastHit2D hit = Physics2D.Raycast(transform.position, rayDetectingDirection, currentDetectionDistance, playerLayer);

        if (player.GetComponent<PlayerMovement>().isKilled && player.GetComponent<PlayerMovement>().isDashing)
        {
            canAttack = false;
            return;
        }

        //Need to improve
        if (attackHit.collider != null)
        {
            currentmoveSpeed = 0;
            canAttack = true;
        }
        else
        {
            canAttack = false;
            if (hit.collider != null)
            {
                currentmoveSpeed = enemyData.MoveSpeedDetected;
                isDetecting = true;
            }
            else
            {
                isDetecting = false;
                currentmoveSpeed = enemyData.MoveSpeedNormal;
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (this.transform.localScale.x == 1)
        {
            rayDetectingDirection = Vector2.right;
        }
        else
        {
            rayDetectingDirection = Vector2.left;
        }
        Gizmos.color = Color.green;
        Gizmos.DrawLine(transform.position, (Vector2)transform.position + rayDetectingDirection * currentDetectionDistance);

        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, (Vector2)transform.position + rayAttackingDirection * currentAttackDistance);
    }









}

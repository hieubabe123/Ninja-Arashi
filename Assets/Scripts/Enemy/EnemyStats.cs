using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public abstract class EnemyStats : MonoBehaviour
{
    private Rigidbody2D rb;
    private BoxCollider2D boxCollider;
    private CircleCollider2D circleCollider;
    [SerializeField] private EnemyScriptableObject enemyData;
    [SerializeField] private GameObject player;
    [SerializeField] private Transform waypoint_1;
    [SerializeField] private Transform waypoint_2;
    [SerializeField] private Transform rayDetectPlayerPosition;
    private Vector2 rayDetectingDirection;
    private Vector2 rayAttackingAheadDirection;


    [Header("-------------------Detection Settings-------------------")]
    public LayerMask playerLayer;
    public bool isDetecting = false;
    public bool canAttack = false;
    private bool hasDetectedPlayerSFX = false;
    private bool hasAttackedPlayerSFX = false;
    private bool hasNotDetectedPlayerSFX = true;
    public Transform currentTarget;


    [Header("------------------- Stats -------------------")]
    private int currentLifeCount;
    private float currentmoveSpeed;
    private int currentDetectionDistance;
    private int currentAttackDistance;

    public bool isDead = false;

    private void Awake()
    {
        rb = GetComponentInChildren<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();
        circleCollider = GetComponent<CircleCollider2D>();
    }

    public virtual void Start()
    {
        currentLifeCount = enemyData.LifeCount;
        currentmoveSpeed = enemyData.MoveSpeedNormal;
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

        if (Vector3.Distance(transform.position, currentTarget.position) < 1f)
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
            AudioManager.instance.PlayListSFX(AudioManager.instance.enemyDeadSFX);
        }
    }

    private void Dead()
    {
        isDead = true;
        rb.bodyType = RigidbodyType2D.Static;
        if (boxCollider != null)
        {
            boxCollider.enabled = false;
        }
        if (circleCollider != null)
        {
            circleCollider.enabled = false;
        }
        rb.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezePositionY;

    }

    private void DetectingPlayer()
    {
        if (this.transform.localScale.x == 1)
        {
            rayDetectingDirection = Vector2.right;
            rayAttackingAheadDirection = Vector2.right;
        }
        else
        {
            rayDetectingDirection = Vector2.left;
            rayAttackingAheadDirection = Vector2.left;
        }


        RaycastHit2D attackHitAhead = Physics2D.Raycast(rayDetectPlayerPosition.transform.position, rayAttackingAheadDirection, currentAttackDistance, playerLayer);
        RaycastHit2D detectionHit = Physics2D.Raycast(rayDetectPlayerPosition.transform.position, rayDetectingDirection, currentDetectionDistance, playerLayer);

        if (player.GetComponent<PlayerMovement>().isKilled || player.GetComponent<PlayerMovement>().isDashing)
        {
            canAttack = false;
            return;
        }

        if (attackHitAhead.collider != null)
        {
            currentmoveSpeed = 0;
            canAttack = true;
            if (!hasAttackedPlayerSFX)
            {
                AudioManager.instance.PlayListSFX(AudioManager.instance.enemyAttackSFX);
                hasAttackedPlayerSFX = true;
            }
        }
        else
        {
            canAttack = false;
            hasAttackedPlayerSFX = false;
            if (detectionHit.collider != null)
            {
                currentmoveSpeed = enemyData.MoveSpeedDetected;
                isDetecting = true;
                if (!hasDetectedPlayerSFX)
                {
                    AudioManager.instance.PlayListSFX(AudioManager.instance.enemyDetectSFX);
                    hasDetectedPlayerSFX = true;
                    hasNotDetectedPlayerSFX = false;
                }
            }
            else
            {
                hasDetectedPlayerSFX = false;
                isDetecting = false;
                currentmoveSpeed = enemyData.MoveSpeedNormal;
                if (!hasNotDetectedPlayerSFX)
                {
                    AudioManager.instance.PlayListSFX(AudioManager.instance.enemyNotDetectSFX);
                    hasNotDetectedPlayerSFX = true;
                }
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
        Gizmos.DrawLine(transform.position, (Vector2)transform.position + rayAttackingAheadDirection * currentAttackDistance);
    }
}

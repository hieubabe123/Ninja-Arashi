using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [Header("------------------Player Data------------------")]
    public PlayerScriptableObject playerData;

    public DashSkillScriptableObject currentDashData;
    public ThrowShurikenScriptableObject currentThrowShurikenData;
    public HealAndShieldScriptableObject currenthealAndShieldData;
    public CamouflageScriptableObject currentCamouflageData;



    private EnemyStats enemy;
    [SerializeField] private PlayerInputAction playerInputAction;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform shurikenSpawnPos;
    [SerializeField] private GameObject deadPlayerPrefab;



    [Header("---------------Stats---------------")]
    public float currentMoveSpeed;
    public float currentjumpForce;
    public float currentCooldownShuriken;
    public float currentCooldownCamouflage;
    private int currentLifeCount;
    private int currentMoney;
    private int currentGem;
    private int currentScrollPaper;
    private float originalGravityScale;

    public float currentCooldownDashKill;



    //Seperate private stat in PlayerStats script and child stat in another script
    public int CurrentLifeCount
    {
        get { return currentLifeCount; }
        set
        {
            if (currentLifeCount != value)
            {
                currentLifeCount = value;
                if (GameManager.instance != null)
                {
                    GameManager.instance.currentLifeCountDisplay.text = CurrentLifeCount.ToString();
                }
            }
        }
    }

    public int CurrentMoney
    {
        get { return currentMoney; }
        set
        {
            if (currentMoney != value)
            {
                currentMoney = value;
                if (GameManager.instance != null)
                {
                    GameManager.instance.currentMoneyDisplay.text = currentMoney.ToString();
                }
            }
        }
    }
    public int CurrentGem
    {
        get { return currentGem; }
        set
        {
            if (currentGem != value)
            {
                currentGem = value;
                if (GameManager.instance != null)
                {
                    GameManager.instance.currentGemDisplay.text = currentGem.ToString();
                }
            }
        }
    }

    public int CurrentScrollPaper
    {
        get { return currentScrollPaper; }
        set
        {
            if (currentScrollPaper != value)
            {
                currentScrollPaper = value;
                if (GameManager.instance != null)
                {
                    GameManager.instance.currentScrollPaperDisplay.text = currentScrollPaper.ToString();
                }
            }
        }
    }


    private float timeToImmortal;


    [Header("---------------- Check State of Player --------------")]
    public bool isMoving;
    public bool isGround;
    public bool isWalling;
    private bool canJumping;
    public bool canAttack;
    public bool isDashing;
    public bool isCamouflage;
    public bool isKilled = false;
    public bool isImmortal;
    public float jumpStatus;
    public float throwStatus;
    [SerializeField] private float dashDistance = 5f;
    [SerializeField] private float dashDuration = 0.5f;
    private static int doubleJump = 2;
    private int currentJump;

    public float lastMoveDirX;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        playerInputAction = GetComponent<PlayerInputAction>();
        enemy = FindObjectOfType<EnemyStats>();


        currentDashData = SkillUpgradeManager.instance.currentDashData;
        currentCamouflageData = SkillUpgradeManager.instance.currentCamouflageData;
        currentThrowShurikenData = SkillUpgradeManager.instance.currentThrowShurikenData;
        currenthealAndShieldData = SkillUpgradeManager.instance.currenthealAndShieldData;

        currentMoveSpeed = playerData.MoveSpeed;
        currentjumpForce = playerData.JumpForce;
        currentCooldownShuriken = SkillUpgradeManager.instance.currentThrowShurikenData.Cooldown;
        currentCooldownDashKill = SkillUpgradeManager.instance.currentDashData.Cooldown;
        currentCooldownCamouflage = SkillUpgradeManager.instance.currentCamouflageData.Cooldown;
        CurrentLifeCount = SkillUpgradeManager.instance.currenthealAndShieldData.LifeCount;
        CurrentMoney = playerData.Money;
        CurrentGem = playerData.Gem;
        CurrentScrollPaper = playerData.ScrollPaper;
    }

    private void Start()
    {
        isImmortal = false;
        timeToImmortal = 0;
        lastMoveDirX = 1;
        originalGravityScale = rb.gravityScale;

        GameManager.instance.currentLifeCountDisplay.text = CurrentLifeCount.ToString();
        GameManager.instance.currentMoneyDisplay.text = CurrentMoney.ToString();
        GameManager.instance.currentGemDisplay.text = CurrentGem.ToString();
        GameManager.instance.currentScrollPaperDisplay.text = CurrentScrollPaper.ToString();
    }

    private void Update()
    {
        if (timeToImmortal <= 0)
        {
            isImmortal = false;
        }
        else
        {
            timeToImmortal -= Time.deltaTime;
        }
        if (currentJump <= 0)
        {
            canJumping = false;
        }
        currentCooldownCamouflage -= Time.deltaTime;
        currentCooldownShuriken -= Time.deltaTime;
        currentCooldownDashKill -= Time.deltaTime;
        if (currentCooldownShuriken > 0)
        {
            canAttack = false;
        }
        if (currentCooldownCamouflage > 0)
        {
            isCamouflage = false;
        }
    }

    private void FixedUpdate()
    {
        if (isDashing)
        {
            return;
        }
        Move();
        FlipSprite();
        jumpStatus = rb.velocity.y;
    }

    public Vector2 MoveDirection()
    {
        return new Vector2(playerInputAction.GetNormalizedDirection().x, 0);
    }

    public void Move()
    {
        if (isDashing)
        {
            return;
        }
        Vector2 moveDir = new Vector2(playerInputAction.GetNormalizedDirection().x, 0);
        float moveDistance = currentMoveSpeed * Time.deltaTime;
        rb.velocity = new Vector2(moveDistance * moveDir.x, rb.velocity.y);
        Debug.Log("Moving");
        isMoving = Mathf.Abs(rb.velocity.x) > 0;
        if (moveDir.x != 0)
        {
            lastMoveDirX = moveDir.x;
        }
    }

    private void FlipSprite()
    {
        Vector2 playerScale = new Vector2(Mathf.Sign(lastMoveDirX), 1f);
        this.transform.localScale = playerScale;
    }

    public void Jump()
    {
        if (!canJumping)
        {
            return;
        }
        rb.velocity = new Vector2(rb.velocity.x, 0);
        rb.AddForce(currentjumpForce * Vector3.up, ForceMode2D.Impulse);
        currentJump--;
    }

    public void Fire()
    {
        currentCooldownShuriken = SkillUpgradeManager.instance.currentThrowShurikenData.Cooldown;
        canAttack = true;

        GameObject shuriken = ObjectPooling.instance.GetObjectFromPool();
        if (shuriken != null)
        {
            shuriken.transform.position = shurikenSpawnPos.position;
            shuriken.SetActive(true);
        }
    }


    public void DashKill()
    {
        if (isDashing && currentCooldownDashKill > 0)
        {
            return;
        }
        isDashing = true;

        originalGravityScale = rb.gravityScale;
        rb.gravityScale = 0;

        rb.velocity = Vector2.zero;
        float dashVelocity = dashDistance / dashDuration;
        rb.velocity = new Vector2(lastMoveDirX * dashVelocity, 0);


        currentCooldownDashKill = SkillUpgradeManager.instance.currentThrowShurikenData.Cooldown;
        StartCoroutine(DashCoroutine());
    }

    public void Camouflage()
    {
        if (currentCooldownCamouflage > 0)
        {
            return;
        }
        isCamouflage = true;
    }

    private IEnumerator DashCoroutine()
    {
        yield return new WaitForSeconds(dashDuration);
        isDashing = false;
        rb.gravityScale = originalGravityScale;
    }



    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGround = true;
            isWalling = false;
            canJumping = true;
            currentJump = doubleJump;
        }
        if (collision.gameObject.CompareTag("Wall"))
        {
            isWalling = true;
            canJumping = true;
            currentJump = doubleJump;
        }
        if (collision.gameObject.CompareTag("Trap"))
        {
            if (!isImmortal)
            {
                Kill();
            }
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy") && isDashing)
        {
            enemy.TakeDamage(100);
        }

    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGround = false;
        }
        if (collision.gameObject.CompareTag("Wall"))
        {
            isWalling = false;
        }
    }

    public void Kill()
    {
        currentLifeCount--;
        deadPlayerPrefab.SetActive(true);
        deadPlayerPrefab.transform.position = this.gameObject.transform.position;
        if (currentLifeCount > 0)
        {
            CheckpointManager.instance.RespawnPlayer(this.gameObject, deadPlayerPrefab, 2f);
        }
        else
        {
            GameManager.instance.GameOver();
        }
        this.gameObject.SetActive(false);
    }

    #region Collect Items

    public void RestoreHealth(int amount)
    {
        CurrentLifeCount += amount;
    }

    public void TakeShield(float time)
    {
        isImmortal = true;
        timeToImmortal += time;
    }

    public void GetMoney(int amount)
    {
        CurrentMoney += amount;
    }

    public void GetGem(int amount)
    {
        CurrentGem += amount;
    }

    public void GetScrollPaper(int amount)
    {
        currentScrollPaper += amount;
    }

    #endregion
    public bool CheckWalling()
    {
        return isWalling;
    }


}

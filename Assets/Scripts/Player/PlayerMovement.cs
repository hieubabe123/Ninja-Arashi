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
    [SerializeField] private GameObject camouFlagePrefab;
    [SerializeField] private GameObject rootPlayerPrefab;



    [Header("---------------Stats---------------")]
    public float currentMoveSpeed;
    public float currentJumpForce;
    public float currentCooldownShuriken;
    public float currentCooldownCamouflage;
    public float currentCooldownDashKill;
    private int currentDiguiseDuration;
    private int currentLifeCount;
    private int currentMoney;
    private int currentGem;
    private int currentScrollPaper;
    private float originalGravityScale;



    #region Stats
    //Seperate private stat in PlayerStats script and child stat in another script
    public int CurrentLifeCount
    {
        get { return currentLifeCount; }
        set
        {
            if (currentLifeCount != value)
            {
                currentLifeCount = value;
                if (UIForAll.instance != null)
                {
                    UIForAll.instance.currentLifeCountDisplay.text = CurrentLifeCount.ToString();
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
                if (UIForAll.instance != null)
                {
                    UIForAll.instance.currentMoneyDisplay.text = currentMoney.ToString();
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
                if (UIForAll.instance != null)
                {
                    UIForAll.instance.currentGemDisplay.text = currentGem.ToString();
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
                if (UIForAll.instance != null)
                {
                    UIForAll.instance.currentScrollPaperDisplay.text = currentScrollPaper.ToString();
                }
            }
        }
    }
    #endregion

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
    public float lastMoveDirX;


    [Header("---------------- Check How Player Dead --------------")]
    public bool isDeadByEnemies;
    public bool isDeadByPoison;
    public bool isDeadByElectric;
    public bool isDeadByFire;

    [Header("---------------- Effect --------------")]
    public ParticleSystem jumpGroundEffect;
    public ParticleSystem doubleJumpEffect;
    public ParticleSystem dashEffect;
    public ParticleSystem camouflageEffect;
    public ParticleSystem shieldEffect;
    public ParticleSystem healEffect;
    public ParticleSystem gemCollectEffect;
    public ParticleSystem coinCollectEffect;
    public GameObject playerEffectPos;

    private static int doubleJump = 2;
    private int currentJump;
    private float dashDistance = 15f;
    private float dashDuration = 0.2f;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        playerInputAction = GetComponent<PlayerInputAction>();
        enemy = FindObjectOfType<EnemyStats>();


        currentDashData = DataManager.instance.currentDashData;
        currentCamouflageData = DataManager.instance.currentCamouflageData;
        currentThrowShurikenData = DataManager.instance.currentThrowShurikenData;
        currenthealAndShieldData = DataManager.instance.currentHealAndShieldData;

        currentMoveSpeed = playerData.MoveSpeed;
        currentJumpForce = playerData.JumpForce;
        currentCooldownShuriken = DataManager.instance.currentThrowShurikenData.Cooldown;
        currentCooldownDashKill = DataManager.instance.currentDashData.Cooldown;
        currentCooldownCamouflage = DataManager.instance.currentCamouflageData.Cooldown;
        currentDiguiseDuration = DataManager.instance.currentCamouflageData.DiguiseDuration;
        CurrentLifeCount = DataManager.instance.currentHealAndShieldData.LifeCount;
        CurrentMoney = playerData.Money;
        CurrentGem = playerData.Gem;
        CurrentScrollPaper = playerData.ScrollPaper;
    }

    private void Start()
    {
        isDeadByEnemies = false;
        isDeadByFire = false;
        isDeadByElectric = false;
        isDeadByPoison = false;

        isImmortal = false;
        timeToImmortal = 0;
        lastMoveDirX = 1;
        originalGravityScale = rb.gravityScale;

        UIForAll.instance.currentLifeCountDisplay.text = CurrentLifeCount.ToString();
        UIForAll.instance.currentMoneyDisplay.text = CurrentMoney.ToString();
        UIForAll.instance.currentGemDisplay.text = CurrentGem.ToString();
        UIForAll.instance.currentScrollPaperDisplay.text = CurrentScrollPaper.ToString();
    }

    private void OnDisable()
    {
        isDeadByEnemies = false;
        isDeadByFire = false;
        isDeadByElectric = false;
        isDeadByPoison = false;
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
        rb.AddForce(currentJumpForce * Vector3.up, ForceMode2D.Impulse);
        doubleJumpEffect.Emit(8);
        currentJump--;
    }

    public void Fire()
    {
        currentCooldownShuriken = DataManager.instance.currentThrowShurikenData.Cooldown;
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


        currentCooldownDashKill = DataManager.instance.currentDashData.Cooldown;
        StartCoroutine(DashCoroutine());
    }

    public void Camouflage()
    {
        if (currentCooldownCamouflage > 0 && isCamouflage)
        {
            return;
        }
        camouFlagePrefab.SetActive(true);
        rootPlayerPrefab.SetActive(false);
        isCamouflage = true;

        currentCooldownCamouflage = DataManager.instance.currentCamouflageData.Cooldown;
        StartCoroutine(CamouflageCoroutine());


    }

    private IEnumerator CamouflageCoroutine()
    {
        yield return new WaitForSeconds(currentDiguiseDuration);
        isCamouflage = false;
        camouFlagePrefab.SetActive(false);
        rootPlayerPrefab.SetActive(true);
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
            jumpGroundEffect.Emit(1);
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
        if (healEffect != null)
        {
            Instantiate(healEffect, playerEffectPos.transform.position, Quaternion.identity);
        }
        CurrentLifeCount += amount;
    }

    public void TakeShield(float time)
    {
        if (shieldEffect != null)
        {
            Instantiate(shieldEffect, playerEffectPos.transform.position, Quaternion.identity);

        }
        isImmortal = true;
        timeToImmortal += time;
    }

    public void GetMoney(int amount)
    {
        if (coinCollectEffect != null)
        {
            Instantiate(coinCollectEffect, playerEffectPos.transform.position, Quaternion.identity);

        }
        CurrentMoney += amount;
    }

    public void GetGem(int amount)
    {
        if (gemCollectEffect != null)
        {
            Instantiate(gemCollectEffect, playerEffectPos.transform.position, Quaternion.identity);

        }
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

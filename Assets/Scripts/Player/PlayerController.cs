using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour, IDamageable
{
    [Header("Player State")]
    public float Health;
    public bool IsDead;
    // 速度
    public float Speed;
    // 跳跃力
    public float JumpForce;
    public Animator Anim;

    [Header("Ground Check")]
    // 检测点
    public Transform GroundCheck;
    // 检测范围
    public float CheckRadius;
    // 检测图层
    public LayerMask GroundLayer;


    [Header("States Check")]
    public bool IsGround;
    public bool Jumpping;
    public bool CanJump;

    [Header("Land Jump FX")]
    public GameObject JumpFX;
    public GameObject LandFX;

    [Header("Attack Setting")]
    public GameObject BombPrefab;
    // 下一次攻击时间
    public float NextAttack = 0;
    // 攻击时间间隔
    public float AttackRate = 2;

    private Rigidbody2D rb;

    private bool isHurt;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Anim = GetComponent<Animator>();
    }

    void Update()
    {
        Anim.SetBool("Dead", IsDead);
        if (IsDead)
        {
            return;
        }
        // 判断是否正在播放受伤动画
        isHurt = Anim.GetCurrentAnimatorStateInfo(1).IsName("Player_Hit");
        CheckInput();
    }

    public void FixedUpdate() 
    {
        if (IsDead)
        {
            rb.velocity = Vector2.zero;
            return;
        }
        PhysicsCheck();
        // 非受伤状态下才可以移动和跳跃
        if (!isHurt)
        {
            Movement();// input 会覆盖 Rigidbody 的速度，所以用 isHurt 来锁定就可以让 Player 被击飞
            Jump();
        }
    }

    /// <summary>
    /// 检测输入
    /// </summary>
    void CheckInput()
    {
        if (Input.GetButtonDown("Jump") && IsGround)
        {
            CanJump = true;
        }
        if (Input.GetKeyDown(KeyCode.J))
        {
            Attack();
        }
    }

    /// <summary>
    /// 移动
    /// </summary>
    void Movement()
    {
        // 获取键盘输入
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        // 左右移动
        rb.velocity = new Vector2(horizontalInput * Speed, rb.velocity.y);

        // player 左右翻转
        if(horizontalInput != 0)
        {
            // 通过控制 x 左右翻转
            transform.localScale = new Vector3(horizontalInput, 1, 1);
        }
    }

    /// <summary>
    /// 跳跃
    /// </summary>
    void Jump()
    {
        if (CanJump)
        {
            Jumpping = true;
            JumpFX.SetActive(true);
            JumpFX.transform.position = transform.position + new Vector3(0, -0.45f, 0);
            rb.velocity = new Vector2(rb.velocity.x, JumpForce);
            // 更改重力,增加下落效果
            rb.gravityScale = 4;
            CanJump = false;
        }
    }

    public void Attack()
    {
        if (Time.time > NextAttack)
        {
            Instantiate(BombPrefab, transform.position, BombPrefab.transform.rotation);
            NextAttack = Time.time + AttackRate;
        }         
    }

    /// <summary>
    /// 物理检测
    /// </summary>
    void PhysicsCheck()
    {
        IsGround = Physics2D.OverlapCircle(GroundCheck.position, CheckRadius, GroundLayer);
        // 落到地面后把重力重新改为1
        if (IsGround)
        {
            Jumpping = false;
            
            rb.gravityScale = 1;
        }
    }

    /// <summary>
    /// animation event 在 Player_Ground 动画第一针添加
    /// </summary>
    public void SetLandFX()
    {
        LandFX.SetActive(true);
        LandFX.transform.position = transform.position + new Vector3(0, -0.75f, 0);
    }

    /// <summary>
    /// unity 自带方法，绘制检测
    /// </summary>
    public void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(GroundCheck.position, CheckRadius);
    }

    /// <summary>
    /// 受伤害
    /// </summary>
    /// <param name="damage"></param>
    public void GetHit(float damage)
    {
        // 受伤短暂无敌
        if (!Anim.GetCurrentAnimatorStateInfo(1).IsName("Player_Hit"))
        {
            Health = Health - damage;
            if (Health < 1)
            {
                Health = 0;
                IsDead = true;
            }
            Anim.SetTrigger("Hit");
        }
        
    }
}

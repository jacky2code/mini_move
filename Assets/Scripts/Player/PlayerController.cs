using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // 速度
    public float Speed;
    // 跳跃力
    public float JumpForce;

    [Header("Ground Check")]
    // 检测点
    public Transform GroundCheck;
    // 检测范围
    public float CheckRadius;
    // 检测图层
    public LayerMask GroundLayer;


    [Header("States Check")]
    public bool IsGround;
    public bool CanJump;

    private Rigidbody2D rb;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        CheckInput();
    }

    public void FixedUpdate() 
    {
        PhysicsCheck();
        Movement();
        Jump();
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
            rb.velocity = new Vector2(rb.velocity.x, JumpForce);
            // 更改重力,增加下落效果
            rb.gravityScale = 4;
            CanJump = false;
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
            rb.gravityScale = 1;
        }
    }

    /// <summary>
    /// unity 自带方法，绘制检测
    /// </summary>
    public void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(GroundCheck.position, CheckRadius);
    }

}

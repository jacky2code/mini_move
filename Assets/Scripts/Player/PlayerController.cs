using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;

    // 速度
    public float Speed;
    // 跳跃力
    public float JumpForce;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void FixedUpdate()
    {
        Movement();
    }

    // 移动
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
}

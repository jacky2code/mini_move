using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private Animator anim;
    private Rigidbody2D rb;
    private PlayerController playerCrl;
    
    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        playerCrl = GetComponent<PlayerController>();
    }

    
    void Update()
    {
        // 获取 Player 移动速度，并取绝对值，传递给 speed 执行 run 动画 
        anim.SetFloat("speed", Mathf.Abs(rb.velocity.x));
        anim.SetFloat("velocityY", rb.velocity.y);
        anim.SetBool("jump", playerCrl.Jumpping);
        anim.SetBool("ground", playerCrl.IsGround);
        
    }
}

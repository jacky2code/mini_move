using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    private Animator animator;
    private Collider2D coll;
    private Rigidbody2D rig;

    public float StartTime;
    public float WaitTime;
    // 炸弹威力
    public float BombForce;

    [Header("Check")]
    public float Radius;
    public LayerMask TargetLayer;

    void Start()
    {
        animator = GetComponent<Animator>();
        coll = GetComponent<Collider2D>();
        rig = GetComponent<Rigidbody2D>();
        StartTime = Time.time;
    }

    void Update()
    {
        if (!animator.GetCurrentAnimatorStateInfo(0).IsName("Bomb_Off"))
        {
            if (Time.time > StartTime + WaitTime)
            {
                // 通过名字播放动画
                animator.Play("Bomb_Explotion");
            }
        }
        
        
    }

    /// <summary>
    /// unity 自带方法，绘制检测
    /// </summary>
    public void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, Radius);
    }

    /// <summary>
    /// 爆炸
    /// animation event 爆炸动画第一帧运行
    /// </summary>
    public void Explotion()
    {
        // 剔除炸弹碰撞体，避免炸弹自己飞上天。。。
        coll.enabled = false;
        // 获取爆炸影响到的所有物体
        Collider2D[] aroundObjects = Physics2D.OverlapCircleAll(transform.position, Radius, TargetLayer);
        // 防止炸弹因为没有碰撞体而掉落
        rig.gravityScale = 0;

        foreach (var item in aroundObjects)
        {
            // 获取物体在 角色 的方向位置
            Vector3 pos = transform.position - item.transform.position;
            // 给物体添加(反方向 + 向上)的爆炸威力和冲击力
            item.GetComponent<Rigidbody2D>().AddForce((-pos + Vector3.up) * BombForce, ForceMode2D.Impulse);

            // 点燃周围熄灭的炸弹
            if (item.CompareTag("Bomb") && item.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Bomb_Off"))
            {
                item.GetComponent<Bomb>().TurnOn();
            }

            if (item.CompareTag("Player"))
            {
                item.GetComponent<IDamageable>().GetHit(3);
            } 
        }
    }

    /// <summary>
    /// 爆炸万后销毁
    /// </summary>
    public void DestroyThis()
    {
        Destroy(gameObject);
    }

    /// <summary>
    /// 熄灭炸弹
    /// </summary>
    public void TurnOff()
    {
        animator.Play("Bomb_Off");
        // 更改炸弹图层
        gameObject.layer = LayerMask.NameToLayer("NPC");
    }

    public void TurnOn()
    {
        StartTime = Time.time;
        animator.Play("Bomb_On");
        gameObject.layer = LayerMask.NameToLayer("Bomb");
    }
}

using UnityEngine;

/// <summary>
/// 船长
/// 实现人物正向翻转：用 SpriteRenderer 中的 FlipX 实现
/// </summary>
public class Captain : Enemy, IDamageable
{
    private SpriteRenderer spriteRenderer;

    public override void Init()
    {
        base.Init();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public override void Update()
    {
        base.Update();
        // 巡逻模式下面部翻转
        if (AnimState == 0)
        {
            spriteRenderer.flipX = false;
        }
    }

    public void GetHit(float damage)
    {
        Health = Health - damage;
        if (Health < 1)
        {
            Health = 0;
            IsDead = true;
        }
        Anim.SetTrigger("Hit");
    }

    /// <summary>
    /// 重写 Captain 技能，遇见炸弹害怕反跑,奔跑时长为技能动画时长
    /// </summary>
    public override void SkillAction()
    {
        base.SkillAction();
        // 如果正处于 skill 动画状态
        if (Anim.GetCurrentAnimatorStateInfo(1).IsName("Captain_Skill"))
        {
            // 正面朝向翻转
            spriteRenderer.flipX = true;

            if (transform.position.x > TargetPoint.position.x)
            {
                
                transform.position = Vector2.MoveTowards(
                    transform.position,
                    transform.position + Vector3.right,
                    // 可调整技能动画时长或者速度倍数，来实现角色的反跑距离
                    Speed * 4 * Time.deltaTime);
            }
            else
            {
                transform.position = Vector2.MoveTowards(
                    transform.position,
                    transform.position + Vector3.left,
                    Speed * 4 * Time.deltaTime);
            }
        }
        else
        {
            spriteRenderer.flipX = false;
        }
    }
}

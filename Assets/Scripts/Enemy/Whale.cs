using UnityEngine;

/// <summary>
/// 巨鲸
/// 技能：吃炸弹
/// </summary>
public class Whale : Enemy, IDamageable
{
    public float Scale;
    private Vector3 originalScale;

    public override void Init()
    {
        base.Init();
        originalScale = transform.localScale;
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
    /// Skill Animation Event
    /// 吞炸弹
    /// </summary>
    public void SwallowBomb()
    {
        // 熄灭
        TargetPoint.GetComponent<Bomb>().TurnOff();
        TargetPoint.gameObject.SetActive(false);

        if (transform.localScale.y < (originalScale.y * 2.0))
        {
            // 鲸鱼变大
            transform.localScale *= Scale;
        }
    }
}
using UnityEngine;

/// <summary>
/// 巨鲸
/// 技能：吃炸弹
/// </summary>
public class Whale : Enemy, IDamageable
{
    // 鲸鱼吃炸弹后的变大系数，必须大于1；
    public float Scale = 1.2f;
    private Vector3 originalScale;

    public override void Init()
    {
        base.Init();
        //originalScale = transform.localScale;
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
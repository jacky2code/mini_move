using UnityEngine;

/// <summary>
/// 大块头
/// </summary>
public class BigGuy : Enemy, IDamageable
{
    public Transform PickPoint;
    public float ThrowPower;

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
    /// 拾取炸弹 Animation Event
    /// </summary>
    public void PickUpBomb()
    {
        if (TargetPoint.CompareTag("Bomb") && !HasBomb)
        {
            TargetPoint.gameObject.transform.position = PickPoint.position;
            // 把炸弹设置到point子集，可以跟随角色
            TargetPoint.SetParent(PickPoint);
            // 取消重力，避免炸弹掉下来
            TargetPoint.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;

            HasBomb = true;
        }
    }

    /// <summary>
    /// 仍炸弹 Animation Event
    /// </summary>
    public void ThrowBomb()
    {
        if (HasBomb)
        {
            TargetPoint.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
            TargetPoint.SetParent(transform.parent.parent);

            if (FindObjectOfType<PlayerController>().gameObject.transform.position.x
                - transform.position.x < 0)
            {
                TargetPoint.GetComponent<Rigidbody2D>().AddForce(new Vector2(-1, 1) * ThrowPower, ForceMode2D.Impulse);            }
            else
            {
                TargetPoint.GetComponent<Rigidbody2D>().AddForce(new Vector2(1, 1) * ThrowPower, ForceMode2D.Impulse);
            }
        }

        HasBomb = false;
    }
}

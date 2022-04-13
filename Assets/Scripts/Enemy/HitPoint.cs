using UnityEngine;

/// <summary>
/// 攻击受伤碰撞检测点
/// ！！！！！！！！！！！！！！！！！！！！！！注意：！！！！！！！！！！！！！！！！！！！！！
/// 给物体的 Collider 2D 勾选 Is Trigger
/// ！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！
/// </summary>
public class HitPoint : MonoBehaviour
{
    // 判断是否有针对炸弹可以使用
    public bool BombAvialble;
    // 炸弹位于BaldPirate的方向（用于Bald踢炸弹的方向）
    private int dir;
    /// <summary>
    /// 和另一个物体碰撞
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter2D(Collider2D other)
    {
        // 判断碰撞物体在本物体哪一侧
        if (transform.position.x > other.transform.position.x)
        {
            dir = -1;
        }
        else
        {
            dir = 1;
        }
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player get hurt !");
            // 普通攻击受伤害：1
            other.GetComponent<IDamageable>().GetHit(1);

            // 伤害并对对方有一个方向作用力，力度：10
            other.gameObject.GetComponent<Rigidbody2D>().AddForce(
                new Vector2(dir, 1) * 10, ForceMode2D.Impulse);
        }

        if (other.CompareTag("Bomb") && BombAvialble)
        {
            // 踢走炸弹，力度：10
            other.gameObject.GetComponent<Rigidbody2D>().AddForce(
                new Vector2(dir, 1) * 10, ForceMode2D.Impulse);
        }
    }
}

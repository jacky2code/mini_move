using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 攻击受伤碰撞检测点
/// </summary>
public class HitPoint : MonoBehaviour
{
    /// <summary>
    /// 和另一个物体碰撞
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player get hurt !");
            // 普通攻击受伤害：1
            other.GetComponent<IDamageable>().GetHit(1);
        }

        if (other.CompareTag("Bomb"))
        {

        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : EnemyBaseState
{
    public override void EnterState(Enemy enemy)
    {
        //Debug.Log("发现敌人了！！！");
        enemy.AnimState = 2;
        enemy.TargetPoint = enemy.AttackList[0];
    }

    public override void OnUpdate(Enemy enemy)
    {
        // 如果没有敌人了，切换到巡逻状态
        if (enemy.AttackList.Count == 0)
        {
            enemy.TransitionToState(enemy.PatrolState);
        }

        // 计算哪个敌人最近
        if (enemy.AttackList.Count > 1)
        {
            for (int i = 0; i < enemy.AttackList.Count; i++)
            {
                if (Mathf.Abs(enemy.transform.position.x - enemy.AttackList[i].position.x) <
                    Mathf.Abs(enemy.transform.position.x - enemy.TargetPoint.position.x))
                {
                    enemy.TargetPoint = enemy.AttackList[i];
                }
            }
        }

        // 判断敌人类型：Player / Bomb
        if (enemy.TargetPoint.CompareTag("Player"))
        {
            // 攻击
            enemy.AttackAction();
        }
        if (enemy.TargetPoint.CompareTag("Bomb"))
        {
            // 释放技能
            enemy.SkillAction();
        }

        // 追逐最近的目标
        enemy.MoveToTarget();
    }
}

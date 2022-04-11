using UnityEngine;
/// <summary>
/// 巡逻状态
/// </summary>
public class PatrolState : EnemyBaseState
{
    public override void EnterState(Enemy enemy)
    {
        enemy.AnimState = 0;

        enemy.SwitchPoint();
    }

    public override void OnUpdate(Enemy enemy)
    {
        // 获取当前动画状态，如果不是 Idle 动画
        if (!enemy.Anim.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
        {
            enemy.AnimState = 1;
            enemy.MoveToTarget();
        }

        if (Mathf.Abs(enemy.transform.position.x - enemy.TargetPoint.position.x) < 0.01f)
        {
            enemy.TransitionToState(enemy.PatrolState);
        }

        if (enemy.AttackList.Count > 0)
        {
            enemy.TransitionToState(enemy.AttackState);
        }
    }
}

using UnityEngine;
/// <summary>
/// 巡逻状态
/// </summary>
public class PatrolState : EnemyBaseState
{
    public override void EnterState(Enemy enemy)
    {
        enemy.SwitchPoint();
    }

    public override void OnUpdate(Enemy enemy)
    {
        if (Mathf.Abs(enemy.transform.position.x - enemy.TargetPoint.position.x) < 0.01f)
        {
            enemy.SwitchPoint();
        }

        enemy.MoveToTarget();
    }
}

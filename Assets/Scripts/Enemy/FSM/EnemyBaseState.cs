/// <summary>
/// 抽象类 EnemyBaseState
/// </summary>
public abstract class EnemyBaseState
{
    /// <summary>
    /// 进入状态
    /// </summary>
    public abstract void EnterState(Enemy enemy);

    public abstract void OnUpdate(Enemy enemy);

}

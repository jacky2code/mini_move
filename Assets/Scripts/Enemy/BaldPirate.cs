/// <summary>
/// 光头海盗
/// </summary>
public class BaldPirate : Enemy, IDamageable
{
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

    // Animation Event
    public void SetOffBomb()
    {
        TargetPoint.GetComponent<Bomb>()?.TurnOff();
    }
}
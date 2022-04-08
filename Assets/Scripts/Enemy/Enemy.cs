using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private EnemyBaseState currentState;


    public Animator Anim;
    public int AnimState;
    [Header("Movement")]
    public float Speed = 2;

    public Transform PointA, PointB;

    public Transform TargetPoint;

    public List<Transform> AttackList = new List<Transform>();

    public PatrolState PatrolState = new PatrolState();
    public AttackState AttackState = new AttackState();

    public virtual void Init()
    {
        Anim = GetComponent<Animator>();
    }

    private void Awake()
    {
        Init();
    }

    void Start()
    {
        TransitionToState(PatrolState);
    }

    
    void Update()
    {
        currentState.OnUpdate(this);
        Anim.SetInteger("State", AnimState);
    }

    /// <summary>
    /// 转换状态
    /// </summary>
    /// <param name="state"></param>
    public void TransitionToState(EnemyBaseState state)
    {
        currentState = state;
        currentState.EnterState(this);
    }

    public void MoveToTarget()
    {
        transform.position = Vector2.MoveTowards(transform.position, TargetPoint.position, Speed * Time.deltaTime);
        FlipDirection();
    }

    /// <summary>
    /// 攻击
    /// </summary>
    public virtual void AttackAction()
    {

    }

    /// <summary>
    /// 技能
    /// </summary>
    public virtual void SkillAction()
    {

    }

    /// <summary>
    /// 翻转
    /// </summary>
    public void FlipDirection()
    {
        if (transform.position.x < TargetPoint.position.x)
        {
            transform.rotation = Quaternion.Euler(0f, 0f, 0f);
        }
        else
        {
            transform.rotation = Quaternion.Euler(0f, 180f, 0f);
        }
    }

    /// <summary>
    /// 选择目标点，离角色远的点被选中
    /// </summary>
    public void SwitchPoint()
    {
        if (Mathf.Abs(PointA.position.x - transform.position.x) > Mathf.Abs(PointB.position.x - transform.position.x))
        {
            TargetPoint = PointA;
        }
        else
        {
            TargetPoint = PointB;
        }
    }

    /// <summary>
    /// 停留在碰撞检测区
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (!AttackList.Contains(collision.transform))
        {
            AttackList.Add(collision.transform);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (AttackList.Contains(collision.transform))
        {
            AttackList.Remove(collision.transform);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private EnemyBaseState currentState;
    private GameObject warningSign;

    public Animator Anim;
    public int AnimState = 0;

    [Header("Base State")]
    public float Health = 3;
    public bool IsDead = false;
    public bool HasBomb = false;

    [Header("Movement")]
    public float Speed = 2;
    public Transform PointA, PointB;
    public Transform TargetPoint;

    [Header("Attack Setting")]
    // 攻击间隔时间
    public float AttackRate = 2;
    private float nextAttack = 0;
    // 普通攻击距离和技能攻击距离
    public float AttackRange = 1;
    public float SkillRange = 1.5f;

    public List<Transform> AttackList = new List<Transform>();

    public PatrolState PatrolState = new PatrolState();
    public AttackState AttackState = new AttackState();

    public virtual void Init()
    {
        Anim = GetComponent<Animator>();
        // 获取第一个子物体
        warningSign = transform.GetChild(0).gameObject;
    }

    private void Awake()
    {
        Init();
    }

    void Start()
    {
        TransitionToState(PatrolState);
    }

    
    public virtual void Update()
    {
        Anim.SetBool("Dead", IsDead);
        if (IsDead)
        {
            return;
        }
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
    // 移动到目标
    public void MoveToTarget()
    {
        transform.position = Vector2.MoveTowards(transform.position, TargetPoint.position, Speed * Time.deltaTime);
        FlipDirection();
    }

    /// <summary>
    /// 普通攻击
    /// </summary>
    public virtual void AttackAction()
    {
        if (Vector2.Distance(transform.position, TargetPoint.position) < AttackRange)
        {
            if (Time.time > nextAttack)
            {
                // 播放攻击动画
                Anim.SetTrigger("Attack");
                Debug.Log("普通攻击！！！");
                nextAttack = Time.time + AttackRate;
            }
        }
    }

    /// <summary>
    /// 技能攻击
    /// </summary>
    public virtual void SkillAction()
    {
        if (Vector2.Distance(transform.position, TargetPoint.position) < SkillRange)
        {
            if (Time.time > nextAttack)
            {
                // 播放技能动画
                Anim.SetTrigger("Skill");
                Debug.Log("这是炸弹，释放技能！");
                nextAttack = Time.time + AttackRate;
            }
        }
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
        if (!AttackList.Contains(collision.transform) && !HasBomb)
        {
            AttackList.Add(collision.transform);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        AttackList.Remove(collision.transform);        
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (!IsDead)
        {
            StartCoroutine(OnSign());
        }
    }

    /// <summary>
    /// 用协程的方式打开和关闭，敌人角色遇险警告。
    /// </summary>
    /// <returns></returns>
    IEnumerator OnSign()
    {
        warningSign.SetActive(true);
        yield return new WaitForSeconds(
            // 获取第 0 个Layer的第 0 个动画片段时长
            warningSign.GetComponent<Animator>().GetCurrentAnimatorClipInfo(0)[0].clip.length
            );
        warningSign.SetActive(false);
    }
}

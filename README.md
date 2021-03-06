# MiNi Move

2D 平台跳跃类游戏，使用 unity 开发。

<img src="https://markdown-res.oss-cn-hangzhou.aliyuncs.com/mdImgs/2022/04/21/20220421110816.png" alt="20220421110816" align="center" style="width:500px" />

## Unit 1 Install & Assets

建议使用版本

- Unity 2019.4.X



### Section 1 Setup Assets 资源设置

- 原素材下载链接 itch.io：[点击跳转](https://pixelfrog-store.itch.io/pirate-bomb) 可以下载原始图片进行更改创新。

- 将具有多个元素的精灵纹理 Assets/Sprites Assets/8-Tile-Sets/Tile-Sets (64-64).png 文件做以下更改：
  - Sprite Mode: Multiple
    - 精灵模式：多元素
  - Pexels Per Unit: 64
    - 单元像素：64
  - Compression: None
    - 压缩方式：无
- 切片：
  - 通过 Sprite Editor 选择 Slice - Type - Grid by cell size - 64 * 64 - Slice 进行对大图按照 64 像素切片



## Unit 2 Build Level

### Section 1 Use Tilemap 使用 Tilemap 瓦片地图

导入瓦片图片信息并绘制场景

- 选择 Tile Palette 窗口，创建 Assets/Tilemap/Tile Palette/Map.prefab
- 注意 Background、Platform 要分开创建，避免修改干扰



### Section 2 Rule Tile 规则瓦片

- 使用 Package Manager 下载
  - （2019.4） https://learning-cdn-public-prd.unitychina.cn/20201210/5633636e-e845-40ab-8af0-2df322849241/2d-extras-master(2019.4).zip 扩展包
  - （2020.1）https://learning-cdn-public-prd.unitychina.cn/20201210/bf941621-493b-4588-8702-8c0e0e9c7e72/2d-extras-master(2020.1).zip



### Section 3 Tilemap Collider 瓦片碰撞器

瓦片地图碰撞器，了解 Tilemap Collider 2D 和 Composite Collider 的使用方法。

- 调整 Layer 显示顺序
  - 选择 Background 添加Sorting Layer，命名为background，并将其调整为最上面顺序（最上面显示在最后）
  - 给物体 Background 指定 Sorting Layer为 backgournd

- 给 platfrom 添加碰撞体
  - 为 platform 添加 Tilemap Collider 2D 地图碰撞体
  - 为 platform 添加 Composite Collider 2D 碰撞体。在附带的 Rigidbody 2D 中，由于重力为普通1，运行游戏时，platform会掉落，所以 Body Type 选择 Static。
  - 在 Tilemap Collider 2D 中选择 Used By Composite 合并成一个整体碰撞体。

  

### Section 4 Other Objects 其他物体

创建场景中的其他物体

设置 刚体 / 碰撞体 / Sorting Layer

- 给其他物品添加 Rigidbody 2D
- 给其他物品添加 Polygon Collider 2D
  - 并修改多边形碰撞体，适配每个物品的形状；左键鼠标增加碰撞点，并拖拽贴合物体。



### Section 5 Physics 2D & Prefab 2D物理及预制体

- 物体碰撞关系设置 
  - 添加 User Layer 8：Ground；
  - 添加 User Layer 9：Environment
  - 设置 Platform 的 Layer 为 Ground
  - 设置其他物体的 Layer 为 Environment
  - 在 Project Setting 中设置各个 Layer 的碰撞关系
- prefab 预制体的使用方法
  - 设置物体 Rigidbody 2D - Collision Detection - Continuous
    - 连续碰撞检查
  - 设置每个物体的质量：Rigidbody 2D - Mass - ...
  - 在 Assets 目录中新建 Prefabs 目录，拖拽场景中的其他物体到目录中，自动生成预制体。



## Unit 3 Create Player

### Section 1 Setup Player 创建玩家

设置 Player 的必要组件，调整各个组件的参数。设置碰撞体大小和碰撞关系。

- 调整 Player-Bomb 图片，按第一章节方式调整 32 像素

  <img src="https://markdown-res.oss-cn-hangzhou.aliyuncs.com/mdImgs/2022/04/21/20220421110250.png" style="width:500px"/>

- 拖拽一张图片到场景中 命名为 Player，添加刚体和碰撞体

- 调整 Mass 为 5；锁定 Z 轴，Constraints- Freeze Rotation - Z 打钩

- 调整 Player 的碰撞体大小适配脚部宽度大小

- 为 Player 添加 Layer 和 Sorting Layer 为 NPC

- 在 Project Setting 中设置 NPC 和 Environment 不碰撞



### Section 2 Movement 基本的移动

让 Player 可以移动，Input Manager 使用方法，如何反转人物。

- 给 Player 添加 PlayerController.cs 脚本，实现左右移动。

  ``` csharp
  using System.Collections;
  using System.Collections.Generic;
  using UnityEngine;
  
  public class PlayerController : MonoBehaviour
  {
      private Rigidbody2D rb;
      // 速度
      public float Speed;
      // 跳跃力
      public float JumpForce;
      // Start is called before the first frame update
      void Start()
      {
          rb = GetComponent<Rigidbody2D>();
      }
      // Update is called once per frame
      void Update()
      {
          
      }
      public void FixedUpdate()
      {
          Movement();
      }
      // 移动
      void Movement()
      {
          // 获取键盘输入
          float horizontalInput = Input.GetAxisRaw("Horizontal");
          // 左右移动
          rb.velocity = new Vector2(horizontalInput * Speed, rb.velocity.y);
  
          // player 左右翻转
          if(horizontalInput != 0)
          {
              // 通过控制 x 左右翻转
              transform.localScale = new Vector3(horizontalInput, 1, 1);
          }
      }
  }
  ```

  

### Section 3 Jump 跳跃

Update 和 FixedUpdate 的使用方法

地面物理检测 Physics2D 的函数方法

OnDrawGizmos 可视化的范围调整

- 为 PLayer 添加空物体，选择icon颜色，作为检测点，并调整Gizmos范围大小

  ``` csharp
  using System.Collections;
  using System.Collections.Generic;
  using UnityEngine;
  
  public class PlayerController : MonoBehaviour
  {
      // 速度
      public float Speed;
      // 跳跃力
      public float JumpForce;
  
      [Header("Ground Check")]
      // 检测点
      public Transform GroundCheck;
      // 检测范围
      public float CheckRadius;
      // 检测图层
      public LayerMask GroundLayer;
  
      [Header("States Check")]
      public bool IsGround;
      public bool CanJump;
  
      private Rigidbody2D rb;
  
      void Start()
      {
          rb = GetComponent<Rigidbody2D>();
      }
      void Update()
      {
          CheckInput();
      }
      public void FixedUpdate() 
      {
          PhysicsCheck();
          Movement();
          Jump();
      }
      /// <summary>
      /// 检测输入
      /// </summary>
      void CheckInput()
      {
          if (Input.GetButtonDown("Jump") && IsGround)
          {
              CanJump = true;
          }
      }
      /// <summary>
      /// 移动
      /// </summary>
      void Movement()
      {
          // 获取键盘输入
          float horizontalInput = Input.GetAxisRaw("Horizontal");
          // 左右移动
          rb.velocity = new Vector2(horizontalInput * Speed, rb.velocity.y);
  
          // player 左右翻转
          if(horizontalInput != 0)
          {
              // 通过控制 x 左右翻转
              transform.localScale = new Vector3(horizontalInput, 1, 1);
          }
      }
      /// <summary>
      /// 跳跃
      /// </summary>
      void Jump()
      {
          if (CanJump)
          {
              rb.velocity = new Vector2(rb.velocity.x, JumpForce);
              // 更改重力,增加下落效果
              rb.gravityScale = 4;
              CanJump = false;
          }
      }
      /// <summary>
      /// 物理检测
      /// </summary>
      void PhysicsCheck()
      {
          IsGround = Physics2D.OverlapCircle(GroundCheck.position, CheckRadius, GroundLayer);
          // 落到地面后把重力重新改为1
          if (IsGround)
          {
              rb.gravityScale = 1;
          }
      }
      /// <summary>
      /// unity 自带方法，绘制检测
      /// </summary>
      public void OnDrawGizmos()
      {
          Gizmos.DrawWireSphere(GroundCheck.position, CheckRadius);
      }
  }
  ```

  

## Unit 4 Animation

### Section 1 Setup Animation 设置动画

制作 2D Sprite 帧动画，制作多个需要的动画片段。

- 通过窗口 Animation 创建动画，拖拽相应动画图片，设置好 Samples 为20；



### Section 2 Animator States 动画控制器状态

动画状态机的切换方法，设置参数和条件，使用代码切换动画。

``` csharp
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private Animator anim;
    private Rigidbody2D rb;
    private PlayerController playerCrl;
    
    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        playerCrl = GetComponent<PlayerController>();
    }

    
    void Update()
    {
        // 获取 Player 移动速度，并取绝对值，传递给 speed 执行 run 动画 
        anim.SetFloat("speed", Mathf.Abs(rb.velocity.x));
        anim.SetFloat("velocityY", rb.velocity.y);
        anim.SetBool("jump", playerCrl.Jumpping);
        anim.SetBool("ground", playerCrl.IsGround);
        
    }
}
```



### Section 3 Jump VFX 跳跃的特效

制作 跳跃 / 落地 的帧动画特效，代码控制播放的时机，碰撞关系。

- 创建 LandFX 特效动画，在动画最后一帧，添加Event

- 创建 JumpFX 同上

  ``` csharp
  using UnityEngine;
  
  public class LandJumpFX : MonoBehaviour
  {
      public void Finish()
      {
          gameObject.SetActive(false);
      }
  }
  ```

  

## Unit 5 Create Bomb

### Section 1 Setup Bomb 创建炸弹

创建炸弹，添加必要组件，调整碰撞关系，创建三种状态动画。

- 创建炸弹对象，需要添加 Circle Collider 2D，并调整碰撞体大小。



### Section 2 Bomb Explosion 炸弹爆炸效果

创建 Animation Event 调用的函数方法实现爆炸效果。通过 **Collider2D[]** 获得范围内物体数组，实现炸开弹飞效果。

``` csharp
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    private Animator animator;
    private Collider2D coll;
    private Rigidbody2D rig;

    public float StartTime;
    public float WaitTime;
    // 炸弹威力
    public float BombForce;

    [Header("Check")]
    public float Radius;
    public LayerMask TargetLayer;

    void Start()
    {
        animator = GetComponent<Animator>();
        coll = GetComponent<Collider2D>();
        rig = GetComponent<Rigidbody2D>();
        StartTime = Time.time;
    }

    void Update()
    {
        if (Time.time > StartTime + WaitTime)
        {
            // 通过名字播放动画
            animator.Play("Bomb_Explotion");
        }
    }

    /// <summary>
    /// unity 自带方法，绘制检测
    /// </summary>
    public void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, Radius);
    }

    /// <summary>
    /// 爆炸
    /// animation event 爆炸动画第一帧运行
    /// </summary>
    public void Explotion()
    {
        // 剔除炸弹碰撞体，避免炸弹自己飞上天。。。
        coll.enabled = false;
        // 获取爆炸影响到的所有物体
        Collider2D[] aroundObjects = Physics2D.OverlapCircleAll(transform.position, Radius, TargetLayer);
        // 防止炸弹因为没有碰撞体而掉落
        rig.gravityScale = 0;

        foreach (var item in aroundObjects)
        {
            // 获取物体在 角色 的方向位置
            Vector3 pos = transform.position - item.transform.position;
            // 给物体添加(反方向 + 向上)的爆炸威力和冲击力
            item.GetComponent<Rigidbody2D>().AddForce((-pos + Vector3.up) * BombForce, ForceMode2D.Impulse);
        }
    }

    /// <summary>
    /// 爆炸万后销毁
    /// </summary>
    public void DestroyThis()
    {
        Destroy(gameObject);
    }
}
```



### Section 3 Player Attack 玩家攻击

让 Player 能够控制释放 Bomb 的 Prefab。设置CD时间。

``` csharp
[Header("Attack Setting")]
public GameObject BombPrefab;
// 下一次攻击时间
public float NextAttack = 0;
// 攻击时间间隔
public float AttackRate = 2;


public void Attack()
{
    if (Time.time > NextAttack)
    {
        Instantiate(BombPrefab, transform.position, BombPrefab.transform.rotation);
        NextAttack = Time.time + AttackRate;
    }         
}
```



## Unit 6 Create Enemy Scripts & AI

### Section 0 Enemies Assets Overview 敌人素材预览



### Section 1 Setup Basic Enemy 设置基本敌人

创建基本敌人 Cucumber 黄瓜怪。设置帧动画。添加 碰撞体 / 刚体。设置碰撞图层，调整碰撞关系。



### Section 2 More Elements 更多的组件

设置巡逻点。添加设置攻击检测范围。调整 Check Area 碰撞图层和关系。



### Section 3 Basic Methods 基本函数方法

创建 Enemy 代码中的基本函数方法。实现 巡逻 / 移动  / 反转 / 添加攻击列表的方法。学习 List<> 的使用。



### Section 4 Inheritance 继承

创建单独的 Cucumber 代码并继承 Enemy 基类。学习 virtual 函数方法如何在子类当中 override 重写。调整代码和参数。

``` csharp
using UnityEngine;

public class Enemy : MonoBehaviour
{
    ...
}
```

``` csharp
using UnityEngine;

public class Cucumber : Enemy
{
    
}
```



### Section 5 Finite States Machine 有限状态机

了解 FSM 状态机的概念 / 抽象类概念 / 抽象函数方法。用抽象类继承创建2个敌人AI状态：PatrolState / AttackState。

``` csharp
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
```

``` csharp
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

```



### Section 6 Animator States 动画状态机

在 Animator 窗口当中，使用多个 Layer 来控制管理多种动画状态。并且通过代码脚本来控制动画的切换。



### Section 7 Switch Attack Target 切换攻击目标

从供给列表中找到离自己最近的作为目标，并且判断目标的 Tag 是 Player 或者 Bomb 来执行不同的攻击方式。

``` csharp
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
```



### Section 8 Attack Action 攻击方式

创建攻击有关的变量，实现 AttackAction 和 SkillAction 两个函数方法。判断攻击距离与攻击间隔同时满足条件的情况，采取相应的行动。

``` csharp
/// <summary>
/// 普通攻击
/// </summary>
public virtual void AttackAction()
{
    if (Vector2.Distance(transform.position, TargetPoint.position) < AttackRange)
    {
        if (Time.time > nextAttack)
        {
            Debug.Log("进行攻击！");
            nextAttack = Time.time + AttackRate;
        }
    }
}

/// <summary>
/// 技能攻击
/// </summary>
public virtual void SkillAction()
{
    if (Vector2.Distance(transform.position, TargetPoint.position) < skillRange)
    {
        if (Time.time > nextAttack)
        {
            Debug.Log("这是炸弹，释放炸弹技能！");
            nextAttack = Time.time + AttackRate;
        }
    }
}
```



### Section 9 Hit Point 打击点

创建攻击点 HitPoint 录制动画实现攻击 Player 的方法。创建 HitPoint 代码脚本。

``` csharp
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitPoint : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player get hurt !");
        }

        if (other.CompareTag("Bomb"))
        {

        }
    }
}
```



### Section 10 Skill Action 技能攻击方式

通过实际创建 Cucumber 的吹灭炸弹特殊技能，了解当敌人的攻击目标是炸弹的时候，如何采取特殊技能效果。

``` csharp
/// <summary>
/// 熄灭炸弹
/// </summary>
public void TurnOff()
{
    animator.Play("Bomb_Off");
    // 更改炸弹图层
    gameObject.layer = LayerMask.NameToLayer("NPC");
}

public void TurnOn()
{
    StartTime = Time.time;
    animator.Play("Bomb_On");
    gameObject.layer = LayerMask.NameToLayer("Bomb");
}
```



### Section 11 Interface：IDamageable 创建接口

学习 Interface 接口的概念，如何通过创建 1 个接口来实现访问所有继承这个接口的代码脚本。轻松实现炸弹爆炸让周围的人物都有受到伤害的效果。

``` csharp
public interface IDamageable
{
    /// <summary>
    /// 受伤害
    /// </summary>
    /// <param name="damage"></param>
    void GetHit(float damage);
}
```

``` csharp
public class Cucumber : Enemy, IDamageable
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
```



### Section 12 Player Get Hit 玩家获得伤害

通过 IDamageable 接口实现玩家受伤，并且受伤动画播放期间短暂无敌。创建敌人警示标示，学习 协程 的使用方法

- 协程函数

  ``` csharp
  public void OnTriggerEnter2D(Collider2D collision)
  {
      StartCoroutine(OnSign());
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
  ```

  

## Unit 7 Skill Action for Each Enemies

### Section 0 Unit Overview 本单元概览

### Section 1 Bald Pirate 光头海盗

光头海盗的特殊技能：踢炸弹。HitPoint 代码添加弹开效果。

``` csharp
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
```



### Section 2 Big Guy 大块头

实现大块头拾取炸弹并扔向玩家的方法。添加 Animation Event 设置 PickUp Point。

``` csharp
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
```



### Section 3 Captain 船长

粉色船长看见炸弹反跑，利用 Sprite Renderer 组件的 FilpX 参数实现翻转。

``` csharp
using UnityEngine;

/// <summary>
/// 船长
/// 实现人物正向翻转：用 SpriteRenderer 中的 FlipX 实现
/// </summary>
public class Captain : Enemy, IDamageable
{
    private SpriteRenderer spriteRenderer;

    public override void Init()
    {
        base.Init();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public override void Update()
    {
        base.Update();
        // 巡逻模式下面部翻转
        if (AnimState == 0)
        {
            spriteRenderer.flipX = false;
        }
    }

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
    /// 重写 Captain 技能，遇见炸弹害怕反跑,奔跑时长为技能动画时长
    /// </summary>
    public override void SkillAction()
    {
        base.SkillAction();
        // 如果正处于 skill 动画状态
        if (Anim.GetCurrentAnimatorStateInfo(1).IsName("Captain_Skill"))
        {
            // 正面朝向翻转
            spriteRenderer.flipX = true;

            if (transform.position.x > TargetPoint.position.x)
            {
                
                transform.position = Vector2.MoveTowards(
                    transform.position,
                    transform.position + Vector3.right,
                    // 可调整技能动画时长或者速度倍数，来实现角色的反跑距离
                    Speed * 4 * Time.deltaTime);
            }
            else
            {
                transform.position = Vector2.MoveTowards(
                    transform.position,
                    transform.position + Vector3.left,
                    Speed * 4 * Time.deltaTime);
            }
        }
        else
        {
            spriteRenderer.flipX = false;
        }
    }
}
```



### Section 4 Whale 鲸鱼

添加 Animation Event 实现吞灭炸弹，趣味添加吞下炸弹体积变大。

``` csharp
using UnityEngine;

/// <summary>
/// 巨鲸
/// 技能：吃炸弹
/// </summary>
public class Whale : Enemy, IDamageable
{
    public float Scale;
    private Vector3 originalScale;

    public override void Init()
    {
        base.Init();
        originalScale = transform.localScale;
    }

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
    /// Skill Animation Event
    /// 吞炸弹
    /// </summary>
    public void SwallowBomb()
    {
        // 熄灭
        TargetPoint.GetComponent<Bomb>().TurnOff();
        TargetPoint.gameObject.SetActive(false);

        if (transform.localScale.y < (originalScale.y * 2.0))
        {
            // 鲸鱼变大
            transform.localScale *= Scale;
        }
    }
}
```



## Unit 8 User Interface

### Section 1 Create Canvas 创建画布

UGUI 使用方法，添加 Player Health Bar 左上角对齐，并设置 Canvas 的自动缩放。

<img src="https://markdown-res.oss-cn-hangzhou.aliyuncs.com/mdImgs/2022/04/22/20220422093756.png" alt="image-20220422093500768" align="center" style="width:500px"/>



### Section 2 Update Health 更新生命值

创建 UIManager 单例模式。获取玩家血量，并实时更新 UI 血量显示。

单例模式：

``` csharp
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    public void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
```



### Section 3 Pause Menu 暂停菜单

创建暂停菜单，Button 组件添加方法，实现 暂停 / 恢复游戏

<img src="https://markdown-res.oss-cn-hangzhou.aliyuncs.com/mdImgs/2022/04/22/20220422131523.png" alt="image-20220422131438708" align="center" style="width:500px" />



### Section 4 Boss Health Bar  Boss 血条

手动创建简单 Slider 组件， Boss 血条实时更新

Enemy.cs

``` csharp
void Start()
{
    TransitionToState(PatrolState);
    if (IsBoss)
    {
        UIManager.Instance.SetMaxValueHealthBarBoss(Health);
    }
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

    if (IsBoss)
    {
        UIManager.Instance.updateValueHealthBarBoss(Health);
    }
}
```

UIManager.cs

``` csharp
public Slider HealthBarBoss;

/// <summary>
/// 设置 Boss 血条最大值
/// </summary>
/// <param name="health"></param>
public void SetMaxValueHealthBarBoss(float health)
{
    HealthBarBoss.maxValue = health;
}

/// <summary>
/// 更新 Boss 血条
/// </summary>
/// <param name="health"></param>
public void updateValueHealthBarBoss(float health)
{
    HealthBarBoss.value = health;
}
```



## Unit 9 Game Manager

### Section 1 Game Over 游戏结束画面

创建 GameManager.cs，生成单例，连接 Player 的状态，全局控制游戏的结束，添加 Gameover Panel 实现死亡弹出菜单重新开始游戏。

<img src="https://markdown-res.oss-cn-hangzhou.aliyuncs.com/mdImgs/2022/04/24/20220424111325.png" align="center" style="width:500px" />

``` csharp
using UnityEngine;

public class Gamemanager : MonoBehaviour
{
    public static Gamemanager Instance;

    public bool IsGameOver;

    private PlayerController playerCtrl;

    public void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        playerCtrl = FindObjectOfType<PlayerController>();
    }

    public void Update()
    {
        IsGameOver = playerCtrl.IsDead;
        UIManager.Instance.IsShowGameOverPanel(IsGameOver);
    }
}
```



### Section 2 Way to the next room 通往下一关

- 使用 Platform Effector 组件创建单项平台。
- 创建 Door 入口和出口代码，实现消灭所有敌人后开门的动画，Player 进门触发范围跳转下一场景。
- 观察者模式的使用。

```  csharp
/// <summary>
/// 游戏结束后重新开始
/// </summary>
public void RestartGame()
{
    SceneManager.LoadScene(SceneManager.GetActiveScene().name);
}

public void NextLevel()
{
    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
}
```



### Section 3 Save Data 保存数据

使用 PlayerPrefs，保存 Player 的血量延续到下一关，在过关时保存数据。

``` csharp
private string playerHealthKey = "PlayerHealth";
/// <summary>
/// 读取保存的血量
/// </summary>
/// <returns></returns>
public float LoadPlayerHealth()
{
    if (!PlayerPrefs.HasKey(playerHealthKey))
    {
        PlayerPrefs.SetFloat(playerHealthKey, 3.0f);
    }
    float currentHealth = PlayerPrefs.GetFloat(playerHealthKey);
    
    return currentHealth;
}

public void SaveData()
{
    PlayerPrefs.SetFloat(playerHealthKey, playerCtrl.Health);
    PlayerPrefs.Save();
}
```



### Section 4 Fix bugs 调整bugs

- 调整一些代码顺序，更改观察者模式的代码设计方法。

- 创建 Main Menu

  <img src="https://markdown-res.oss-cn-hangzhou.aliyuncs.com/mdImgs/2022/04/25/20220425115116.png" align="center" style="width:500px" />

  

### Section 5 Load Game 加载游戏进度

实现保存游戏场景，让玩家可以延续之前的游戏进度。Main Menu 各种 Button 的函数方法实现。



## Unit 10 Build for Mobile & Monetization

### section 1 Mobile Platform Control 移动平台控制方法

下载使用 Asset Store 中 Joystick 插件， 创建屏幕横向控制器，创建按钮实现跳跃和攻击时放炸弹。重新调整代码配合 Button 的 On Click Event。

- Unity2020开始取消了内置的Asset Store 请访问网页版商城并将需要的插件添加到你的账号中
  - Joysick https://assetstore.unity.com/packages/tools/input-management/joystick-pack-107631
- Unity 打开 Package Manager 选择 My Assets 就能看到自己购买的插件下载安装

``` csharp
/// <summary>
/// 移动
/// </summary>
void Movement()
{
    // 获取键盘输入
    //float horizontalInput = Input.GetAxisRaw("Horizontal");
    // 操纵杆
    float horizontalInput = joystick.Horizontal;
    // 左右移动
    rb.velocity = new Vector2(horizontalInput * Speed, rb.velocity.y);

    // player 左右翻转
    //if(horizontalInput != 0)
    //{
    //    // 通过控制 x 左右翻转
    //    transform.localScale = new Vector3(horizontalInput, 1, 1);
    //}

    if (horizontalInput > 0)
    {
        transform.eulerAngles = new Vector3(0, 0, 0);
    }
    if (horizontalInput < 0)
    {
        transform.eulerAngles = new Vector3(0, 180, 0);
    }
}
```

<img src="https://markdown-res.oss-cn-hangzhou.aliyuncs.com/mdImgs/2022/04/26/20220426105843.png" style="width:500px" />



### Section 2 Build and Run 生成并运行

生成 Android 和 iOS 平台，真机测试。








# MiMi Move

2D 平台跳跃类游戏，使用 unity 开发。



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

  

  




















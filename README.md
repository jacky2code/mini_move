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






























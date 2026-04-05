# Youth Meadow General Store

`Youth Meadow General Store` 是该项目的工程名与仓库名，软件对外显示名称为 `青春田野小卖部`。

这是一个基于 `C# WinForms + .NET Framework 4.8` 的单机经营小游戏项目，来源于原始 Python 版本的 `game.py` 迁移。项目目标不是做现代 UI 重构，而是在 Windows 桌面环境中尽量保留原作的玩法结构、事件流程、节日触发、弹窗交互和音频体验。

## 项目状态

- 当前仓库托管的是 WinForms 迁移版本。
- 旧版 Python 存档不兼容，当前版本使用新的 `save.dat` 格式。
- 输出目标是单个 `.exe` 主程序，依赖 DLL 会在构建阶段嵌入可执行文件内部。
- 音效与背景音乐资源保留为项目目录内的 `sounds/` 外部资源目录。

## 命名说明

- 仓库名：`Youth Meadow General Store`
- 解决方案名：`Youth Meadow General Store.sln`
- 项目名：`Youth Meadow General Store.csproj`
- 程序显示名：`青春田野小卖部`
- 主窗口标题：`青春田野小卖部`

## 核心特性

- 基于 WinForms 的桌面单窗口经营玩法
- 采购、仓库、货架、营业流程完整可运行
- 节日事件与随机事件拆分为多文件维护
- 支持启动音、背景音乐、结果音、选择音、营业音等音频播放
- 背景音乐与音效走统一混音输出，避免播放音效时背景被抢设备卡顿
- 构建输出自动清理 `.dll`、`.pdb`、`.config` 冗余文件
- 解决方案与项目已经分离命名，便于使用 Visual Studio 或命令行维护

## 技术栈

- `C#`
- `Windows Forms`
- `.NET Framework 4.8`
- `NAudio 2.3.0`
- `NAudio.Vorbis 1.5.0`
- `Fody 6.9.3`
- `Costura.Fody 6.0.0`

## 目录结构

```text
Youth Meadow General Store/
├─ Dialogs/                          # 采购、选择、输入等弹窗
├─ Models/                           # Product / ShelfSlot / GameSaveData
├─ Services/                         # 声音、存档、加密服务
├─ Utilities/                        # 自定义 UI 控件
├─ sounds/                           # 音频资源目录，运行时必须存在
├─ MainForm.cs                       # 主窗体核心逻辑
├─ MainForm.Designer.cs              # 主窗体布局
├─ MainForm.Gameplay.cs              # 日常经营与结局等逻辑
├─ MainForm.Festivals.cs             # 节日事件逻辑
├─ MainForm.RandomEvents.cs          # 随机事件入口
├─ MainForm.RandomEvents.Part1.cs    # 随机事件分段实现
├─ MainForm.RandomEvents.Part2.cs
├─ MainForm.RandomEvents.Part3.cs
├─ MainForm.RandomEvents.SpecialDates.cs
├─ Program.cs                        # 程序入口
├─ FodyWeavers.xml                   # Costura 配置
├─ Youth Meadow General Store.csproj # 项目文件
└─ Youth Meadow General Store.sln    # 解决方案文件
```

## 运行环境要求

### 开发环境

- Windows
- Visual Studio 2022，或
- 安装了 .NET SDK 且具备 `.NET Framework 4.8` 构建支持的开发环境

### 运行环境

- Windows
- `.NET Framework 4.8 Runtime`
- 与 `exe` 同级的 `sounds/` 目录

## 构建方式

### 方式一：使用命令行

在项目根目录执行：

```powershell
dotnet build "D:/python/game/Youth Meadow General Store/Youth Meadow General Store.sln"
```

### 方式二：使用 Visual Studio

1. 打开 [Youth Meadow General Store.sln](./Youth%20Meadow%20General%20Store.sln)
2. 选择 `Debug|Any CPU` 或你自己的构建配置
3. 直接生成解决方案

## 运行方式

构建成功后，主程序位于：

```text
bin/Debug/net48/Youth Meadow General Store.exe
```

运行时请确保 `sounds/` 目录在可执行文件同级输出目录中存在。

## 输出产物说明

本项目对输出目录做了额外约束，目标是尽量精简部署内容。

构建完成后，输出目录原则上只保留：

- `Youth Meadow General Store.exe`
- `sounds/`
- 运行后生成的 `save.dat`

以下内容会在构建后自动清理：

- `*.dll`
- `*.pdb`
- `*.config`

依赖库通过 `Costura.Fody` 嵌入进主程序内部，不再作为外部 DLL 分发。

## 音频系统说明

### 资源位置

所有音频资源位于项目目录下的 [sounds](./sounds)。

### 当前音频策略

- 启动时先同步播放 `开业.wav`
- 主窗口显示后再开始背景音乐
- 背景音乐与音效使用统一混音输出
- 上架、购买、营业、结果、选择等音效按功能分类播放

### 为什么音频资源没有嵌进 exe

当前实现只把代码依赖 DLL 嵌入到 `.exe` 中，`sounds/` 目录仍然作为外部资源保留。这样做的原因是：

- 更容易替换或调整音频资源
- 避免把大量媒体文件塞入主程序导致体积和维护成本上升
- 与现有资源加载逻辑保持简单直接

## 存档说明

### 当前存档文件

- 文件名：`save.dat`
- 位置：程序运行目录下

### 存档内容

存档主要保存：

- 当前资金
- 当前日期
- 仓库商品
- 货架商品与解锁状态
- 背景音乐状态
- 若干事件进度标记

### 存档格式

内部先序列化为 JSON，再经过简单异或加密后写入磁盘。

### 兼容性说明

- 不兼容旧版 Python 存档
- 这是有意设计，不做历史存档兼容迁移

## 基本玩法说明

### 主界面按钮

- `进货列表`
- `仓库列表`
- `货架列表`
- `开始营业`

### 快捷键

- `F1`：打开进货列表
- `F2`：打开仓库列表
- `F3`：打开货架列表
- `F4`：开始营业
- `F8`：降低音量
- `F9`：提高音量

### 信息列表

主界面下方使用真正的列表控件展示信息文本，支持：

- 选中项浏览
- 自动换行
- 根据内容高度自动调整每一项显示高度

### 采购流程

1. 打开采购列表
2. 选择商品
3. 输入数量
4. 校验资金
5. 写入仓库并自动存档

### 货架流程

支持：

- 单个货架上架
- 下架
- 扩充货架
- 全部上架

### 营业流程

1. 用户确认开始营业
2. 播放营业音效
3. 必要时切换背景音乐
4. 延迟触发随机事件
5. 推进日期与经营状态

## 代码结构说明

### MainForm

[MainForm.cs](./MainForm.cs) 负责主窗体的基础交互与核心流程协调，不直接把所有逻辑塞进一个文件，而是按职责拆分。

### 事件拆分

- [MainForm.Gameplay.cs](./MainForm.Gameplay.cs)：经营推进、营业结果、结局处理
- [MainForm.Festivals.cs](./MainForm.Festivals.cs)：节日事件
- [MainForm.RandomEvents.cs](./MainForm.RandomEvents.cs)：随机事件主调度
- [MainForm.RandomEvents.Part1.cs](./MainForm.RandomEvents.Part1.cs)
- [MainForm.RandomEvents.Part2.cs](./MainForm.RandomEvents.Part2.cs)
- [MainForm.RandomEvents.Part3.cs](./MainForm.RandomEvents.Part3.cs)
- [MainForm.RandomEvents.SpecialDates.cs](./MainForm.RandomEvents.SpecialDates.cs)

### 服务层

- [SoundManager.cs](./Services/SoundManager.cs)：音频播放与混音
- [GamePersistenceService.cs](./Services/GamePersistenceService.cs)：存档读写
- [SaveEncryptionService.cs](./Services/SaveEncryptionService.cs)：存档加解密

### 数据模型

- [Product.cs](./Models/Product.cs)
- [ShelfSlot.cs](./Models/ShelfSlot.cs)
- [GameSaveData.cs](./Models/GameSaveData.cs)

## 迁移说明

本仓库不是从零设计的新游戏，而是从 Python 版本迁移而来。迁移过程中主要处理了以下问题：

- Python 脚本逻辑拆分到 WinForms 多文件结构
- 启动音、背景音乐、音效时序重新梳理
- 节日与随机事件按类别拆分
- 主界面信息区域重做为 WinForms 列表展示
- 构建输出收敛为 `exe + sounds`
- 新增解决方案文件，便于 IDE 管理

## 已知约束

- 仅支持 Windows
- 依赖 `sounds/` 外部目录
- 当前 README 不附带截图
- 存档加密只是轻量混淆，不是安全防护方案

## 开发建议

如果后续继续维护，建议优先从以下方向继续整理：

- 把随机事件进一步抽象成数据驱动结构
- 给节日与特殊日期逻辑补统一映射表
- 为关键经营流程补最小化自动化测试
- 把资源路径与配置抽离成集中配置项

## 仓库使用建议

如果你是第一次拉取本项目，建议顺序如下：

1. 打开解决方案
2. 先确认 `sounds/` 资源完整
3. 直接构建
4. 运行主程序验证启动音、主界面、列表和经营流程

## 免责声明

本项目主要用于原始脚本的桌面迁移与工程化整理，当前重点是功能可运行、结构可维护和部署更简单，不代表已经完成最终商业化打磨。

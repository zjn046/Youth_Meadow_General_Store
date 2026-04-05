# Youth Meadow General Store

`Youth Meadow General Store` 是该项目的工程名与仓库名，软件对外显示名称为 `青春田野小卖部`。

`青春田野小卖部` 是一个简单的经营养成游戏。

小时候更喜欢玩打打杀杀、节奏很快、反馈很强的游戏，现在反而越来越喜欢日常一些、慢一点、能让人放松下来的经营养成游戏。比起一直战斗、一直对抗，慢慢进货、整理货架、看着小店一点点运转起来，会更治愈一些。这个项目最开始也是在这样的想法下做出来的。

这是一个基于 `C# WinForms + .NET Framework 4.8` 的单机经营小游戏项目，当前仓库存放的是 C# 迁移版本。它来源于更早的 Python 版本 `game.py`，迁移目标不是重新设计成另一套现代 UI，而是在 Windows 桌面环境中尽量保留原作的玩法结构、节日触发、随机事件、弹窗交互和音频体验。

## 项目简介

这是一个围绕乡村小卖部展开的轻量经营游戏。玩家从有限启动资金开始，通过进货、上架、营业、应对节日和随机事件，逐步经营自己的小店。整体玩法并不复杂，但正因为简单，才更强调那种“日常经营”的节奏感。

如果你喜欢的是那种能慢慢看着数值增长、库存变化、货架填满、事件推进的小体量经营游戏，那这个项目应该会有一点意思。

## 开发背景

这个项目的灵感，来自以前玩过的一款叫做《我有一家小卖部》的游戏。当时我们觉得这个题材很有意思，同学就想自己再做一个新的版本。于是大二那会儿，我们真的用 Python 开始动手做，前前后后折腾了一个学期，才把最初能跑起来的版本做出来。

它不是那种一开始就有完整商业计划、完整策划文档、完整工程体系的项目，更像是几个学生在“想做一个属于自己的小卖部游戏”这个念头驱动下，一点点做出来的东西。也正因为这样，这个项目对我们来说不只是代码，更像是学生时期认真做完的一件事。

## 为什么从 Python 迁移到 C#

最早版本使用的是 Python。Python 开发快，试错成本也低，所以在项目最初阶段非常合适。但后面实际整理和打包的时候，一个很现实的问题摆在面前：Python 版本的发布体积太大了。

项目本身其实不复杂，但 Python 运行时和打包依赖带来的体积并不小。后来仔细想了想，这种桌面小体量游戏，其实更适合迁移成 `C# WinForms + .NET Framework 4.8` 这种更直接的桌面应用形态。迁移完成之后，部署结构更清晰，输出也更紧凑，体积一下子小了很多，这也是这次迁移的核心原因之一。

这次迁移并不是为了“技术升级”而升级，而是一个很务实的决定：

- Python 版本先把玩法原型做出来
- C# 版本把桌面部署、体积和结构整理得更适合长期维护
- 开源出来后，让这个项目不只停留在我们自己电脑里

## 为什么开源

我们希望这个项目在迁移成 C# 之后，不只是变得更容易运行，也能变得更容易继续完善。

所以最后决定把它开源出来。我们真心希望感兴趣的人可以一起参与进来，让这个游戏变得更完整、更好玩。

如果你愿意，可以通过这些方式参与：

- 提交 `Issue`，指出 bug、体验问题或玩法建议
- 提交 `PR`，直接改进代码、界面、交互或内容
- 帮忙补充文案、事件、节日内容和经营细节

如果未来这个游戏能比现在更有趣，那一定不是因为某一次单独的迁移，而是因为后来不断有人愿意继续把它往前推一点。

## 项目状态

- 当前仓库托管的是 WinForms 迁移版本。
- 旧版 Python 存档不兼容，当前版本使用新的 `save.dat` 格式。
- 输出目标是标准桌面程序结构，主程序、依赖 DLL 和音频资源分别放在输出目录中。
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
- 构建输出自动清理 `.pdb`、`.config` 冗余文件
- 解决方案与项目已经分离命名，便于使用 Visual Studio 或命令行维护

## 技术栈

- `C#`
- `Windows Forms`
- `.NET Framework 4.8`
- `NAudio 2.0.0`
- `NAudio.Core 2.0.0`
- `NAudio.Vorbis 1.5.0`

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

先进入项目目录，再执行：

```powershell
cd "Youth Meadow General Store"
dotnet build "Youth Meadow General Store.sln"
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
- 运行所需的依赖 `*.dll`
- `sounds/`
- 运行后生成的 `save.dat`

以下内容会在构建后自动清理：

- `*.pdb`
- `*.config`

## 音频系统说明

### 资源位置

所有音频资源位于项目目录下的 [sounds](./sounds)。

### 当前音频策略

- 启动时先同步播放 `开业.wav`
- 主窗口显示后再开始背景音乐
- 背景音乐与音效使用统一混音输出
- 上架、购买、营业、结果、选择等音效按功能分类播放

### 为什么音频资源仍然保留为外部目录

当前实现保持 `sounds/` 目录为外部资源。这样做的原因是：

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

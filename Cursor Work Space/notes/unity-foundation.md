# Unity 基础控制层 — 过程记录

日期：2026-09-30  
Unity：2022.3.62f3c1（唯一编辑器；MCP 也只连此版本）  
输入：旧 Input Manager（`activeInputHandler: 0`）

## 约定

- **过程文件**（说明、草稿、临时产物）→ `Cursor Work Space/`
- **可运行游戏代码与资源** → `Assets/_Game/`
- Unity 不会打包根目录下的 `Cursor Work Space`
- **场景 / 预制体操作规范** → `Cursor Work Space/control/ai-unity-workflow.md`
- **上下文记忆** → `Cursor Work Space/memory/`（含 `CURRENT.md`）
- **代码风格** → 少写保护性兜底，尽早暴露错误（见 `.cursor/rules/fail-fast-code.mdc`）

## 本次交付

| 路径 | 职责 |
|---|---|
| `Assets/_Game/Scripts/Core/Singleton.cs` | 场景内单例基类 |
| `Assets/_Game/Scripts/Core/GameManager.cs` | 运行状态、暂停、时间缩放 |
| `Assets/_Game/Scripts/Player/PlayerController.cs` | CharacterController 第三人称移动 |
| `Assets/_Game/Scripts/Camera/CameraFollow.cs` | 第三人称相机跟随与环绕 |

## 场景挂接（手动）

1. 空物体 `GameSystems` → 挂 `GameManager`
2. 玩家物体挂 `CharacterController` + `PlayerController`，Tag 设为 `Player`
3. 主相机挂 `CameraFollow`，`Target` 指向玩家

## 默认按键

- 移动：WASD / 方向键
- 视角：鼠标
- 跳跃：Space
- 冲刺：Left Shift
- 暂停：Esc

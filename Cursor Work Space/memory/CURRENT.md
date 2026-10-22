# 当前快照

更新：2026-10-23

## 已定约定

- 编辑器：Unity **6000.6.5f1**（`F:\Unity Hub\Unity\6000.6.5f1`）；勿用 2022.3 / 团结引擎开本工程
- MCP：HTTP `http://127.0.0.1:8765/mcp`（8080 被 Steam 占用）
- 场景/预制体：优先 Unity MCP；失败给手动步骤；未要求不写菜单工具
- 过程文件 → `Cursor Work Space/`；运行时 → `Assets/_Game/`
- 运行时脚本少写保护性代码，尽早暴露配置/逻辑错误
- 输入：仍为旧 Input Manager（未迁新 Input System）

## 当前状态

- MCP 包：`Packages/com.coplaydev.unity-mcp`
- 基础脚本：`Singleton` / `GameManager` / `PlayerController` / `CameraFollow`
- 编辑器连桥：`Assets/_Game/Editor/McpLocalHttpBootstrap.cs`
- Cursor 规则：`.cursor/rules/`（MCP 流程、项目约束、fail-fast、记忆）
- **玩法方向（概念）**：类《星际拓荒》有限箱庭；详见概念记忆
- **渲染底座**：**URP 17.6.0**（Pipeline / Renderer / Global Volume / Bootstrap）
- **已完成**：迁移至 Unity 6000.6.5f1（见 `2026-10-23-migrate-unity-6000.6.5f1.md`）

## 关键控制 / 记忆

- `Cursor Work Space/control/ai-unity-workflow.md`
- `Cursor Work Space/control/editor-version.md`
- `.cursor/rules/unity-mcp-workflow.mdc`
- `Cursor Work Space/memory/2026-09-30-concept-outer-wilds-direction.md`
- `Cursor Work Space/memory/2026-09-30-render-pipeline-from-concept.md`
- `Cursor Work Space/memory/2026-09-30-urp-base-setup.md`
- `Cursor Work Space/memory/2026-10-23-migrate-unity-6000.6.5f1.md`
- 概念原文：`Cursor Work Space/Doce/《云端王国》概念文档.pdf`

## 未决 / 注意

- Unity 启动若改写 `mcp.json` 为 8080，以 Bootstrap 写回的 8765 为准
- `Cursor Work Space/logs/`、`vendor/` 不进仓库
- **`.gitignore` 只能用 `/[Bb]uild/`（根目录）**；勿用全局 `[Bb]uild/`（见 `2026-09-30-fix-mcp-build-gitignore.md`）
- 概念未决：肉鸽？元素矩阵交互；知识锁与锁合粒子/深潜分层尚未展开
- 渲染下一阶段：压力雾 / 体积云 / 锁合材质

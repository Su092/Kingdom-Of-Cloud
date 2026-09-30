# 当前快照

更新：2026-09-30

## 已定约定

- 编辑器：Unity **2022.3.62f3c1**（`D:\Unity\2022.3.62f3c1`）；不用团结引擎开发本工程
- MCP：HTTP `http://127.0.0.1:8765/mcp`（8080 被 Steam 占用）
- 场景/预制体：优先 Unity MCP；失败给手动步骤；未要求不写菜单工具
- 过程文件 → `Cursor Work Space/`；运行时 → `Assets/_Game/`
- 运行时脚本少写保护性代码，尽早暴露配置/逻辑错误

## 当前状态

- MCP 包：`Packages/com.coplaydev.unity-mcp`
- 基础脚本：`Singleton` / `GameManager` / `PlayerController` / `CameraFollow`
- 编辑器连桥：`Assets/_Game/Editor/McpLocalHttpBootstrap.cs`
- Cursor 规则：`.cursor/rules/`（MCP 流程、项目约束、fail-fast、记忆）

## 关键控制文件

- `Cursor Work Space/control/ai-unity-workflow.md`
- `Cursor Work Space/control/editor-version.md`
- `.cursor/rules/unity-mcp-workflow.mdc`

## 未决 / 注意

- 本机开工程请用 62f3c1；Unity 启动若改写 `mcp.json` 为 8080，以 Bootstrap 写回的 8765 为准
- `Cursor Work Space/logs/`、`vendor/` 不进仓库

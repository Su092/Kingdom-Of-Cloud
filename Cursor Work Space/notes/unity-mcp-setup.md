# Unity MCP 连接过程

日期：2026-09-30

## 绑定（已生效）

- 编辑器：**Unity 6000.6.5f1**（`F:\Unity Hub\Unity\6000.6.5f1\Editor\Unity.exe`）
- 勿用 2022.3 / 团结引擎打开本工程
- HTTP：`http://127.0.0.1:8765/mcp`（8080 被 Steam 占用）
- 自动连桥 + 写回 Cursor 配置：`Assets/_Game/Editor/McpLocalHttpBootstrap.cs`
- 控制说明：`Cursor Work Space/control/editor-version.md`
- 迁移记录：`Cursor Work Space/memory/2026-10-23-migrate-unity-6000.6.5f1.md`

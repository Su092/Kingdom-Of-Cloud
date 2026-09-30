# 控制文件：Unity 场景 / 预制体操作

后续场景搭建、预制体编辑、Hierarchy 装配、组件挂接，按本文件执行。

## 编辑器

- 只用 **Unity 2022.3.62f3c1**（`D:\Unity\2022.3.62f3c1\Editor\Unity.exe`）
- MCP 桥只连该编辑器；勿用团结引擎打开本工程

## 记忆

- 整理上下文或阶段收尾时，更新 `Cursor Work Space/memory/CURRENT.md` 与按日主题文件
- 长期规范在 `.cursor/rules/` 与本目录；进度与坑在 `memory/`

## 优先级

1. **优先走 Unity MCP 桥接**  
   直接在已打开的 Unity 编辑器里完成：建物体、挂组件、改 Transform、做 Prefab、保存场景。过程记录写到 `Cursor Work Space/`，不要把说明当成替代操作。
2. **MCP 做不到或当前没有 Unity MCP**  
   不要半成品脚本凑合。写出可逐步执行的手动步骤：菜单路径、Hierarchy 物体名、组件字段、Inspector 赋值、保存位置。
3. **禁止默认写编辑器菜单工具**  
   未明确要求时，不要新增 `UnityEditor` 菜单栏、`[MenuItem]`、EditorWindow、一键装配按钮来完成场景或预制体工作。用户点名要工具时才写。

## 允许写进 `Assets/` 的内容

- 运行时脚本、资源、场景、预制体本身  
- MCP 在 Unity 里生成的场景与 Prefab  
- 用户明确要求的 Editor 工具

## 禁止用这些替代 MCP

- 手写 YAML 场景/Prefab 文件冒充搭建  
- 用 Editor 菜单脚本绕过 MCP  
- 只给笼统说明（“挂个脚本就行”）而不给步骤

## 失败时的步骤格式

```
目标：
前置：Unity 版本 / 打开的场景 / 需要的资源
步骤：
1. 菜单或 Hierarchy 操作
2. Inspector 字段 = 具体值
3. 保存路径（如 Assets/_Game/Res/Prefab/xxx.prefab）
验收：进 Play 后应看到什么
```

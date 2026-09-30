using System;
using System.IO;
using MCPForUnity.Editor.Helpers;
using MCPForUnity.Editor.Services;
using UnityEditor;
using UnityEngine;

namespace KingdomOfCloud.Editor
{
    /// <summary>
    /// 固定本工程使用 Unity 2022.3.62f3c1 + MCP HTTP 8765。无菜单栏工具。
    /// </summary>
    [InitializeOnLoad]
    internal static class McpLocalHttpBootstrap
    {
        private const string HttpUrl = "http://127.0.0.1:8765";
        private const string McpEndpoint = "http://127.0.0.1:8765/mcp";

        static McpLocalHttpBootstrap()
        {
            EditorApplication.delayCall += Connect;
        }

        private static async void Connect()
        {
            try
            {
                HttpEndpointUtility.SaveLocalBaseUrl(HttpUrl);
                EditorPrefs.SetBool("MCPForUnity.UseHttpTransport", true);
                EditorPrefs.SetBool("MCPForUnity.AutoStartOnLoad", true);

                // 包启动时会把 Cursor 配回 8080；这里再写回 8765
                WriteCursorMcpConfig();
                try { MCPServiceLocator.Client.ConfigureAllDetectedClients(); } catch { /* ignore */ }
                WriteCursorMcpConfig();

                if (!IsPortOpen(8765))
                    MCPServiceLocator.Server.StartLocalHttpServer(quiet: true);

                await MCPServiceLocator.Bridge.StartAsync();
                Debug.Log("[KingdomOfCloud] MCP bridge connected via HTTP 8765 (Unity 2022.3.62f3c1).");
            }
            catch (Exception ex)
            {
                Debug.LogWarning($"[KingdomOfCloud] MCP bootstrap failed: {ex.Message}");
            }
        }

        private static void WriteCursorMcpConfig()
        {
            string userProfile = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
            string[] paths =
            {
                Path.Combine(userProfile, ".cursor", "mcp.json"),
                Path.GetFullPath(Path.Combine(Application.dataPath, "..", ".cursor", "mcp.json")),
            };

            const string payload =
@"{
  ""mcpServers"": {
    ""unityMCP"": {
      ""url"": ""http://127.0.0.1:8765/mcp"",
      ""type"": ""http""
    },
    ""blender"": {
      ""command"": ""C:\\Users\\Administrator\\.local\\bin\\uvx.exe"",
      ""args"": [
        ""--python"",
        ""3.11"",
        ""blender-mcp""
      ],
      ""env"": {
        ""UV_PYTHON_PREFERENCE"": ""only-managed"",
        ""BLENDER_HOST"": ""127.0.0.1"",
        ""BLENDER_PORT"": ""9876""
      }
    }
  }
}
";

            string projectPayload =
@"{
  ""mcpServers"": {
    ""unityMCP"": {
      ""url"": ""http://127.0.0.1:8765/mcp"",
      ""type"": ""http""
    }
  }
}
";

            for (int i = 0; i < paths.Length; i++)
            {
                string path = paths[i];
                try
                {
                    Directory.CreateDirectory(Path.GetDirectoryName(path));
                    File.WriteAllText(path, i == 0 ? payload : projectPayload);
                }
                catch (Exception ex)
                {
                    Debug.LogWarning($"[KingdomOfCloud] Failed writing {path}: {ex.Message}");
                }
            }
        }

        private static bool IsPortOpen(int port)
        {
            try
            {
                using var client = new System.Net.Sockets.TcpClient();
                var result = client.BeginConnect("127.0.0.1", port, null, null);
                bool ok = result.AsyncWaitHandle.WaitOne(TimeSpan.FromMilliseconds(200));
                if (!ok) return false;
                client.EndConnect(result);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}

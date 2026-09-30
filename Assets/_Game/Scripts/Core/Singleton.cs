using UnityEngine;

namespace KingdomOfCloud.Core
{
    /// <summary>
    /// 场景内单例。不跨场景持久化；需要持久化时在子类 Awake 中自行 DontDestroyOnLoad。
    /// </summary>
    public abstract class Singleton<T> : MonoBehaviour where T : MonoBehaviour
    {
        public static T Instance { get; private set; }

        protected virtual void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Debug.LogWarning($"[{typeof(T).Name}] Duplicate instance destroyed on '{name}'.");
                Destroy(gameObject);
                return;
            }

            Instance = this as T;
        }

        protected virtual void OnDestroy()
        {
            if (Instance == this)
                Instance = null;
        }
    }
}

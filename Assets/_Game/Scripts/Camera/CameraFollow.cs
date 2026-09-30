using UnityEngine;
using KingdomOfCloud.Core;

namespace KingdomOfCloud.CameraControl
{
    /// <summary>
    /// 第三人称环绕相机：跟随目标，鼠标控制偏航/俯仰，滚轮调距离。
    /// </summary>
    public class CameraFollow : MonoBehaviour
    {
        [Header("Target")]
        [SerializeField] private Transform target;
        [SerializeField] private Vector3 targetOffset = new Vector3(0f, 1.6f, 0f);

        [Header("Orbit")]
        [SerializeField] private float mouseSensitivity = 3f;
        [SerializeField] private float minPitch = -25f;
        [SerializeField] private float maxPitch = 60f;
        [SerializeField] private float distance = 5.5f;
        [SerializeField] private float minDistance = 2f;
        [SerializeField] private float maxDistance = 10f;
        [SerializeField] private float zoomSpeed = 2f;
        [SerializeField] private float followSmooth = 18f;

        [Header("Collision")]
        [SerializeField] private LayerMask collisionMask = ~0;
        [SerializeField] private float collisionRadius = 0.25f;

        private float _yaw;
        private float _pitch = 15f;

        private void Start()
        {
            if (target == null)
            {
                GameObject player = GameObject.FindGameObjectWithTag("Player");
                if (player != null)
                    target = player.transform;
            }

            Vector3 angles = transform.eulerAngles;
            _yaw = angles.y;
            _pitch = angles.x;
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        private void LateUpdate()
        {
            if (target == null)
                return;

            bool paused = GameManager.Instance != null && GameManager.Instance.IsPaused;
            if (!paused)
                ReadLookInput();

            Quaternion rotation = Quaternion.Euler(_pitch, _yaw, 0f);
            Vector3 pivot = target.position + targetOffset;
            float desiredDistance = GetZoomedDistance();
            Vector3 desired = pivot - rotation * Vector3.forward * desiredDistance;
            desired = ResolveCollision(pivot, desired);

            float t = 1f - Mathf.Exp(-followSmooth * Time.unscaledDeltaTime);
            transform.position = Vector3.Lerp(transform.position, desired, t);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, t);
        }

        private void ReadLookInput()
        {
            _yaw += Input.GetAxis("Mouse X") * mouseSensitivity;
            _pitch -= Input.GetAxis("Mouse Y") * mouseSensitivity;
            _pitch = Mathf.Clamp(_pitch, minPitch, maxPitch);

            float scroll = Input.GetAxis("Mouse ScrollWheel");
            if (Mathf.Abs(scroll) > 0.0001f)
                distance = Mathf.Clamp(distance - scroll * zoomSpeed * 10f, minDistance, maxDistance);
        }

        private float GetZoomedDistance() => distance;

        private Vector3 ResolveCollision(Vector3 pivot, Vector3 desired)
        {
            Vector3 direction = desired - pivot;
            float length = direction.magnitude;
            if (length < 0.001f)
                return desired;

            if (Physics.SphereCast(
                    pivot,
                    collisionRadius,
                    direction.normalized,
                    out RaycastHit hit,
                    length,
                    collisionMask,
                    QueryTriggerInteraction.Ignore))
            {
                return pivot + direction.normalized * Mathf.Max(hit.distance - collisionRadius, 0.1f);
            }

            return desired;
        }

        public void SetTarget(Transform newTarget) => target = newTarget;
    }
}

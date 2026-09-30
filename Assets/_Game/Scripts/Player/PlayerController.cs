using UnityEngine;
using KingdomOfCloud.Core;

namespace KingdomOfCloud.Player
{
    /// <summary>
    /// 第三人称移动：WASD 相对相机平面移动，Space 跳跃，Shift 冲刺。
    /// 需要同物体上的 CharacterController。
    /// </summary>
    [RequireComponent(typeof(CharacterController))]
    public class PlayerController : MonoBehaviour
    {
        [Header("Move")]
        [SerializeField] private float walkSpeed = 4.5f;
        [SerializeField] private float sprintSpeed = 7.5f;
        [SerializeField] private float rotationSpeed = 12f;
        [SerializeField] private float gravity = -20f;
        [SerializeField] private float jumpHeight = 1.4f;

        [Header("Ground")]
        [SerializeField] private float groundedStick = -2f;

        [Header("Refs")]
        [SerializeField] private Transform cameraPivot;

        private CharacterController _controller;
        private Vector3 _velocity;
        private bool _grounded;

        private void Awake()
        {
            _controller = GetComponent<CharacterController>();
            if (cameraPivot == null && Camera.main != null)
                cameraPivot = Camera.main.transform;
        }

        private void Update()
        {
            if (GameManager.Instance != null && GameManager.Instance.IsPaused)
                return;

            UpdateGrounded();
            HandleMove();
            HandleJumpAndGravity();
            _controller.Move(_velocity * Time.deltaTime);
        }

        private void UpdateGrounded()
        {
            _grounded = _controller.isGrounded;
            if (_grounded && _velocity.y < 0f)
                _velocity.y = groundedStick;
        }

        private void HandleMove()
        {
            float h = Input.GetAxisRaw("Horizontal");
            float v = Input.GetAxisRaw("Vertical");
            Vector3 input = new Vector3(h, 0f, v);
            if (input.sqrMagnitude > 1f)
                input.Normalize();

            Vector3 move = GetCameraRelative(input);
            float speed = Input.GetKey(KeyCode.LeftShift) ? sprintSpeed : walkSpeed;
            Vector3 planar = move * speed;

            _velocity.x = planar.x;
            _velocity.z = planar.z;

            if (move.sqrMagnitude > 0.0001f)
            {
                Quaternion target = Quaternion.LookRotation(move, Vector3.up);
                transform.rotation = Quaternion.Slerp(
                    transform.rotation,
                    target,
                    rotationSpeed * Time.deltaTime);
            }
        }

        private void HandleJumpAndGravity()
        {
            if (_grounded && Input.GetButtonDown("Jump"))
                _velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);

            _velocity.y += gravity * Time.deltaTime;
        }

        private Vector3 GetCameraRelative(Vector3 input)
        {
            if (cameraPivot == null)
                return input;

            Vector3 forward = cameraPivot.forward;
            Vector3 right = cameraPivot.right;
            forward.y = 0f;
            right.y = 0f;
            forward.Normalize();
            right.Normalize();
            return forward * input.z + right * input.x;
        }
    }
}

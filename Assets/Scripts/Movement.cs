using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class Movement : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    [SerializeField] private float turnSpeed = 180f;
    [SerializeField] private float gravity = -20f;
    [SerializeField] private float jumpHeight = 2f;

    private CharacterController _controller;
    private float _verticalSpeed;

    private void Awake()
    {
        _controller = GetComponent<CharacterController>();
    }

    private void Update()
    {
        Vector2 input = ReadInput();
        transform.Rotate(Vector3.up, input.x * turnSpeed * Time.deltaTime);

        HandleJump();
        _verticalSpeed += gravity * Time.deltaTime;

        Vector3 velocity = transform.forward * (input.y * speed);
        velocity.y = _verticalSpeed;
        _controller.Move(velocity * Time.deltaTime);
    }

    private Vector2 ReadInput()
    {
        Keyboard keyboard = Keyboard.current;

        if (keyboard == null)
            return Vector2.zero;

        return new Vector2(
            keyboard.dKey.ReadValue() - keyboard.aKey.ReadValue(),
            keyboard.wKey.ReadValue() - keyboard.sKey.ReadValue());
    }

    private void HandleJump()
    {
        if (!_controller.isGrounded || _verticalSpeed > 0f)
            return;

        _verticalSpeed = 0f;

        if (Keyboard.current != null && Keyboard.current.spaceKey.wasPressedThisFrame)
            _verticalSpeed = Mathf.Sqrt(jumpHeight * -2f * gravity);
    }
}

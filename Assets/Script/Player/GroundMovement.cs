using Unity.Cinemachine;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

public class GroundMovement : MonoBehaviour
{
    InputAction speedAction;
    InputAction jumpAction;

    float speedInput;
    bool canBrake = false;

    Player player;
    Rigidbody2D body;

    [SerializeField] Animator animator;
    // Track the current speed milestone level

    void Start()
    {
        speedAction = InputSystem.actions["Player/Speed"];
        jumpAction = InputSystem.actions["Player/Jump"];

        speedAction.performed += OnSpeed;
        jumpAction.performed += OnJump;

        speedAction.canceled += OnSpeed;

        speedAction.Enable();
        jumpAction.Enable();

        player = GetComponent<Player>();
        body = GetComponent<Rigidbody2D>();
    }

    void OnDestroy()
    {
        speedAction.performed -= OnSpeed;
        jumpAction.performed -= OnJump;

        speedAction.canceled -= OnSpeed;

        speedAction.Disable();
        jumpAction.Disable();
    }

    void FixedUpdate()
    {
        animator.SetFloat("Run", Mathf.Abs(body.linearVelocityX / (player.data.MAX_SPEED + player.MaxSpeedModifier)));
        GroundCheck();

        if (speedInput < 0 && canBrake)
        {
            body.linearVelocityX = Mathf.MoveTowards(
                body.linearVelocityX,
                0,
                Time.fixedDeltaTime * 4 * player.data.ACCELERATION
            );
        }
        else if (speedInput > 0)
        {
            body.linearVelocityX = Mathf.MoveTowards(
                body.linearVelocityX,
                2 * (player.data.MAX_SPEED + player.MaxSpeedModifier),
                Time.fixedDeltaTime * 2 * player.data.ACCELERATION
            );
        }
        else
        {
            body.linearVelocityX = Mathf.MoveTowards(
                body.linearVelocityX,
                player.data.MAX_SPEED + player.MaxSpeedModifier,
                Time.fixedDeltaTime * player.data.ACCELERATION
            );
        }
        if (body.linearVelocityX >= (player.data.MAX_SPEED + player.MaxSpeedModifier))
        {
            canBrake = true;
        }
        else if (body.linearVelocityX == 0 && speedInput < 0)
        {
            canBrake = false;
        }

        player.Speed = body.linearVelocity.x;
        player.Distance = body.position.x;
        player.CheckSpeedMilestones();
        OutOfBoundsCheck();
    }

    void OnSpeed(InputAction.CallbackContext context)
    {
        speedInput = context.ReadValue<float>();
    }

    void OnJump(InputAction.CallbackContext context)
    {
        if (GroundCheck()) body.linearVelocityY = player.data.JUMP_FORCE;
    }

    RaycastHit2D GroundCheck()
    {
        return Physics2D.CapsuleCast(
            transform.position,
            new Vector2(0.75f, 1f),
            CapsuleDirection2D.Vertical,
            0,
            Vector2.down,
            0.05f,
            LayerMask.GetMask("Ground")
        );
    }

    void OutOfBoundsCheck()
    {
        if (transform.position.y < -25)
        {
            Player.Instance.TimeFinished = Time.time;
            Destroy(gameObject);
            Player.OnPlayerDeath.Invoke();
        }
    }
}

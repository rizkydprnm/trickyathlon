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

    void FixedUpdate()
    {
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
                2 * player.data.MAX_SPEED + player.max_speed_modifier,
                Time.fixedDeltaTime * 2 * player.data.ACCELERATION
            );
        }
        else
        {
            body.linearVelocityX = Mathf.MoveTowards(
                body.linearVelocityX,
                player.data.MAX_SPEED + player.max_speed_modifier,
                Time.fixedDeltaTime * player.data.ACCELERATION
            );
        }

        if (body.linearVelocityX >= player.data.MAX_SPEED + player.max_speed_modifier)
        {
            canBrake = true;
        }
        else if (body.linearVelocityX == 0 && speedInput < 0)
        {
            canBrake = false;
        }

        player.energy = Mathf.MoveTowards(
            player.energy, 0, Time.fixedDeltaTime * body.linearVelocityX / player.data.MAX_SPEED + player.max_speed_modifier
        );

        OutOfBounds();
        Player.distance += body.linearVelocityX * Time.fixedDeltaTime;
    }

    void OnSpeed(InputAction.CallbackContext context)
    {
        speedInput = context.ReadValue<float>();
    }

    void OnJump(InputAction.CallbackContext context)
    {
        if (GroundCheck()) body.linearVelocityY = player.data.JUMP_FORCE;
    }

    void OutOfBounds()
    {
        if (transform.position.y < -20)
        {
            Destroy(gameObject);
        }
    }

    RaycastHit2D GroundCheck()
    {
        return Physics2D.BoxCast(transform.position, Vector2.one, 0, Vector2.down, 0.05f, LayerMask.GetMask("Ground"));
    }
}

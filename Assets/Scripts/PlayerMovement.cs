using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public const int DEFAULT_SPEED = 4;

    private Rigidbody2D playerBody;
    private PlayerInput playerInput;
    private SpriteRenderer playerRenderer;

    // Start is called before the first frame update
    void Start()
    {
        playerInput = GetComponent<PlayerInput>();
        playerBody = GetComponent<Rigidbody2D>();
        playerRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        var velocity_keys = CombineKeyVelocities();
        var velocity_gamepad = GetGamepadVelocity();

        var velocity = NormalizeToDefaultSpeed(velocity_keys + velocity_gamepad);

        playerBody.velocity = velocity;
        FlipAccordingToDirection();
    }

    private void FlipAccordingToDirection()
    {
        bool isTargetToTheRight = playerBody.velocity.x > 0;

        if (isTargetToTheRight)
            playerRenderer.flipX = true;

        else playerRenderer.flipX = false;
    }

    private Vector2 GetGamepadVelocity()
    {
        return playerInput.actions["Move"].ReadValue<Vector2>();
    }

    private Vector2 CombineKeyVelocities()
    {
        var horizontal = Input.GetAxisRaw("Horizontal");
        var vertical = Input.GetAxisRaw("Vertical");
        return new Vector2(horizontal, vertical);
    }

    private Vector2 NormalizeToDefaultSpeed(Vector2 velocity)
    {
        if (velocity.magnitude != 0)
        {
            return velocity / (velocity.magnitude / DEFAULT_SPEED);
        }
        else
        {
            return velocity;
        }
    }
}

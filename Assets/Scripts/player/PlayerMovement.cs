using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public const int DEFAULT_SPEED = 4;

    private Rigidbody2D playerBody;
    private PlayerInput playerInput;
    private SpriteRenderer playerRenderer;
    private HealthBehaviour health;
    public Vector2 LastDirection { get; private set; } = new Vector2(-1, 0);

    // Start is called before the first frame update
    void Start()
    {
        playerInput = GetComponent<PlayerInput>();
        playerBody = GetComponent<Rigidbody2D>();
        playerRenderer = GetComponent<SpriteRenderer>();
        health = GetComponent<HealthBehaviour>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!health.isDead())
        {
            var combinedVelocity = CombineKeyVelocities() + GetGamepadVelocity();
            UpdateLastDirection(combinedVelocity);

            playerBody.velocity = NormalizeToDefaultSpeed(combinedVelocity);
            FlipAccordingToDirection();
        }
        else
        {
            playerBody.velocity = Vector2.zero;
        }
    }

    private void UpdateLastDirection(Vector2 combinedVelocity)
    {
        if (combinedVelocity != Vector2.zero)
        {
            LastDirection = combinedVelocity.normalized;
        }
    }

    private void FlipAccordingToDirection()
    {
        bool isTargetToTheRight = this.LastDirection.x > 0;

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

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Chest"))
        {
            gameObject.GetComponent<AttackManager>().ChooseSpell(Spell.FireballGreen);
        }
    }
}

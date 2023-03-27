using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public const int DEFAULT_SPEED = 4;

    PlayerInput playerInput;

    Vector3 STATIONARY = new Vector3(0, 0, 0);

    Dictionary<string, Vector3> keysToVelocity = new Dictionary<string, Vector3>() {
        {"up", Vector3.up},
        {"down", Vector3.down},
        {"left", Vector3.left},
        {"right", Vector3.right}
    };

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Hello, world!");

        playerInput = GetComponent<PlayerInput>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 velocity_keys = CombineKeyVelocities();
        Vector3 velocity_gamepad = GetGamepadVelocity();

        Vector3 velocity = NormalizeToDefaultSpeed( velocity_keys + velocity_gamepad );
        
        GetPlayerBody().velocity = velocity;
        FlipAccordingToDirection();
    }

    private void FlipAccordingToDirection() {
        bool isTargetToTheRight = GetPlayerBody().velocity.x > 0;

        if ( isTargetToTheRight )
            GetComponent<SpriteRenderer>().flipX = true;

        else GetComponent<SpriteRenderer>().flipX = false;
    }

    private Vector3 GetGamepadVelocity()
    {
        Vector2 gamepadInput = playerInput.actions["Move"].ReadValue<Vector2>();

        return new Vector3(gamepadInput.x, gamepadInput.y, 0);
    }

    private Vector3 CombineKeyVelocities()
    {
        var currentVelocity = STATIONARY;

        foreach (var (key, velocity) in keysToVelocity)
        {
            if (Input.GetKey(key))
            {
                currentVelocity = currentVelocity + velocity;
            }
        }

        return currentVelocity;
    }

    Vector3 NormalizeToDefaultSpeed(Vector3 velocity) {
        if (velocity.magnitude != 0) {
            return velocity / (velocity.magnitude / DEFAULT_SPEED);
        } else {
            return velocity;
        }
    }

    Rigidbody2D GetPlayerBody()
    {
        return GetComponent<Rigidbody2D>();
    }
}

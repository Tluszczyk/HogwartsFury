using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerMovement : MonoBehaviour
{
    const int DEFAULT_SPEED = 4;

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
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 velocity = CombineKeyVelocities();
        GetPlayerBody().velocity = NormalizeToDefaultSpeed(velocity);
        FlipAccordingToVelocity();
    }

    private void FlipAccordingToVelocity() {
        if (GetPlayerBody().velocity.x > 0) {
            GetComponent<SpriteRenderer>().flipX = true;
        } else if (GetPlayerBody().velocity.x < 0) {
            GetComponent<SpriteRenderer>().flipX = false;
        }
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

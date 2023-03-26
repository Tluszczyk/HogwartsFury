using UnityEngine;

public class OpponentMovement : MonoBehaviour
{
    public Transform target;

    const int DEFAULT_SPEED = 1;

    // Start is called before the first frame update
    void Update()
    {
        Vector3 velocity = (target.position - transform.position).normalized;
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

using UnityEngine;

public class OpponentMovement : MonoBehaviour
{
    public Transform target;

    public const int DEFAULT_SPEED = 1;
    public const float MAX_DISTANCE = 1f;

    // Start is called before the first frame update
    void Update()
    {
        Vector3 velocity = (target.position - transform.position).normalized;
        velocity = NormalizeToDefaultSpeed(velocity);
        
        if ( !IsCloseToTarget() ) {
            GetPlayerBody().velocity = velocity;
        } else {
            GetPlayerBody().velocity = Vector3.zero;
        }

        FlipAccordingToDirection();
    }

    private void FlipAccordingToDirection() {
        bool isTargetToTheRight = target.position.x > transform.position.x;

        if ( isTargetToTheRight )
            GetComponent<SpriteRenderer>().flipX = true;

        else GetComponent<SpriteRenderer>().flipX = false;
    }

    Vector3 NormalizeToDefaultSpeed(Vector3 velocity) {
        if (velocity.magnitude != 0) {
            return velocity / (velocity.magnitude / DEFAULT_SPEED);
        } else {
            return velocity;
        }
    }

    bool IsCloseToTarget() {
        return Vector3.Distance(transform.position, target.position) < MAX_DISTANCE;
    }

    Rigidbody2D GetPlayerBody()
    {
        return GetComponent<Rigidbody2D>();
    }
}

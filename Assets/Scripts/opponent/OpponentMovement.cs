using UnityEngine;

public class OpponentMovement : MonoBehaviour
{
    private Transform target;
    private Rigidbody2D opponentBody;
    private SpriteRenderer opponentRenderer;

    public const int DEFAULT_SPEED = 1;
    public const float MAX_DISTANCE = 1f;


    public void SetTarget(Transform target) {
        this.target = target;
    }


    void Start() {
        opponentBody = GetComponent<Rigidbody2D>();
        opponentRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        Vector3 velocity = (target.position - transform.position).normalized;
        velocity = NormalizeToDefaultSpeed(velocity);
        
        if ( !IsCloseToTarget() ) {
            opponentBody.velocity = velocity;
        } else {
            opponentBody.velocity = Vector2.zero;
        }

        FlipAccordingToDirection();
    }

    private void FlipAccordingToDirection() {
        bool isTargetToTheRight = target.position.x > transform.position.x;

        if ( isTargetToTheRight )
            opponentRenderer.flipX = true;

        else opponentRenderer.flipX = false;
    }

    Vector2 NormalizeToDefaultSpeed(Vector2 velocity) {
        if (velocity.magnitude != 0) {
            return velocity / (velocity.magnitude / DEFAULT_SPEED);
        } else {
            return velocity;
        }
    }

    bool IsCloseToTarget() {
        return Vector3.Distance(transform.position, target.position) < MAX_DISTANCE;
    }
}

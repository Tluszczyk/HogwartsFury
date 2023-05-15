using UnityEngine;
using System;

public class OpponentMovement : MonoBehaviour
{
    private Transform target;
    private Rigidbody2D opponentBody;
    private SpriteRenderer opponentRenderer;

    public const int DEFAULT_SPEED = 1;
    public const float MAX_DISTANCE = 1f;

    private float currentSpeed = DEFAULT_SPEED;
    private DateTime slowStartTime;
    private float slowDuration = 5000;


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

        if ( DateTime.Now > slowStartTime.AddMilliseconds(slowDuration) ) {
            currentSpeed = DEFAULT_SPEED;
            gameObject.GetComponent<SpriteRenderer>().color = Color.clear;
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

    public void SlowDown() {
        currentSpeed = DEFAULT_SPEED / 2;
        slowStartTime = DateTime.Now;
    }

    bool IsCloseToTarget() {
        return Vector3.Distance(transform.position, target.position) < MAX_DISTANCE;
    }
}

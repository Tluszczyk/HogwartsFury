using UnityEngine;
using System;

public class OpponentMovement : MonoBehaviour
{
    private Transform target;
    private Rigidbody2D opponentBody;
    private SpriteRenderer opponentRenderer;

    public const float DEFAULT_SPEED = 1f;
    public const float MAX_DISTANCE = 1f;

    private float currentSpeed = DEFAULT_SPEED;
    private DateTime slowStartTime;
    private float slowDuration = 5000;
    private Color defaultColor;


    public void SetTarget(Transform target)
    {
        this.target = target;
    }


    void Start()
    {
        opponentBody = GetComponent<Rigidbody2D>();
        opponentRenderer = GetComponent<SpriteRenderer>();
        defaultColor = opponentRenderer.color;
    }

    void Update()
    {
        Vector3 velocity = (target.position - transform.position).normalized;
        velocity = NormalizeToCurrentSpeed(velocity);

        if (!IsCloseToTarget())
        {
            opponentBody.velocity = velocity;
        }
        else
        {
            opponentBody.velocity = Vector2.zero;
        }

        if (currentSpeed < DEFAULT_SPEED && DateTime.Now > slowStartTime.AddMilliseconds(slowDuration))
        {
            currentSpeed = DEFAULT_SPEED;
            opponentRenderer.color = defaultColor;
        }

        FlipAccordingToDirection();
    }

    private void FlipAccordingToDirection()
    {
        bool isTargetToTheRight = target.position.x > transform.position.x;

        if (isTargetToTheRight)
            opponentRenderer.flipX = true;

        else opponentRenderer.flipX = false;
    }

    Vector2 NormalizeToCurrentSpeed(Vector2 velocity)
    {
        if (velocity.magnitude != 0)
        {
            return velocity / (velocity.magnitude / currentSpeed);
        }
        else
        {
            return velocity;
        }
    }

    public void SlowDown()
    {
        gameObject.GetComponent<SpriteRenderer>().color = Color.cyan;
        currentSpeed = DEFAULT_SPEED / 2;
        slowStartTime = DateTime.Now;
    }

    bool IsCloseToTarget()
    {
        return Vector3.Distance(transform.position, target.position) < MAX_DISTANCE;
    }
}

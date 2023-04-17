using UnityEngine;

public class FireballBehaviour : MonoBehaviour
{
    public static int SPELL_OFFSET_MS = 150;
    private const int DEFAULT_SPEED = 6;
    private Rigidbody2D fireballBody;
    private SpriteRenderer fireballRenderer;
    private MapBehaviour map;
    public GameObject self;

    public void Initialize(Vector2 direction)
    {
        fireballBody = GetComponent<Rigidbody2D>();
        fireballRenderer = GetComponent<SpriteRenderer>();
        fireballBody.velocity = DEFAULT_SPEED * direction.normalized;
        map = GameObject.FindGameObjectWithTag("Tilemap").GetComponent<MapBehaviour>();
        
        RotateAccordingToDirection();
    }

    public void Update()
    {
        if (map.isOutOfMapBounds(fireballBody.position))
        {
            Destroy(self);
        }
    }

    private void RotateAccordingToDirection()
    {
        fireballRenderer.flipX = true;
        transform.Rotate(0, 0, Mathf.Atan2(fireballBody.velocity.y, fireballBody.velocity.x) * Mathf.Rad2Deg);
    }
}


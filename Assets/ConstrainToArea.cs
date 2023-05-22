using UnityEngine;

/// <summary>
/// Keeps a gameObject's position within a predefined area.
/// </summary>
public class ConstrainToArea : MonoBehaviour
{
    // a convenient visual way to define the area
    public BoxCollider2D area;

    // called after Update, before rendering
    private void LateUpdate()
    {
        // get the current position
        Vector3 clampedPosition = transform.position;
        
        // limit the x and y positions to be between the area's min and max x and y.
        clampedPosition.x = Mathf.Clamp(clampedPosition.x, area.bounds.min.x, area.bounds.max.x);
        clampedPosition.y = Mathf.Clamp(clampedPosition.y, area.bounds.min.y, area.bounds.max.y);
        
        // z remains unchanged
        // apply the clamped position
        transform.position = clampedPosition;
    }
}
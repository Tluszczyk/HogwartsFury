using UnityEngine;
using System;

public class ChestBehaviour : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;

    public Sprite openedSprite;
    public Sprite closedSprite;

    private bool isOpened = false;
    private DateTime openedTime;
    public float timeToClose = 5f;

    void Start()
    {
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = closedSprite;
    }

    void Update() {
        if (isOpened && DateTime.Now > openedTime.AddSeconds(timeToClose)) {
            Destroy(gameObject);
        }
    }
    
    public void Open()
    {
        spriteRenderer.sprite = openedSprite;
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Open();
            isOpened = true;
            openedTime = DateTime.Now;
        }
    }
}

using UnityEngine;
using UnityEngine.Tilemaps;
using System;

public class MapBehaviour : MonoBehaviour
{
    public Tilemap tilemap;
    public Grid grid;

    public bool isOutOfMapBounds(Vector2 position) {
        return Math.Abs(position.x) > grid.cellSize.x * tilemap.size.x || Math.Abs(position.y) > grid.cellSize.y * tilemap.size.y;
    }
}

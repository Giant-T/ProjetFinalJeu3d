using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[RequireComponent(typeof(Tilemap))]
public class TileMapColliderGenerator : MonoBehaviour
{
    [SerializeField] private float thickness = 0.5f;

    private void Start()
    {
        Vector3 size = GetTileMapSize();
        AddCollider(size);
    }

    /// <summary>
    /// Calcule la grosseur de la tilemap.
    /// </summary>
    /// <returns>La grosseur de la tilemap</returns>
    private Vector3 GetTileMapSize()
    {
        Tilemap tilemap = GetComponent<Tilemap>();
        tilemap.CompressBounds();
        BoundsInt bounds = tilemap.cellBounds;

        Vector3 size = new Vector3(bounds.size.x, thickness, bounds.size.y);

        return size;
    }

    /// <summary>
    /// Ajoute les collisions au GameObject.
    /// </summary>
    /// <param name="size">La grosseur du BoxCollider.</param>
    private void AddCollider(Vector3 size)
    {
        BoxCollider collider = gameObject.AddComponent<BoxCollider>();

        collider.size = size;
        Vector3 colliderCenter = collider.center;
        colliderCenter.y -= thickness / 2;
        collider.center = colliderCenter;
    }
}

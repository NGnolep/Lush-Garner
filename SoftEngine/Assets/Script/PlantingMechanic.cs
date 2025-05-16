using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PlantingMechanics : MonoBehaviour
{
    public Tilemap groundTilemap;
    public TileBase plantedTile;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            TryPlant();
        }
    }

    void TryPlant()
    {
        Vector3Int cellPos = groundTilemap.WorldToCell(transform.position);
        TileBase currentTile = groundTilemap.GetTile(cellPos);

        if (IsUnplantedDirt(currentTile))
        {
            groundTilemap.SetTile(cellPos, plantedTile);
        }
    }

    bool IsUnplantedDirt(TileBase tile)
    {
        if (tile == null) return false;

        string name = tile.name.ToLower();
        return name.Contains("plantabledirt");
    }
}
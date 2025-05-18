using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using TMPro;

public class PlantingMechanics : MonoBehaviour
{
    public Tilemap groundTilemap;
    public TileBase plantedTile;
    public GameObject eText;
    void Update()
    {
        ShowOrHideEText();

        if (Input.GetKeyDown(KeyCode.E))
        {
            TryPlant();
        }
    }

    void ShowOrHideEText()
    {
        Vector3Int cellPos = groundTilemap.WorldToCell(transform.position);
        TileBase currentTile = groundTilemap.GetTile(cellPos);

        if (IsUnplantedDirt(currentTile))
        {
            eText.SetActive(true);
        }
        else
        {
            eText.SetActive(false);
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
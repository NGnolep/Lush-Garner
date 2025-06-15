using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.SceneManagement;
using TMPro;

public class PlantingMechanics : MonoBehaviour
{
    public Tilemap groundTilemap;
    public TileBase plantedTile;
    public TileBase grownTile;

    public GameObject eText;

    void Update()
    {
        ShowOrHideEText();

        if (Input.GetKeyDown(KeyCode.E))
        {
            Vector3 playerPos = transform.position + new Vector3(0, -0.1f, 0);
            Vector3Int cellPos = groundTilemap.WorldToCell(playerPos);
            TileBase currentTile = groundTilemap.GetTile(cellPos);

            if (IsUnplantedDirt(currentTile))
            {
                groundTilemap.SetTile(cellPos, plantedTile);
            }
            else if (currentTile == plantedTile)
            {
                LoadMinigame();
            }
        }
    }

    void ShowOrHideEText()
    {
        Vector3 playerPos = transform.position + new Vector3(0, -0.1f, 0);
        Vector3Int cellPos = groundTilemap.WorldToCell(playerPos);
        TileBase currentTile = groundTilemap.GetTile(cellPos);

        if (IsUnplantedDirt(currentTile) || currentTile == plantedTile)
        {
            eText.SetActive(true);
        }
        else
        {
            eText.SetActive(false);
        }
    }

    public void ReplacePlantedTiles()
    {
        BoundsInt bounds = groundTilemap.cellBounds;
    
        for (int x = bounds.xMin; x < bounds.xMax; x++)
        {
            for (int y = bounds.yMin; y < bounds.yMax; y++)
            {
                Vector3Int cell = new Vector3Int(x, y, 0);
                TileBase tile = groundTilemap.GetTile(cell);
    
                if (tile == plantedTile)
                {
                    groundTilemap.SetTile(cell, grownTile);
                }
            }
        }
    }
    bool IsUnplantedDirt(TileBase tile)
    {
        if (tile == null) return false;
        return tile.name.ToLower().Contains("plantabledirt");
    }

    void LoadMinigame()
    {
        //Time.timeScale = 0f;
        SceneManager.LoadScene("Watering", LoadSceneMode.Additive);
    }
}

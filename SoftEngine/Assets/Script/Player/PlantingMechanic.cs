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

    bool IsUnplantedDirt(TileBase tile)
    {
        if (tile == null) return false;
        return tile.name.ToLower().Contains("plantabledirt");
    }

    void LoadMinigame()
    {
        Time.timeScale = 0f;
        SceneManager.LoadScene("Minigame1", LoadSceneMode.Additive);
    }
}

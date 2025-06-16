using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.SceneManagement;
using TMPro;
using Unity.VisualScripting;

public class PlantingMechanics : MonoBehaviour
{
    public Tilemap groundTilemap;
    public TileBase plantedTile;
    public TileBase grownTile;
    public TileBase insectFreeTile;
    public TileBase resetTile;
    public TextMeshProUGUI shortText;
    public TextMeshProUGUI longText;
    public bool shortShown = false;
    public bool longShown = false;

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
                // GameObject player = GameObject.FindWithTag("Player");
                // player.GetComponent<PlayerMovement>().enabled = false;

                // Rigidbody2D rb = player.GetComponent<Rigidbody2D>();
                // rb.isKinematic = true;
                SceneManager.LoadScene("Watering", LoadSceneMode.Additive);
            }
            else if (currentTile == grownTile)
            {
                ReplaceGrownTiles();
                // GameObject player = GameObject.FindWithTag("Player");
                // player.GetComponent<PlayerMovement>().enabled = false;

                // Rigidbody2D rb = player.GetComponent<Rigidbody2D>();
                // rb.isKinematic = true;
                //SceneManager.LoadScene("InsectDefend", LoadSceneMode.Additive);
            }
            else if (currentTile == insectFreeTile)
            {
                ResetAllHarvestedTiles();
                // GameObject player = GameObject.FindWithTag("Player");
                // player.GetComponent<PlayerMovement>().enabled = false;

                // Rigidbody2D rb = player.GetComponent<Rigidbody2D>();
                // rb.isKinematic = true;
                //SceneManager.LoadScene("Harvest", LoadSceneMode.Additive);
            }
        }
    }

    void ShowOrHideEText()
    {
        Vector3 playerPos = transform.position + new Vector3(0, -0.1f, 0);
        Vector3Int cellPos = groundTilemap.WorldToCell(playerPos);
        TileBase currentTile = groundTilemap.GetTile(cellPos);

        if (IsUnplantedDirt(currentTile) && !longShown)
        {
            shortText.gameObject.SetActive(true);
            shortText.text = "to Plant";
            shortShown = true;
            longShown = false;
        }
        else if (currentTile == plantedTile && !longShown)
        {
            shortText.gameObject.SetActive(true);
            shortText.text = "to Water";
            shortShown = true;
            longShown = false;
        }
        else if (currentTile == grownTile && !shortShown)
        {
            longText.gameObject.SetActive(true);
            longText.text = "to Defend";
            longShown = true;
            shortShown = false;
        }
        else if (currentTile == insectFreeTile && !shortShown)
        {
            longText.gameObject.SetActive(true);
            longText.text = "to Harvest";
            longShown = true;
            shortShown = false;
        }
        else
        {
            shortText.gameObject.SetActive(false);
            longText.gameObject.SetActive(false);
            shortShown = false;
            longShown = false;
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
    public void ReplaceGrownTiles()
    {
        BoundsInt bounds = groundTilemap.cellBounds;

        for (int x = bounds.xMin; x < bounds.xMax; x++)
        {
            for (int y = bounds.yMin; y < bounds.yMax; y++)
            {
                Vector3Int cell = new Vector3Int(x, y, 0);
                if (groundTilemap.GetTile(cell) == grownTile)
                {
                    groundTilemap.SetTile(cell, insectFreeTile);
                }
            }
        }
    }

    public void ResetAllHarvestedTiles()
    {
        BoundsInt bounds = groundTilemap.cellBounds;

        for (int x = bounds.xMin; x < bounds.xMax; x++)
        {
            for (int y = bounds.yMin; y < bounds.yMax; y++)
            {
                Vector3Int cell = new Vector3Int(x, y, 0);
                if (groundTilemap.GetTile(cell) == insectFreeTile)
                {
                    groundTilemap.SetTile(cell, resetTile);
                }
            }
        }
    }
    bool IsUnplantedDirt(TileBase tile)
    {
        if (tile == null) return false;
        return tile.name.ToLower().Contains("plantabledirt");
    }
}

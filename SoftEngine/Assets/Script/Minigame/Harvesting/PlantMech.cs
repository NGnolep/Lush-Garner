using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantMech : MonoBehaviour
{
    private bool isDragging = false;
    private Vector3 offset;
    private BoxTrigger basket;
    private Collider2D plantCollider;
    private HarvestManager manager;

    void Start()
    {
        basket = FindObjectOfType<BoxTrigger>();
        plantCollider = GetComponent<Collider2D>();
        manager = FindObjectOfType<HarvestManager>();
    }
    void Update()
    {
        if (isDragging)
        {
            transform.position = GetMouseWorldPos() + offset;
        }
    }

    void OnMouseDown()
    {
        isDragging = true;
        offset = transform.position - GetMouseWorldPos();
        
        basket.SetHighlight(true);
    }

    void OnMouseUp()
    {
        isDragging = false;
        
        basket.SetHighlight(false);

        if (basket.IsInsideBasket(plantCollider))
        {
            manager?.OnPlantHarvested();
            HarvestSFX.Instance.PlayHarvestSound();
            Destroy(gameObject);
        }
    }
    Vector3 GetMouseWorldPos()
    {
        Vector3 mousePoint = Input.mousePosition;
        mousePoint.z = Camera.main.WorldToScreenPoint(transform.position).z;
        return Camera.main.ScreenToWorldPoint(mousePoint);
    }
}

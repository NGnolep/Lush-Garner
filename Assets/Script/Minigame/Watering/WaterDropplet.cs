using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterDropplet : MonoBehaviour
{
    public float lifetime = 3f;

    void Start()
    {
        Destroy(gameObject, lifetime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Dirt"))
        {
            PlantGrowth plant = FindObjectOfType<PlantGrowth>();
            if (plant != null)
            {
                plant.Water();
            }

            Destroy(gameObject);
        }
    }
}

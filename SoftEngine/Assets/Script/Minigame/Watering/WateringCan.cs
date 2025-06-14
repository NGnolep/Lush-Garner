using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WateringCan : MonoBehaviour
{
    public GameObject dropletPrefab;
    public Transform dropletSpawnPoint;
    public float dropRate = 0.2f;
    private float timer = 0f;

    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            timer += Time.deltaTime;
            if (timer >= dropRate)
            {
                Instantiate(dropletPrefab, dropletSpawnPoint.position, Quaternion.identity);
                timer = 0f;
            }
        }
        else
        {
            timer = dropRate;
        }
    }
}

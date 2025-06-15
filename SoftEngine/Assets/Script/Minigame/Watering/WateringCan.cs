using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WateringCan : MonoBehaviour
{
    public GameObject dropletPrefab;
    public Transform dropletSpawnPoint;
    public float fireRate = 0.2f;
    private float tiltAngle = 45f;
    private float tiltSpeed = 10f;

    private float timer = 0f;
    private bool isHolding = false;
    private bool isTilted = false;

    private Quaternion originalRotation;
    private Quaternion tiltedRotation;

    void Start()
    {
        originalRotation = Quaternion.Euler(0f, 0f, 0f);
        tiltedRotation = Quaternion.Euler(0f, 0f, tiltAngle);
    }

    void Update()
    {
        isHolding = Input.GetMouseButton(0);

        Quaternion desiredRotation = isHolding ? tiltedRotation : originalRotation;
        transform.rotation = Quaternion.Lerp(transform.rotation, desiredRotation, Time.deltaTime * tiltSpeed);

        isTilted = Quaternion.Angle(transform.rotation, tiltedRotation) < 1f;

        if (isHolding && isTilted)
        {
            timer += Time.deltaTime;
            if (timer >= fireRate)
            {
                Instantiate(dropletPrefab, dropletSpawnPoint.position, Quaternion.identity);
                timer = 0f;
            }
        }
        else
        {
            timer = fireRate;
        }
    }
}

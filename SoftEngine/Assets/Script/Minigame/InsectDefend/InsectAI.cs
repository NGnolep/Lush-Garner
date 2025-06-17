using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InsectAI : MonoBehaviour
{
    public float speed = 2f;
    private Transform target;
    private InsectSpawner spawner;

    public void SetTarget(Transform t)
    {
        target = t;
    }
    public void SetSpawner(InsectSpawner s)
    {  
        spawner = s;
    }

    void Update()
    {
        if (target == null) return;

        Vector3 direction = (target.position - transform.position).normalized;
        transform.position += direction * speed * Time.deltaTime;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, angle - 90f);
    }

    void OnMouseDown()
    {
        spawner.insectAlive--;
        Destroy(gameObject);
    }
}

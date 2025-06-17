using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxTrigger : MonoBehaviour
{
    private Vector3 originalScale;
    public float highlightScale = 1.2f;

    void Start()
    {
        originalScale = transform.localScale;
    }

    public void SetHighlight(bool active)
    {
        transform.localScale = active ? originalScale * highlightScale : originalScale;
    }

    public bool IsInsideBasket(Collider2D plantCollider)
    {
        Collider2D basketCollider = GetComponent<Collider2D>();
        return basketCollider.bounds.Intersects(plantCollider.bounds);
    }
}

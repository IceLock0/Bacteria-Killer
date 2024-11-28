using System.Collections.Generic;
using UnityEngine;

public class ClosestObjectDetectorService
{
    public T GetClosestObjectInBoxByType<T>(Vector2 at, Vector2 size, float angle = 0)
    {
        T closestObject = default;

        var allColliders = Physics2D.OverlapBoxAll(at, size, angle);
        
        var filteredColliders = FilterCollidersByType<T>(allColliders);

        var closestCollider = FindClosestCollider(filteredColliders, at);
        
        closestObject = closestCollider.GetComponent<T>();
        
        return closestObject;
    }

    private List<Collider2D> FilterCollidersByType<T>(Collider2D[] allColliders)
    {
        var filteredColliders = new List<Collider2D>();

        foreach (var collider in allColliders)
        {
            var isComponentExist = collider.TryGetComponent(typeof(T), out Component component);

            if (isComponentExist)
                filteredColliders.Add(collider);
        }

        return filteredColliders;
    }

    private Collider2D FindClosestCollider(List<Collider2D> colliders, Vector2 at)
    {
        Collider2D closestCollider = null;
        
        float minDistance = 0;

        foreach (var collider in colliders)
        {
            var distance = (at - (Vector2) collider.transform.position).magnitude;

            if (distance < minDistance)
            {
                minDistance = distance;
                closestCollider = collider;
            }
        }

        return closestCollider;
    }
}

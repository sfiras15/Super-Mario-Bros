using System;
using Unity.VisualScripting;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

public static class Extensions
{
    private static LayerMask layerMask = LayerMask.GetMask("Default"); 
    // Rigidbody2D is extended with this new method 
    public static bool Raycast(this Rigidbody2D rigidbody,Vector2 direction)
    {
        float distance;
        if (rigidbody.isKinematic)
        {
            return false;
        }
        // koopa is taller so his y position is higher than the other gameobjects
        if (rigidbody.gameObject.CompareTag("Koopa"))
        {
            distance = 0.525f;
        }
        else
        {
           distance = 0.375f;
        }
        float radius = 0.25f;    
        RaycastHit2D hit = Physics2D.CircleCast(rigidbody.position, radius, direction.normalized, distance,layerMask);
        return hit.collider != null && hit.rigidbody != rigidbody;
    }
    // Transform is extended with this new method 
    // Checks whether direction of these transforms have roughly the same direction as the testDirection
    public static bool DotTest(this Transform transform,Transform other , Vector2 testDirection)
    {
        Vector2 direction = other.position - transform.position;
        return Vector2.Dot(direction.normalized, testDirection) > 0.3f; // ~~ 77.24°. the more you decrease the value the larger the hit zone grows

        
    }
}


using System;
using Unity.VisualScripting;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

public static class Extensions
{
    private static LayerMask layerMask = LayerMask.GetMask("Default"); // why is it static? and why layermask ? getmask returns an int 
    public static bool Raycast(this Rigidbody2D rigidbody,Vector2 direction)
    {
        float distance;
        if (rigidbody.isKinematic)
        {
            return false;
        }
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

    public static bool DotTest(this Transform transform,Transform other , Vector2 testDirection)//why is it static?
    {
        Vector2 direction = other.position - transform.position;
        return Vector2.Dot(direction.normalized, testDirection) > 0.3f; // the more you decrease the value the larger the hit zone grows

        
    }
}


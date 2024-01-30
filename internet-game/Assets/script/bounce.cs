using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bounce : MonoBehaviour
{
    // Start is called before the first frame update
    private Vector2 lastVelocity;
    private Rigidbody2D rb;
     
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
     
    // save velocity for use after a collision
    private void FixedUpdate()
    {
        lastVelocity = rb.velocity;
    }
     
    // reflect velocity off the surface
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Vector2 towardsCollision = collision.transform.position - transform.position;
        Ray2D ray = new Ray2D(transform.position, towardsCollision);
        RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, 1f);
        if(hit.transform != null)
        {
            Vector2 surfaceNormal = hit.normal;
            rb.velocity = Vector2.Reflect(lastVelocity, surfaceNormal);
        }
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    Rigidbody2D rigidbody2d;
    
    void Awake()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
    }
    
    public void Launch(Vector2 direction, float force)
    {
        rigidbody2d.AddForce(direction * (force * 3));
    }
    
    void Update()
    {
        if(transform.position.magnitude > 40.0f)
        {
            Destroy(gameObject);
        }
    }
    
    void OnCollisionEnter2D(Collision2D other)
    {
        Enemy e = other.collider.GetComponent<Enemy>();
        if (e != null)
        {
            e.damage(1);
        }
    
        Destroy(gameObject);
    }
}
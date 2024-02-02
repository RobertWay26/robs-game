using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int hitpoints;
    bool dying = false;
    Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        if(hitpoints<=0)
        {
            death();
        }
    }
    
    public void damage(int d)
    {
        hitpoints = hitpoints - d;
    }
    
    void death()
    {
        dying = true;
        //anim.Play("death")
        //if(death is done) this can be done with a method that idk or just waiting an amount of time, deltaTime will not work because it is not in the FixedUpdate function
        Destroy(gameObject);
    }

    void OnCollisionStay2D(Collision2D other)
    {
        Player e = other.collider.GetComponent<Player>();

          if (e != null)
          {
              e.ChangeHealth(-1);
          }
    }
}

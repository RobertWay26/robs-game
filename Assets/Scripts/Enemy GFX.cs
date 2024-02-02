using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class EnemyGFX : MonoBehaviour
{
    public AIPath aiPath;
    public Animator anim;
    // Update is called once per frame
    void Update()
    {
        if(aiPath.desiredVelocity.x >= 0.01f) {
            transform.localScale = new Vector3(1f, 1f, 1f); anim.SetBool("isMoving", true);}
        else if(aiPath.desiredVelocity.x <= -0.01f) {
            transform.localScale = new Vector3(-1f, 1f, 1f); anim.SetBool("isMoving", true);}
        if(!(aiPath.desiredVelocity.x <= -0.01f) && !(aiPath.desiredVelocity.x >= 0.01f))
            anim.SetBool("isMoving", true);
    }
}

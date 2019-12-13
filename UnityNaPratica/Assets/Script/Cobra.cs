using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cobra : EnemyController
{
   void Start()
    {
        health = 2;
    }

    void Update()
    {
        float distance = PlayerDistance();

        isMoving = (distance <= distanceAttack);

        if (isMoving)
        {
            if((player.position.x > transform.position.x && sprite.flipX) || 
              (player.position.x > transform.position.x && !sprite.flipX)){
                Flip();
            }
        }
    }

    void FixedUpdate()
    {
        if (isMoving)
        {
            rb2d.velocity = new Vector2(speed, rb2d.velocity.y);
        }
         
    }

}

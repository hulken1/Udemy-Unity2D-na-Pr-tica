using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public int health;
    public float speed;
    public float distanceAttack;

    protected Rigidbody2D rb2d;
    protected Animator anim;
    protected Transform player;

    protected bool isMoving = false;
    protected SpriteRenderer sprite;
    protected bool facingRight = false;

    void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
        player = GameObject.Find("Player").GetComponent<Transform>();
    }
   
    protected float PlayerDistance()
    {
        return Vector2.Distance(player.position, transform.position);
    }

    protected virtual void Update()
    {
        float distance = PlayerDistance();

        isMoving = (distance <= distanceAttack);

        if (isMoving)
        {
            if ((player.position.x > transform.position.x && sprite.flipX) ||
               (player.position.x < transform.position.x && !sprite.flipX))
            {
                Flip();
            }
        }
    }
    protected void Flip()
    {
        //Validando se está virado para direita ou esquerda
        facingRight = !facingRight;
        transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
        speed *= -1;
    }
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Attack"))
        {
            DamageEnemy();
        }
    }

    IEnumerator DamageEffect()
    {
        float actualSpeed = speed;
        speed = speed * -1;
        sprite.color = Color.red;
        rb2d.AddForce(new Vector2(0f, 200f));
        yield return new WaitForSeconds(0.1f);
        speed = actualSpeed;
        sprite.color = Color.white;

    }
    void DamageEnemy()
    {
        health--;
        StartCoroutine(DamageEffect());

        if (health < 1)
        {
            Destroy(gameObject);
        }
    }
}

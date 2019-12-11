using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snake : MonoBehaviour
{
    public float speed;
    public int health;
    public Transform wallCheck;

    private bool facingRight = true;

    private SpriteRenderer sprite;
    private Rigidbody2D rb2d;

    private bool tocheddWall = false;
    // Start is called before the first frame update
    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        rb2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {      
        tocheddWall = Physics2D.Linecast(transform.position, wallCheck.position, 1 << LayerMask.NameToLayer("Ground"));

        if (tocheddWall)
        {
            Flip();
        }

    }
    void FixedUpdate()
    {
        rb2d.velocity = new Vector2(speed, rb2d.velocity.y);
    }
    //Logica para mudar de lado e andar 
    void Flip()
    {
        facingRight = !facingRight;
        transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y,transform.localScale.z);
        speed *= -1;
    }

    void OnTriggerEnter2D (Collider2D col)
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

        if(health < 1)
        {
            Destroy(gameObject);
        }
    }
}

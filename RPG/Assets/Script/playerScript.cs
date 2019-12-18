using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerScript : MonoBehaviour
{
    private Animator playerAnimator;

    public bool Grounded; // Indica se está no chao ou superficie
    public int idAnimation;  //Id da animação
    private Rigidbody2D playerRB;
    public Transform groundCheck; //Objeto responsavel por detectar se o personagem esta encostando no chao
    private float h, v;  // VARIALVE MOVIMENTO HORIZONTAL E VERTICAL
    public float speed; // Velocidade de movimento
    public float jumpForce; // Força aplicada para gerar o pulo do personagem
    public bool LookLeft; //Olhando para esquerda
    public bool attacking; // indicar se o personagem esta atacando
    public Collider2D standing, crouching; // colisor empe e abaixado
    // Start is called before the first frame update
    void Start()
    {
        playerAnimator = GetComponent<Animator>(); // INICIALIZA O COMPONENT A VARIAVEL
        playerRB = GetComponent<Rigidbody2D>(); // INICIALIZA O COMPONENT A VARIAVEL
    }

    void FixedUpdate() // TAXA DE ATT FIXA DE 0.02
    {
        Grounded = Physics2D.OverlapCircle(groundCheck.position, 0.02f);
        playerRB.velocity = new Vector2(h * speed, playerRB.velocity.y);
    }
    // Update is called once per frame
    void Update()
    {
        h = Input.GetAxis("Horizontal");
        v = Input.GetAxis("Vertical");

        if(h > 0 && LookLeft == true && attacking == false)
        {
            Flip();
        }else if (h < 0 && !LookLeft)
        {
            Flip();
        }

        if (v < 0)
        {
            idAnimation = 2;
            if(Grounded == true)
            {
                h = 0;
            }
            
        }else if (h != 0)
        {
            idAnimation = 1;
        }
        else
        {
            idAnimation = 0;
        }

        if (Input.GetButtonDown("Fire1") && v >= 0 && attacking == false)
        {
            playerAnimator.SetTrigger("attack");
        }

        if (Input.GetButtonDown("Jump") && Grounded == true && attacking == false)
        {
            playerRB.AddForce(new Vector2(0, jumpForce));

        }

        if(attacking == true && Grounded == true)
        {
            h = 0;

        }

        if(v < 0 && Grounded == true)
        {
            crouching.enabled = true;
            standing.enabled = false;
        }
        else if(v >= 0 && Grounded == true)
        {
            crouching.enabled = false;
            standing.enabled = true;
        }
        else if(v != 0 && Grounded == false)
        {
            crouching.enabled = false;
            standing.enabled = true;
        }
        playerAnimator.SetBool("grounded", Grounded);
        playerAnimator.SetInteger("idAnimation", idAnimation);
        playerAnimator.SetFloat("speedY", playerRB.velocity.y);
    }

    void Flip()
    {
        LookLeft = !LookLeft; // inverte o valor da variavel
        float x = transform.localScale.x;
        x *= -1; // inverte o sinal do scale x
        transform.localScale = new Vector3(x, transform.localScale.y, transform.localScale.z);
    }

    void attack(int atk)
    {
        switch (atk)
        {
            case 0:
                attacking = false;
                break;
            case 1:
                attacking = true;
                break;
        }
    }
}

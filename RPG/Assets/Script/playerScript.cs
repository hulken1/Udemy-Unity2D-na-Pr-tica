using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerScript : MonoBehaviour
{
    private _GameController _GameController;

    private Animator playerAnimator;
    private Rigidbody2D playerRB;
    public Transform groundCheck; //Objeto responsavel por detectar se o personagem esta encostando no chao
    public LayerMask whatIsGround; // indicia oque é superficie para o teste do grounded
    public Collider2D standing, crouching; // colisor empe e abaixado

    public int vidaMax, vidaAtual;

    public bool Grounded; // Indica se está no chao ou superficie
    public int idAnimation;  //Id da animação
    private float h, v;  // VARIALVE MOVIMENTO HORIZONTAL E VERTICAL
    public float speed; // Velocidade de movimento
    public float jumpForce; // Força aplicada para gerar o pulo do personagem
    public bool LookLeft; //Olhando para esquerda
    public bool attacking; // indicar se o personagem esta atacando

    //Variaveis de interação com items
    public Transform hand;
    private Vector3 dir = Vector3.right;
    public LayerMask interacao;
    public GameObject objetoInteracao;

    // sistema de armas
    public GameObject[] armas;

    // Start is called before the first frame update
    void Start()
    {
        vidaAtual = vidaMax;

        _GameController = FindObjectOfType(typeof(_GameController)) as _GameController;

        playerAnimator = GetComponent<Animator>(); // INICIALIZA O COMPONENT A VARIAVEL
        playerRB = GetComponent<Rigidbody2D>(); // INICIALIZA O COMPONENT A VARIAVEL

        //desativar objeto arma
        foreach(GameObject o in armas)
        {
            o.SetActive(false);
        }
    }

    void FixedUpdate() // TAXA DE ATT FIXA DE 0.02
    {
        Grounded = Physics2D.OverlapCircle(groundCheck.position, 0.02f, whatIsGround);
        playerRB.velocity = new Vector2(h * speed, playerRB.velocity.y);
        interagir();
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

        if (Input.GetButtonDown("Fire1") && v >= 0 && attacking == false && objetoInteracao == null)
        {
            playerAnimator.SetTrigger("attack");
        }
        // chamando funcao do script do bau para abrir e fechar, mandando um SendMessageOptions
        if (Input.GetButtonDown("Fire1") && v >= 0 && attacking == false && objetoInteracao != null)
        {
            objetoInteracao.SendMessage("interacao", SendMessageOptions.DontRequireReceiver);
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
        // devinir a direçao da set Raycast
        dir.x = x;
    }

    void attack(int atk)
    {
        switch (atk)
        {
            case 0:
                attacking = false;
                armas[2].SetActive(false);
                break;
            case 1:
                attacking = true;
                break;
        }
    }
    
    void interagir()
    {
        Debug.DrawRay(hand.position, dir * 0.1f, Color.red);
        RaycastHit2D hit = Physics2D.Raycast(hand.position, dir, 0.1f, interacao);

        if (hit)
        {
            objetoInteracao = hit.collider.gameObject;
        }
        else
        {
            objetoInteracao = null;
        }
    }

    void controleArma(int id)
    {
        foreach (GameObject o in armas)
        {
            o.SetActive(false);
        }

        armas[id].SetActive(true);
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        switch (col.gameObject.tag)
        {
            case "coletavel":
                col.gameObject.SendMessage("coletar", SendMessageOptions.DontRequireReceiver);

            break;
        }
    }
}

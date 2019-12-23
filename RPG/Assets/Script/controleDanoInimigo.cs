using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class controleDanoInimigo : MonoBehaviour
{
    private _GameController _GameController;
    private playerScript playerScript;
    private SpriteRenderer sRender;
    private Animator animator;

    [Header("Configuração de Vida")]
    public int vidaInimigo;
    public int vidaAtual;
    public GameObject barrasVida;// objetos contendo todas as barars
    public Transform hpBar;// objetoo indicador da quantidade de VIDA
    private float percVida;//controla o percentual de vida
    public GameObject danoTxtPrefab; // objeto que ira exibir dano tomado

    [Header("Configuração Resistencia/Fraqueza")]
    public float[] ajusteDano; // sistema de resistencia/fraqueza contra determinado tipo de dano

    [Header("Configuração KnockBack")]
    public GameObject knockForcePrefab; // forca repulsao
    public Transform knockPosition; // ponto de origem da força
    public float knockX; // valor padrao do position x
    private float kx;
    public bool olhandoEsquerda, playerEsquerda;
    private bool getHit; // indica se tomou hit

    public Color[] characterColor;
    private bool death; // indica se esta morto
    // Start is called before the first frame update
    void Start()
    {
        
        _GameController = FindObjectOfType(typeof(_GameController)) as _GameController;
        playerScript = FindObjectOfType(typeof(playerScript)) as playerScript;
        sRender = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();

        barrasVida.SetActive(false);
        vidaAtual = vidaInimigo;
        hpBar.localScale = new Vector3(1, 1, 1);

        sRender.color = characterColor[0];
        if (olhandoEsquerda)
        {
            float x = transform.localScale.x;
            x *= -1; // inverte o sinal do scale x
            transform.localScale = new Vector3(x, transform.localScale.y, transform.localScale.z);
            barrasVida.transform.localScale = new Vector3(x, barrasVida.transform.localScale.y, barrasVida.transform.localScale.z);
        }
        
    }
    void Update()
    {
        // verifica se o player esta a esquerda ou direita do inimigo
        float xPlayer = playerScript.transform.position.x;

        if(xPlayer < transform.position.x)
        {
            playerEsquerda = true;
        }
        else if(xPlayer > transform.position.x) 
        {
            playerEsquerda = false;
        }

        if(olhandoEsquerda && playerEsquerda)
        {
            kx = knockX;
        }
        else if (!olhandoEsquerda && playerEsquerda)
        {
            kx = knockX * -1;
        }
        else if (olhandoEsquerda && !playerEsquerda)
        {
            kx = knockX * -1;
        }
        else if (!olhandoEsquerda && !playerEsquerda)
        {
            kx = knockX;
        }

        knockPosition.localPosition = new Vector3(kx, knockPosition.localPosition.y,0);
        animator.SetBool("grounded", true);

    }
    void OnTriggerEnter2D(Collider2D col)
    {
        if(death == true) { return; }
        switch (col.tag)
        {
            case "arma":

                if(!getHit)
                {
                    getHit = true;
                    barrasVida.SetActive(true);
                    infoArmas armasInfo = col.gameObject.GetComponent<infoArmas>();

                    animator.SetTrigger("hit");

                    float danoArma = Random.Range(armasInfo.danoMin, armasInfo.danoMax);
                    int tipoDano = armasInfo.tipoDano;

                    // danoTomado = danoArma +(danoArma * (ajusteDano[id] / 100))
                    float danoTomado = danoArma + (danoArma * (ajusteDano[tipoDano] / 100));

                    vidaAtual -= Mathf.RoundToInt(danoTomado); // reduz da vida a quantidade de deano tomado

                    percVida = (float) vidaAtual / (float)vidaInimigo; // CALCULA PERCENTUAL DE VIDA
                    if(percVida < 0)
                    {
                        percVida = 0;
                    }

                    hpBar.localScale = new Vector3(percVida, 1, 1);

                    if (vidaAtual <= 0)
                    {
                        animator.SetInteger("idAnimation", 3);
                        death = true;
                        Destroy(this.gameObject, 2f);
                    }

                    GameObject danoTemp = Instantiate(danoTxtPrefab, transform.position, transform.localRotation);
                    danoTemp.GetComponent<TextMesh>().text = Mathf.RoundToInt(danoTomado).ToString();
                    danoTemp.GetComponent<MeshRenderer>().sortingLayerName = "hud";

                    GameObject fxTemp = Instantiate(_GameController.fxDano[tipoDano], transform.position, transform.localRotation);
                    Destroy(fxTemp, 1f);

                    int forcaX = 80;
                    if (!playerEsquerda)
                    {
                        forcaX *= -1;
                    }
                    danoTemp.GetComponent<Rigidbody2D>().AddForce(new Vector2(forcaX, 250));
                    Destroy(danoTemp, 1f);
                    GameObject knockTemp = Instantiate(knockForcePrefab, knockPosition.position, knockPosition.localRotation);
                    Destroy(knockTemp, 0.02f);

                    StartCoroutine("invuneravel");
                }

                break;
        }
    }
    void Flip()
    {
        olhandoEsquerda = !olhandoEsquerda; // inverte o valor da variavel
        float x = transform.localScale.x;
        x *= -1; // inverte o sinal do scale x
        transform.localScale = new Vector3(x, transform.localScale.y, transform.localScale.z);
        barrasVida.transform.localScale = new Vector3(x, barrasVida.transform.localScale.y, barrasVida.transform.localScale.z);

    }
    IEnumerator invuneravel()
    {
        
        sRender.color = characterColor[1];
        yield return new WaitForSeconds(0.2f);
        sRender.color = characterColor[0];
        yield return new WaitForSeconds(0.2f);
        sRender.color = characterColor[1];
        yield return new WaitForSeconds(0.2f);
        sRender.color = characterColor[0];
        yield return new WaitForSeconds(0.2f);
        sRender.color = characterColor[1];
        yield return new WaitForSeconds(0.2f);
        sRender.color = characterColor[0];
        getHit = false;
        barrasVida.SetActive(false);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class chest : MonoBehaviour
{
    private _GameController _GameController;
    private SpriteRenderer spriteRenderer;
    public Sprite[] imagemObjeto;
    public bool open;
    public GameObject[] loots;
    private bool gerouLoot;

    // Start is called before the first frame update
    void Start()
    {
        //FindObjectOfType vai achar apenas o primeiro item que tem esta tag

        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void interacao()
    {
        // abrindo e fechando o bau
        if(open == false)
        {
            open = true;
            spriteRenderer.sprite = imagemObjeto[1];
            StartCoroutine("gerarLoot");
            GetComponent<Collider2D>().enabled = false;
        }
    }

    IEnumerator gerarLoot()
    {
        gerouLoot = true;
        //controle de loot
        int qtdMoedas = Random.Range(1, 10);
        for (int l = 0; l < qtdMoedas; l++)
        {
            int rand = 0;
            int idLoot = 0;
            rand = Random.Range(0, 100);
            if(rand >= 75)
            {
                idLoot = 1;
            }
            GameObject lootTemp = Instantiate(loots[idLoot], transform.position, transform.localRotation);
            lootTemp.GetComponent<Rigidbody2D>().AddForce(new Vector2(Random.Range(-25, 25), 50));
            yield return new WaitForSeconds(0.1f);
        }
    }
}

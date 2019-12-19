using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class chest : MonoBehaviour
{
    private _GameController _GameController;
    private SpriteRenderer spriteRenderer;
    public Sprite[] imagemObjeto;
    public bool open;

    // Start is called before the first frame update
    void Start()
    {
        //FindObjectOfType vai achar apenas o primeiro item que tem esta tag
        _GameController = FindObjectOfType(typeof(_GameController)) as _GameController;

        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void interacao()
    {
        // abrindo e fechando o bau
        open = !open;
        switch (open)
        {
            case true:
                spriteRenderer.sprite = imagemObjeto[1];

                if(_GameController == null)
                {
                    _GameController = FindObjectOfType(typeof(_GameController)) as _GameController;
                }
                _GameController.teste++;
                break;
            case false:
                spriteRenderer.sprite = imagemObjeto[0];
                break;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class _GameController : MonoBehaviour
{
    public int teste;
    public string[] tiposDano;
    public GameObject[] fxDano; // animacao dano
    public GameObject fxMorte;

    public int gold; // responsavel armazena quantitade de ouro coletado

    public TextMeshProUGUI goldTxt;
    // Start is called before the first frame update
    private fade fade;

    void Start()
    {
        fade = FindObjectOfType(typeof(fade)) as fade;
        fade.fadeOut();
    }

    // Update is called once per frame
    void Update()
    {
        goldTxt.text = gold.ToString("N0");
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class controleDanoInimigo : MonoBehaviour
{
    private _GameController _GameController;
    public float[] ajusteDano; // sistema de resistencia/fraqueza contra determinado tipo de dano

    //knockback
    public GameObject knockForcePrefab; // forca repulsao
    public Transform knockPosition; // ponto de origem da força

    // Start is called before the first frame update
    void Start()
    {
        _GameController = FindObjectOfType(typeof(_GameController)) as _GameController;
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        switch (col.tag)
        {
            case "arma":

                infoArmas armasInfo = col.gameObject.GetComponent<infoArmas>();

                float danoArma = armasInfo.dano;
                int tipoDano = armasInfo.tipoDano;

                // danoTomado = danoArma +(danoArma * (ajusteDano[id] / 100))
                float danoTomado = danoArma + (danoArma * (ajusteDano[tipoDano] / 100));

                print("tomei: " + danoTomado + "de dano" + " do tipo" + _GameController.tiposDano[tipoDano]);

                GameObject knockTemp = Instantiate(knockForcePrefab, knockPosition.position, knockPosition.localRotation);
                Destroy(knockTemp, 0.03f);
                break;
        }
    }
}

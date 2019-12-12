using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager scoreManager;
    private int coinsAtual = 0;
    public Text coins;

    // Start is called before the first frame update
    void Awake()
    {
        if(scoreManager == null)
        {
            scoreManager = this;
        }else if(scoreManager != this)
        {
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
       
       
    }
    public void SetCoins()
    {
        coinsAtual++;
        Debug.Log(coinsAtual);
        coins.text = coinsAtual.ToString();

    }
    public int GetMoedas()
    {
        return coinsAtual;
    }
}

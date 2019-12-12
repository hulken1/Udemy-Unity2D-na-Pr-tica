using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTrigger : MonoBehaviour
{
    private Player playerScript;

    public AudioClip fxCoin;

    // Start is called before the first frame update
    void Start()
    {
        playerScript = GameObject.Find("Player").GetComponent<Player> ();
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Enemy"))
        {
            playerScript.DamagePlayer();
        }

        if (col.CompareTag("Water"))
        {
            playerScript.DamageWater();
        }
        if (col.CompareTag("Coin"))
        {
            ScoreManager.scoreManager.SetCoins();
            SoundManager.instance.PlaySound(fxCoin);
            Destroy(col.gameObject);
        }
    }
}

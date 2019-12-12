using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public AudioSource fxSource;
    public AudioSource musicSource;

    public static SoundManager instance = null;//static permite que outros scripts possam chamar suas funcoes
    // Start is called before the first frame update
    void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }

    public void PlaySound(AudioClip clip)
    {
        fxSource.clip = clip;
        fxSource.Play();
    }
}

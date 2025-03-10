using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioSingleton : MonoBehaviour
{
    public AudioSource audioSource;
    public static AudioSingleton main;
    // Start is called before the first frame update
    private void Awake()
    {
        main = this;
    }
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
    public void PlayAudio()
    {
        audioSource.Play();
    }
}

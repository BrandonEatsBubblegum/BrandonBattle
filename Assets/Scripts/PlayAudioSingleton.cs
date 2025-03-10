using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayAudioSingleton : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        AudioSingleton.main.PlayAudio();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

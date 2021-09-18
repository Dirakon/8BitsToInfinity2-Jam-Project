using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static float volume=1f;
    // Start is called before the first frame update
    public AudioSource clockSound,fallingSound,shortTick,bossVoice;
    public static SoundManager singleton;
    void Start()
    {
        
    }
    void Awake(){
        Debug.Log(volume);
        singleton=this;
        clockSound.volume *= volume;
        fallingSound.volume *= volume;
        shortTick.volume *= volume;
        bossVoice.volume *= volume;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

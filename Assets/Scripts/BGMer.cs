using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class BGMer : MonoBehaviour
{
    public static BGMer singeton;
    void Awake(){
        if (singeton != null){
            Destroy(gameObject);
            return;
        }
        singeton=this;
        DontDestroyOnLoad(gameObject);
    }
    public void AdjustToVolume(){
        SoundManager.volume = FindObjectOfType<Slider>().value;
        GetComponent<AudioSource>().volume = 0.5f*SoundManager.volume;
    }
    public void StartFirstLevel(){
        SceneManager.LoadScene("Level0",LoadSceneMode.Single);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class RestarterText : MonoBehaviour
{
    TextMeshPro text;
    void Awake(){
        text = GetComponent<TextMeshPro>();
    }
    public float speed = 1f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (House.singleton.gameOver)
            return;
        Color color = text.color;
        color.a-=Time.deltaTime*speed;
        if (color.a <= 0)
            Destroy(gameObject);
        text.color=color;
    }
}

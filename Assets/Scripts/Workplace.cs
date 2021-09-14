using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Workplace : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public MovingObject movingObject;
    public float distanceToActivate = 0.25f;
    public SpriteRenderer spriteRenderer;
    void Awake(){
spriteRenderer = GetComponent<SpriteRenderer>();
movingObject = GetComponent<MovingObject>();
    }

    bool someoneIsWorking = false;
    public bool allowsRandomRotation = false;
    public void Work(){
        someoneIsWorking=true;
    }

    // Update is called once per frame
    void Update()
    {
        if (House.singleton.gameOver)
            return;
        spriteRenderer.enabled = !someoneIsWorking;
        movingObject.movingAllowed = !someoneIsWorking;
        someoneIsWorking=false;
    }
}

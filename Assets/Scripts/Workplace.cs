using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Workplace : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void setArrowState(bool state){
        if (House.singleton.nextLevel != "Level2" || WorkList.singleton.tutorialCompleted)
            arrow.SetActive(state);
    }
    public GameObject arrow;
    public MovingObject movingObject;
    public Character whoIsWorking = null;
    public float distanceToActivate = 0.25f;
    public SpriteRenderer spriteRenderer;
    public BoxCollider2D boxCollider2D;
    void Awake(){
spriteRenderer = GetComponent<SpriteRenderer>();
movingObject = GetComponent<MovingObject>();
boxCollider2D = GetComponent<BoxCollider2D>();
arrow.SetActive(false);
    }

    void OnMouseDown(){
        if (whoIsWorking != null){
            whoIsWorking.OnMouseDown();
        }
    }

    bool someoneIsWorking = false;
    public bool allowsRandomRotation = false;
    public void Work(Character who){
        someoneIsWorking=true;
        whoIsWorking = who;
    }

    // Update is called once per frame
    void Update()
    {
        if (WorkList.singleton.tutorialCompleted){
            setArrowState(true);
        }
        if (House.singleton.gameOver)
            return;
        spriteRenderer.enabled = !someoneIsWorking;
        movingObject.movingAllowed = !someoneIsWorking;
        if (!someoneIsWorking){
            whoIsWorking=null;
            boxCollider2D.enabled=false;
        }else{
            boxCollider2D.enabled=true;
            someoneIsWorking=false;
        }
    }
}

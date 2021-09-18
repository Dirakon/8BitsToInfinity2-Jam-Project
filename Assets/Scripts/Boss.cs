using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    // Start is called before the first frame update
    public static Boss singleton;
    public bool acceptsOrders = true;
    public Animator animator;
    public GameObject[] bubbles;
    void Awake(){
        animator = GetComponent<Animator>();
        singleton = this;
    }
    public void OrderSent(){
        SoundManager.singleton.bossVoice.Play();
        acceptsOrders = false;
        animator.SetBool("Ordered", false);
        bubbles[Random.Range(0,bubbles.Length)].SetActive(true);
    }
    public void OrderFinished(){
        acceptsOrders = true;
        foreach (GameObject bubble in bubbles){
            bubble.SetActive(false);
        }
    }
    public void OrderToOrder(){
        if (!acceptsOrders)
            return;
        animator.SetBool("Ordered", true);
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

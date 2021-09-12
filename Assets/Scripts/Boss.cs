using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    // Start is called before the first frame update
    public static Boss singleton;
    public bool acceptsOrders = true;
    public Animator animator;
    void Awake(){
        animator = GetComponent<Animator>();
        singleton = this;
    }
    public void OrderSent(){
        acceptsOrders = false;
        animator.SetBool("Ordered", false);
    }
    public void OrderFinished(){
        acceptsOrders = true;
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

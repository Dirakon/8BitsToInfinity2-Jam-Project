using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Commander : MonoBehaviour
{
    // Start is called before the first frame update
    public static Commander singleton;
    public Character chosenOne = null;
    void Start()
    {
        
    }
    void Awake(){
        singleton=this;
    }
    public void changeChosenOne(Character newChosenOne){
        if (chosenOne != null)
            chosenOne.getUnchosen();
        chosenOne=newChosenOne;
        chosenOne.getChosen();
    }

    // Update is called once per frame
    void Update()
    {
        if (House.singleton.gameOver)
            return;
        if (Input.GetMouseButtonDown(1) && chosenOne != null){
            Boss.singleton.OrderToOrder();

            Vector3 mousePos = Input.mousePosition;
            mousePos.z = Camera.main.nearClipPlane;
            Vector3 worldPosition = Camera.main.ScreenToWorldPoint(mousePos);
            Vector3 housePosition = worldPosition - House.singleton.transform.position;
            float projection = House.singleton.GetProjection(housePosition);
            chosenOne.TryAchieveGoal(House.singleton.capProjection(projection,projection>0?chosenOne.rightSideLeeway:chosenOne.leftSideLeeway));
        }
    }
}

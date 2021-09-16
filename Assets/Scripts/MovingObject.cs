using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingObject : MonoBehaviour
{
    // Start is called before the first frame update
    public float speedModifier = 1f;
    public float rightSideBaseSize = 0.2f;
    public float leftSideBaseSize = -0.2f;

    public bool movingAllowed = true;
    public static List<MovingObject> movingObjects;
    void Awake(){
        if (movingObjects == null)
            movingObjects = new List<MovingObject>();
        movingObjects.Add(this);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

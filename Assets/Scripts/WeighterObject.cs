using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeighterObject : MonoBehaviour
{
    // Start is called before the first frame update
    public float relativeMass = 1f;
    public static List<WeighterObject> weighterObjects;
    void Awake(){
        if (weighterObjects == null)
            weighterObjects = new List<WeighterObject>();
        weighterObjects.Add(this);
    }
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

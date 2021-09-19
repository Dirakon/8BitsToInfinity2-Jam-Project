using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatedLogo : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    float tRot = 1;
    float tZoom = 1;
    float movZoom,movRot;
    Vector3 startRot,goalRot;
    Vector3 startZoom,goalZoom;
    public float minZoom = 0.2f,maxZoom =1.5f;
    public float zoomSpeed=1f,rotSpeed=1f;
    public float minRot = -30,maxRot = 30;
    void Update()
    {
        #if !UNITY_WEBGL
        if (Input.GetKeyDown(KeyCode.Escape)){
            Application.Quit();
        }
        #endif
        if (tRot >= 1){
            float rotValue = Random.Range(minRot,maxRot);
            goalRot = new Vector3(0,0,rotValue);
            startRot =transform.rotation.eulerAngles; 
            float curRot = transform.rotation.eulerAngles.z;
            while (curRot > 90)
                curRot-=360;
            while (curRot < -90)
                curRot+=360;
            movRot = rotSpeed/Mathf.Abs(rotValue-curRot);
            tRot=0;
        }
        if (tZoom >= 1){
            tZoom = 0;
            float zoomValue = Random.Range(minZoom,maxZoom);
            startZoom = transform.localScale;
            goalZoom = zoomValue*Vector3.one;
            movZoom = zoomSpeed/Mathf.Abs(zoomSpeed-startZoom.x);

        }
        tRot+=movRot*Time.deltaTime;
        if (tRot > 1)
            tRot=1;
        transform.rotation = Quaternion.Lerp(Quaternion.Euler(startRot),Quaternion.Euler(goalRot),tRot);
        tZoom+=movZoom*Time.deltaTime;
        if (tZoom > 1)
            tZoom=1;
        transform.localScale = Vector3.Lerp(startZoom,goalZoom,tZoom);
    }
}

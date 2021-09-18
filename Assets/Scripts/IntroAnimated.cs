using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class IntroAnimated : MonoBehaviour
{
    // Start is called before the first frame update
    Vector3 desiredBossPosition = new Vector3(-5.89f,2.36f,-10);
    Vector3 desiredHousePosition = new Vector3(-0.74f,-0.88f,0);
    public float houseSpeed = 1f,bossSpeed = 1f;
    IEnumerator descendHouse(){
        float t =0;
        Vector3 start = House.singleton.transform.position;
        while (t < 1){
            t+=Time.deltaTime*houseSpeed;
            if (t > 1)
                 t= 1;
            House.singleton.transform.position = Vector3.Lerp(start,desiredHousePosition,t);
            yield return null;
        }
        yield return new WaitForSeconds(1f);
        yield return descendBoss();
    }
    IEnumerator descendBoss(){
        float t =0;
        Vector3 start = Boss.singleton.transform.position;
        while (t < 1){
            t+=Time.deltaTime*bossSpeed;
            if (t > 1)
                 t= 1;
            Boss.singleton.transform.position = Vector3.Lerp(start,desiredBossPosition,t);
            yield return null;
  }
        yield return new WaitForSeconds(1f);
        WeighterObject.weighterObjects = null;
        MovingObject.movingObjects = null;
        SceneManager.LoadScene("Level1",LoadSceneMode.Single);
    }
    void Start()
    {
        StartCoroutine(descendHouse());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

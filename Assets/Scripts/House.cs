using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class House : MonoBehaviour
{
    // Start is called before the first frame update
    void Awake(){
        singleton=this;
        gameOver = false;
    }
    void Start()
    {
    }
    public bool gameOver = false;
    public float gameOverDistance = 30f;
    public float gameOverSpeed = 5f;
    public float horizontalRadius;
    public IEnumerator GameOver(Vector3 direction){
        gameOver = true;
        float t= 0;
        Vector3 start = transform.position;
        int sign = direction == transform.right? -1 : 1;
        while (t < 1)
        {
            t += (gameOverSpeed * Time.deltaTime);
            if (t > 1)
                t = 1;
            Vector3 angle = transform.rotation.eulerAngles;
            angle.z+=sign*200f*massMultiplier*Time.deltaTime;
            transform.rotation = Quaternion.Euler(angle);

            transform.localPosition = Vector3.Lerp(start, transform.position + gameOverDistance*sign*(-transform.right), t);
            yield return new WaitForEndOfFrame();
        }
        yield return new WaitForSeconds(2f);
        WeighterObject.weighterObjects=new List<WeighterObject>();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex,LoadSceneMode.Single);
    }
    public static House singleton;
    public float massMultiplier;
    public void FixedUpdate(){

    }
    public float GetProjection(Vector3 vectorToProject){
        return GetProjection(vectorToProject,false);
    }
    public float GetProjection(Vector3 vectorToProject, bool debug){
        float ans = Vector3.Dot(transform.right,vectorToProject);
        if (debug)
            Debug.Log(ans);
        return ans;
    }
    // Update is called once per frame
    void Update()
    {
        if (gameOver)
            return;
        float angularVelocity = 0f;
        foreach(WeighterObject weighterObject in WeighterObject.weighterObjects){
            Vector3 difference = weighterObject.transform.position-transform.position;
            angularVelocity+=GetProjection(difference)*weighterObject.relativeMass;
        }
        Vector3 angle = transform.rotation.eulerAngles;
        angle.z-=angularVelocity*massMultiplier*Time.deltaTime;
        transform.rotation = Quaternion.Euler(angle);
        while (angle.z>=90)
            angle.z-=180;
        while (angle.z<-90)
            angle.z+=180;
        if (Mathf.Abs(angle.z) >= 35)
            StartCoroutine(GameOver(angle.z < 0? transform.right : -transform.right));
    }
}

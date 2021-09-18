using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clock : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }
    void Awake(){
        singleton=this;
    }
    float nextCheckpoint = -1;
    bool deathIncoming = false;
    public void setTime(float timeInRealSeconds){
        float minutes = timeInRealSeconds%House.inGameHour;
        if (nextCheckpoint == -1){
            nextCheckpoint = timeInRealSeconds - minutes;
        }
        if (!deathIncoming && timeInRealSeconds < House.inGameHour){
            deathIncoming = true;
            nextCheckpoint = -999;
            SoundManager.singleton.clockSound.Play();
        }
        if (timeInRealSeconds <= nextCheckpoint){
            nextCheckpoint = timeInRealSeconds - House.inGameHour;
            nextCheckpoint = timeInRealSeconds - minutes;
            SoundManager.singleton.shortTick.Play();
        }
        float minuteValue01 = minutes/House.inGameHour;
        float hourValue01 = (timeInRealSeconds%(House.inGameHour*12))/(12*House.inGameHour);
        hoursToRotate.transform.rotation = Quaternion.Euler(new Vector3(0,0,-hourValue01*360));
        minutesToRotate.transform.rotation = Quaternion.Euler(new Vector3(0,0,-minuteValue01*360));
    }
    public GameObject minutesToRotate,hoursToRotate;
    public static Clock singleton;

    // Update is called once per frame
    void Update()
    {
        
    }
}

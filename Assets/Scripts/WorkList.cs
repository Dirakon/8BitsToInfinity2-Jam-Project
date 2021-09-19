using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class WorkList : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

        foreach(WeighterObject weighterObject in WeighterObject.weighterObjects){
            Character character =  weighterObject.gameObject.GetComponent<Character>();
            if (character != null && character.workplace != null){
            
                for(int id = 0;;id++){
                    if (workplaces[id]==character.workplace){
                        characterToHisId.Add(character,id);
                        break;
                    }
                }
            }
        }
        if (House.singleton.nextLevel == "Level2")
        {
            var p = characterToHisId.Keys;
            foreach(var a in p){
                a.workplace= null;
            }
        }
    }
    void Awake()
    {
        singleton = this;
        achieved = new float[phrases.Length];
    }
    public Workplace[] workplaces;
    Dictionary<Character, int> characterToHisId = new Dictionary<Character, int>();
    public string[] phrases;
    public int[] goals;
    float[] achieved;
    public static WorkList singleton;
    public TextMeshPro text;
    public bool tutorialCompleted = false;
    // Update is called once per frame
    bool stopGameOverOnce = true;
    void Update()
    {
        #if !UNITY_WEBGL
        if (Input.GetKeyDown(KeyCode.Escape)){
            Application.Quit();
        }
        #endif
        if (!tutorialCompleted &&  House.singleton.nextLevel == "Level2"){
            // First level tutorial, here we go!
            if (Commander.singleton.chosenOne == null){
                House.singleton.gameOver = true;
                text.text = "Click on the manager to choose him!";
            }else{
                text.text = "Send manager with right-click to balance the office!";
                if (stopGameOverOnce)
                    House.singleton.gameOver = false;
                stopGameOverOnce = false;
            }
            if (House.singleton.transform.rotation.eulerAngles.z > 0 && House.singleton.transform.rotation.eulerAngles.z < 100){
                tutorialCompleted=true;

            var p = characterToHisId.Keys;
            foreach(var a in p){
                a.workplace= workplaces[0];
            }
            }


            


            return;
        }
        if (House.singleton.gameOver){
            text.enabled=false;
            return;
        }
        bool missionComplete = true;
        string newText = "";
        foreach(Character character in characterToHisId.Keys){
            int id=0;
            characterToHisId.TryGetValue(character, out id);
            if (character.previouslyWorked){
                achieved[id]+=Time.deltaTime;
            }
            int realAchieved = (int)(achieved[id]/House.inGameHour);
            if (realAchieved < goals[id])
                missionComplete=false;
            else{
                newText += "<mark=#2E933C80>";
            }
            newText += phrases[id] + "(" + realAchieved.ToString() + " / " + goals[id].ToString() + ")\n";
            if (realAchieved >= goals[id]){
                newText+="</mark>";
            }
        }
        text.text = newText;
        if (missionComplete){
            // TODO something
            House.singleton.LoadNextScene();
        }
    }
}

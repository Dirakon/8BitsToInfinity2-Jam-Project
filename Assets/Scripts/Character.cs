using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public Animator animator;
    public SpriteRenderer spriteRenderer;

    public AudioSource walkSound;
    public AudioSource workSound;
    // Start is called before the first frame update
    void Start()
    {
    }
    public float rangeToIgnore = 0.25f;
    void Awake()
    {
        walkSound.volume*=SoundManager.volume;
        if (workSound != null)
            workSound.volume*=SoundManager.volume;
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    public void OnMouseDown()
    {
        if (House.singleton.gameOver && House.singleton.nextLevel !="Level2")
            return;

        Commander.singleton.changeChosenOne(this);
    }
    public float rightSideLeeway = 0f;
    public float leftSideLeeway = 0f;
    int id = -1;
    public void TryAchieveGoal(float projection)
    {
        float ourProjection = House.singleton.GetProjection(transform.position - House.singleton.transform.position);
        projection -= ourProjection;
        if (Mathf.Abs(projection) < rangeToIgnore)
            return;
        goalAchieved = true;
        id++;
        if (projection > 0)
        {
            spriteRenderer.flipX = true;
        }
        else
        {
            spriteRenderer.flipX = false;
        }
        StartCoroutine(goTo(transform.localPosition + Vector3.right * projection, id));
    }
    bool goalAchieved = true;
    public Workplace workplace = null;
    public float speed = 1f;
    public void Rotate()
    {
        if (House.singleton.gameOver)
            return;
        if (!workplace.allowsRandomRotation || !previouslyWorked)
            return;
        if (Random.Range(0, 2) == 0)
        {
            spriteRenderer.flipX = true;
        }
        else
        {
            spriteRenderer.flipX = false;
        }
    }
    public void getChosen(){
        if (workplace != null)
            workplace.setArrowState(true);
    }
    public void getUnchosen(){
        if (workplace != null)
            workplace.setArrowState(false);
    }
    IEnumerator goTo(Vector3 goal, int localId)
    {
        walkSound.Play();
        goalAchieved = false;
        Vector3 start = transform.localPosition;
        goal.z = start.z;
        float dist = (start - goal).magnitude;
        float t = 0;
        while (t < 1)
        {
            if (id != localId || House.singleton.gameOver){
                break;
            }
            t += (speed * Time.deltaTime) / dist;
            if (t > 1)
                t = 1;
            transform.localPosition = Vector3.Lerp(start, goal, t);
            yield return new WaitForEndOfFrame();
        }
        if (id == localId){
            goalAchieved = true;
            walkSound.Stop();
        }

    }
    // Update is called once per frame
    public bool previouslyWorked = false;
    void Update()
    {
        if (House.singleton.gameOver)
            return;
        if (!goalAchieved)
        {
            //Walkin'
            animator.SetBool("isWorking", false);
            animator.SetBool("isRunning", true);
            if (previouslyWorked && workSound != null)
                workSound.Stop();
            previouslyWorked = false;
        }
        else if (workplace != null && (workplace.distanceToActivate > ((Vector2)transform.position - (Vector2)workplace.transform.position).magnitude))
        {

            if (previouslyWorked == false){
                if (workSound != null)
                    workSound.Play();
                float divergence = 0f;
                divergence = House.singleton.GetProjection(transform.position-House.singleton.transform.position) - House.singleton.GetProjection(workplace.transform.position-House.singleton.transform.position) ;
                workplace.transform.position += House.singleton.transform.right*divergence;
            }
            //Workin'
            workplace.Work(this);

            animator.SetBool("isWorking", true);
            animator.SetBool("isRunning", false);
            previouslyWorked = true;
        }
        else
        {
            //Chillin'
            animator.SetBool("isWorking", false);
            animator.SetBool("isRunning", false);
            if (previouslyWorked && workSound != null)
                workSound.Stop();
            previouslyWorked = false;
        }
    }
}

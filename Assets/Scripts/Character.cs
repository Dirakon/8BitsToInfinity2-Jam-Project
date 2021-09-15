using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public Animator animator;
    public SpriteRenderer spriteRenderer;
    // Start is called before the first frame update
    void Start()
    {
    }
    public float rangeToIgnore = 0.25f;
    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    public void OnMouseDown()
    {
        if (House.singleton.gameOver)
            return;
        Commander.singleton.changeChosenOne(this);
    }
    int id = -1;
    public void TryAchieveGoal(float projection)
    {
        id++;
        goalAchieved = true;
        float ourProjection = House.singleton.GetProjection(transform.position);
        projection -= ourProjection;
        if (Mathf.Abs(projection) < rangeToIgnore)
            return;
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
    IEnumerator goTo(Vector3 goal, int localId)
    {
        goalAchieved = false;
        Vector3 start = transform.localPosition;
        goal.z = start.z;
        float dist = (start - goal).magnitude;
        float t = 0;
        while (t < 1)
        {
            if (id != localId)
                break;
            if (House.singleton.gameOver)
                break;
            t += (speed * Time.deltaTime) / dist;
            if (t > 1)
                t = 1;
            transform.localPosition = Vector3.Lerp(start, goal, t);
            yield return new WaitForEndOfFrame();
        }
        if (id == localId)
            goalAchieved = true;

    }
    // Update is called once per frame\
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
            previouslyWorked = false;
        }
        else if (workplace != null && (workplace.distanceToActivate > ((Vector2)transform.position - (Vector2)workplace.transform.position).magnitude))
        {
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
            previouslyWorked = false;
        }
    }
}

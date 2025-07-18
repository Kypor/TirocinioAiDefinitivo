using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

public class CharacterBehaviour : MonoBehaviour
{
    [System.Serializable]
    public struct Actions
    {
        public string sentence;
        public string verb;
        public string noun;
    }

    public string currentVerb { get; private set; }
    public string currentNoun { get; private set; }
    private enum State
    {
        Idle,
        Moving,
        Greet,
        Puzzled,
        GetObject,
        BringObjectsToCheckout,
        Dance,
        Eat,
        Kneel,
        Sleep,
        Pet
    }

    [SerializeField] GameObject shoes;

    QuestManager questManager;

    [SerializeField] private float objectDistance = 3.0f;
    [SerializeField] Transform grabbingPoint;
    [SerializeField] Transform defaultPosition;
    [SerializeField] Transform eatingPoint;
    [SerializeField] Transform sleepPosition;
    public bool catInteraction;

    private Camera cam;

    private Animator animator;



    [Header("Robot list of actions")]
    public List<Actions> actionsList;
    private State state;
    private GameObject goalObject;
    [HideInInspector]
    public List<string> sentences; // Robot list of sentences (actions)

    NavMeshAgent agent;
    void Start()
    {
        cam = FindAnyObjectByType<Camera>();
        animator = GetComponentInChildren<Animator>();
        agent = GetComponent<NavMeshAgent>();
        questManager = FindAnyObjectByType<QuestManager>();
    }

    // Update is called once per frame
    void Update()
    {
        animator.SetFloat("Speed", agent.velocity.magnitude);
        switch (state)
        {
            default:
            case State.Idle:

                break;
            case State.Moving:
                Debug.Log("Moving");
                agent.SetDestination(goalObject.transform.position);
                if (Vector3.Distance(transform.position, goalObject.transform.position) < objectDistance)
                {
                    state = State.Idle;
                }
                break;
            case State.Greet:
                Debug.Log("greeting");
                Greeting(goalObject);
                break;
            case State.Puzzled:
                Debug.Log("Puzzled");
                animator.SetBool("Puzzle", true);
                if (this.animator.GetCurrentAnimatorStateInfo(0).IsName("Puzzle"))
                {
                    // Avoid any reload.
                    Debug.Log("Finita");
                    state = State.Idle;
                    animator.SetBool("Puzzle", false);
                }
                break;
            case State.GetObject:
                agent.SetDestination(goalObject.transform.position);
                if (Vector3.Distance(transform.position, goalObject.transform.position) < objectDistance)
                {
                    Grab(goalObject);
                    state = State.Idle;
                }
                break;
            case State.BringObjectsToCheckout:
                agent.SetDestination(goalObject.transform.position);
                if (Vector3.Distance(transform.position, goalObject.transform.position) <= 2f)
                {
                    BringObjectsToCheckout(goalObject);
                    state = State.Idle;
                }
                break;
            case State.Dance:
                animator.SetBool("Dancing", true);
                if (this.animator.GetCurrentAnimatorStateInfo(0).IsName("Dance"))
                {
                    // Avoid any reload.
                    Debug.Log("Finita");
                    state = State.Idle;
                    animator.SetBool("Dancing", false);
                }
                break;
            case State.Eat:
                agent.SetDestination(goalObject.transform.position);
                if (Vector3.Distance(transform.position, goalObject.transform.position) < objectDistance)
                {

                    var goalObjectRb = goalObject.GetComponent<Rigidbody>();
                    goalObjectRb.isKinematic = true;
                    goalObject.transform.parent = eatingPoint;
                    goalObject.transform.position = eatingPoint.position;
                    goalObject.transform.rotation = eatingPoint.rotation;

                    animator.SetBool("Eating", true);
                    // state = State.Idle;
                    if (this.animator.GetCurrentAnimatorStateInfo(0).IsName("Eat"))
                    {

                        state = State.Idle;
                        animator.SetBool("Eating", false);
                        StartCoroutine(DisableGameObject(goalObject));

                    }

                }
                break;
            case State.Kneel:
                animator.SetBool("Kneeling", true);
                if (this.animator.GetCurrentAnimatorStateInfo(0).IsName("Kneel"))
                {

                    // Avoid any reload.
                    Debug.Log("Finita");
                    state = State.Idle;
                    animator.SetBool("Kneeling", false);
                    //shoes.SetActive(false);
                    StartCoroutine(DisableGameObject(shoes));
                }
                break;
            case State.Sleep:
                agent.SetDestination(goalObject.transform.position);
                if (Vector3.Distance(transform.position, goalObject.transform.position) < objectDistance)
                {
                    transform.position = sleepPosition.position;
                    animator.SetBool("Sleeping", true);
                }
                break;
            case State.Pet:
                catInteraction = true;
                agent.SetDestination(goalObject.transform.position);
                if (Vector3.Distance(transform.position, goalObject.transform.position) < objectDistance)
                {
                    animator.SetBool("Kneeling", true);
                    if (this.animator.GetCurrentAnimatorStateInfo(0).IsName("Kneel"))
                    {

                        state = State.Idle;
                        animator.SetBool("Kneeling", false);
                        catInteraction = false;

                    }
                }
                break;


        }
    }
    /// <summary>
    /// Utility function: Given the results of HuggingFaceAPI, select the State with the highest score
    /// </summary>
    /// <param name="maxValue">Value of the option with the highest score</param>
    /// <param name="maxIndex">Index of the option with the highest score</param>
    public void Utility(float maxScore, int maxScoreIndex)
    {
        // First we check that the score is > of 0.2, otherwise we let our agent perplexed;
        // This way we can handle strange input text (for instance if we write "Go see the dog!" the agent will be puzzled).
        if (maxScore < 0.30f)
        {
            state = State.Puzzled;
        }
        else
        {
            // Get the verb and noun (if there is one)
            goalObject = GameObject.Find(actionsList[maxScoreIndex].noun);

            string verb = actionsList[maxScoreIndex].verb;

            currentVerb = actionsList[maxScoreIndex].verb;
            currentNoun = actionsList[maxScoreIndex].noun;

            if (questManager != null && questManager.currentQuest != null)
            {
                if (verb.ToLower() == questManager.currentQuest.requiredVerb.ToLower())
                {
                    state = (State)System.Enum.Parse(typeof(State), verb, true);
                }
                else
                {
                    state = State.Puzzled;
                }
            }
            else
                state = (State)System.Enum.Parse(typeof(State), verb, true);
        }

        // Set the Robot State == verb

    }
    private void Greeting(GameObject gameObject)
    {
        Vector3 direction = gameObject.transform.position - transform.position;
        direction.y = 0; // Per evitare inclinazioni verticali
        Quaternion targetRotation = Quaternion.LookRotation(direction.normalized);
        if (Vector3.Angle(transform.forward, direction) > 10f)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime);
        }
        else
        {
            animator.SetBool("Bowing", true);
            if (this.animator.GetCurrentAnimatorStateInfo(0).IsName("Bow"))
            {
                // Avoid any reload.
                Debug.Log("Finita");
                state = State.Idle;
                animator.SetBool("Bowing", false);
            }
            //animator.SetBool("Bowing", false);
            Debug.Log("animazione");
        }
    }

    private void BringObjectsToCheckout(GameObject gameObject)
    {
        Transform[] childArray = grabbingPoint.GetComponentsInChildren<Transform>();
        for (int i = 1; i < childArray.Length; i++)
        {
            Destroy(childArray[i].gameObject);
        }
        gameObject.transform.parent = null;
    }
    private void Grab(GameObject gameObject)
    {
        var rb = gameObject.GetComponent<Rigidbody>();

        rb.useGravity = false;
        rb.isKinematic = true;
        Vector3 spawnPosition = grabbingPoint.position + Vector3.up * (grabbingPoint.childCount * 0.5f);
        gameObject.transform.position = spawnPosition;
        gameObject.transform.parent = grabbingPoint;
    }

    private void Drop(GameObject gameObject)
    {
        gameObject.transform.parent = null;
        var rb = gameObject.GetComponent<Rigidbody>();

        rb.useGravity = true;

        //gameObject.transform.position = defaultPosition.position;
    }

    private IEnumerator DisableGameObject(GameObject gameObject)
    {

        yield return new WaitForSeconds(3);
        gameObject.SetActive(false);
    }






    // public void OnOrderGiven(string prompt)
    // {
    //     Tuple<int, float> tuple_ = sentenceSimilarity.RankSimilarityScores(prompt, sentencesArray);
    //     Utility(tuple_.Item2, tuple_.Item1);
    // }
}

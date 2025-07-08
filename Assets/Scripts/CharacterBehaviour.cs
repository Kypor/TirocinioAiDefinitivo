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
    private enum State
    {
        Idle,
        Moving,
        Greet,
        Puzzled,
        GetObject,
        BringObjectsToCheckout
    }

    [SerializeField] private float objectDistance = 3.0f;
    [SerializeField] Transform grabbingPoint;
    [SerializeField] Transform defaultPosition;

    bool inHand = false;

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
        animator = GetComponentInChildren<Animator>();
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        switch (state)
        {
            default:
            case State.Idle:
                //Debug.Log("idle");
                animator.SetFloat("Speed", 0f);
                break;
            case State.Moving:
                Debug.Log("Moving");
                animator.SetBool("isWalking", true);
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
                break;
            case State.GetObject:
                agent.SetDestination(goalObject.transform.position);
                animator.SetFloat("Speed", 2f);
                if (Vector3.Distance(transform.position, goalObject.transform.position) < objectDistance)
                {
                    Grab(goalObject);
                    state = State.Idle;
                }
                break;
            case State.BringObjectsToCheckout:
                agent.SetDestination(goalObject.transform.position);
                animator.SetFloat("Speed", 2f);
                if (Vector3.Distance(transform.position, goalObject.transform.position) <= 2f)
                {
                    BringObjectsToCheckout(goalObject);
                    state = State.Idle;
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
        if (maxScore < 0.20f)
        {
            state = State.Puzzled;
        }
        else
        {
            // Get the verb and noun (if there is one)
            goalObject = GameObject.Find(actionsList[maxScoreIndex].noun);

            string verb = actionsList[maxScoreIndex].verb;

            // Set the Robot State == verb
            state = (State)System.Enum.Parse(typeof(State), verb, true);
        }
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
        inHand = true;
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
    // public void OnOrderGiven(string prompt)
    // {
    //     Tuple<int, float> tuple_ = sentenceSimilarity.RankSimilarityScores(prompt, sentencesArray);
    //     Utility(tuple_.Item2, tuple_.Item1);
    // }
}

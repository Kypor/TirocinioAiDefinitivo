using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.AI;

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
        Puzzled,
        Attacking
    }


    [Header("Robot list of actions")]
    public List<Actions> actionsList;
    private State state;
    [HideInInspector]
    public List<string> sentences; // Robot list of sentences (actions)
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        switch (state)
        {
            default:
            case State.Idle:
                Debug.Log("idle");
                break;
            case State.Moving:
                Debug.Log("Moving");
                break;
            case State.Puzzled:
                Debug.Log("Puzzled");
                break;
            case State.Attacking:
                Debug.Log("Attacking");
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
            //goalObject = GameObject.Find(actionsList[maxScoreIndex].noun);

            string verb = actionsList[maxScoreIndex].verb;

            // Set the Robot State == verb
            state = (State)System.Enum.Parse(typeof(State), verb, true);
        }
    }
    // public void OnOrderGiven(string prompt)
    // {
    //     Tuple<int, float> tuple_ = sentenceSimilarity.RankSimilarityScores(prompt, sentencesArray);
    //     Utility(tuple_.Item2, tuple_.Item1);
    // }
}

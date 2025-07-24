using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class CatBehaviour : MonoBehaviour
{
    private NavMeshAgent agent;
    private CharacterBehaviour robot;
    public float secondsToStop;
    public float secondsToRestart;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        robot = FindAnyObjectByType<CharacterBehaviour>();
    }

    void Update()
    {
        if (robot.catInteraction)
        {
            StopMovement();
            if (!robot.catInteraction)
            {
                StartCoroutine(ResumeAfterSeconds(secondsToRestart));
            }
        }
        
    }

    void StopMovement()
    {
        agent.isStopped = true;
        
        Debug.Log("[Gatto] Fermato. Attesa 3 secondi prima di ripartire.");
        StartCoroutine(ResumeAfterSeconds(secondsToStop));
    }

    IEnumerator ResumeAfterSeconds(float seconds)
    {
        yield return new WaitForSeconds(seconds);

        agent.isStopped = false;


    }
}

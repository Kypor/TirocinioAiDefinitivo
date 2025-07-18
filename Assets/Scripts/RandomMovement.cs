using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI; //important

//if you use this code you are contractually obligated to like the YT video
public class RandomMovement : MonoBehaviour //don't forget to change the script name if you haven't
{
    public NavMeshAgent agent;
    private CharacterBehaviour characterBehaviour;
    private Animator animator;
    public float range; //radius of sphere

    public Transform centrePoint; //centre of the area the agent wants to move around in
    //instead of centrePoint you can set it as the transform of the agent if you don't care about a specific area

    [Header("Pause Settings")]
    public float minPauseTime = 2f; // tempo minimo di pausa
    public float maxPauseTime = 5f; // tempo massimo di pausa
    [Range(0f, 1f)]
    public float pauseChance = 0.3f; // probabilità di pausa (30%)

    private bool isPaused = false;
    private float pauseTimer = 0f;
    public float customTimer = 5f;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        characterBehaviour = FindAnyObjectByType<CharacterBehaviour>();
    }


     void Update()
    {
        animator.SetFloat("Vert", agent.velocity.magnitude);

        if (characterBehaviour.catInteraction == true)
        {
            Debug.Log("Gatto fermo");
            StartPause(customTimer);
        }
        else
        {
            
            // Se è in pausa, conta il tempo
            if (isPaused)
            {
                
                pauseTimer -= Time.deltaTime;
                Debug.Log(pauseTimer);
                if (pauseTimer <= 0f)
                {
                    isPaused = false;
                    // Riprendi il movimento trovando una nuova destinazione
                    FindNewDestination();
                }
                return;
            }

            if (agent.remainingDistance <= agent.stoppingDistance) //done with path
            {
                // Controlla se deve fare una pausa
                if (Random.value < pauseChance)
                {
                    StartPause();
                }
                else
                {
                    FindNewDestination();
                }
            }
        }

    }

    void StartPause(float? customDuration = null)
    {
        isPaused = true;
        
        // Se viene passato un valore personalizzato, usalo, altrimenti usa random
        if (customDuration.HasValue)
        {
            pauseTimer = customDuration.Value;
        }
        else
        {
            pauseTimer = Random.Range(minPauseTime, maxPauseTime);
        }
        
        agent.SetDestination(transform.position); // Ferma l'agent
    }

    void FindNewDestination()
    {
        Vector3 point;
        if (RandomPoint(centrePoint.position, range, out point)) //pass in our centre point and radius of area
        {
            Debug.DrawRay(point, Vector3.up, Color.blue, 1.0f); //so you can see with gizmos
            agent.SetDestination(point);
        }
    }

    bool RandomPoint(Vector3 center, float range, out Vector3 result)
    {
        Vector3 randomPoint = center + Random.insideUnitSphere * range; //random point in a sphere 
        NavMeshHit hit;
        if (NavMesh.SamplePosition(randomPoint, out hit, 1.0f, NavMesh.AllAreas)) //documentation: https://docs.unity3d.com/ScriptReference/AI.NavMesh.SamplePosition.html
        {
            //the 1.0f is the max distance from the random point to a point on the navmesh, might want to increase if range is big
            //or add a for loop like in the documentation
            result = hit.position;
            return true;
        }

        result = Vector3.zero;
        return false;
    }
}
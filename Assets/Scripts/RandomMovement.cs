using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class RandomMovement : MonoBehaviour
{
    public NavMeshAgent agent;
    private Animator animator;
    public float range;
    public Transform centrePoint;

    public float waitTimeMin = 1f;
    public float waitTimeMax = 5f;

    private bool isWaiting = false;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        animator.SetFloat("Vert", agent.velocity.magnitude);

        if (!isWaiting && agent.remainingDistance <= agent.stoppingDistance && !agent.pathPending)
        {
            StartCoroutine(WaitAndMoveUntilSuccess());
        }
    }

    IEnumerator WaitAndMoveUntilSuccess()
    {
        isWaiting = true;

        float waitTime = Random.Range(waitTimeMin, waitTimeMax);
        Debug.Log($"[RandomMovement] Attendo {waitTime:F2} secondi prima di cercare una nuova posizione...");
        yield return new WaitForSeconds(waitTime);

        Vector3 point;
        int attempts = 0;

        // Continua a cercare finché non trova un punto valido
        while (!RandomPoint(centrePoint.position, range, out point))
        {
            attempts++;
            yield return null; // aspetta un frame per non bloccare il gioco
        }

        //Debug.Log($"[RandomMovement] Punto trovato dopo {attempts} tentativi: {point}");
        //Debug.DrawRay(point, Vector3.up, Color.green, 1.0f);
        agent.SetDestination(point);

        isWaiting = false;
    }

    bool RandomPoint(Vector3 center, float range, out Vector3 result)
    {
        Vector3 randomPoint = center + Random.insideUnitSphere * range;

        NavMeshHit hit;
        if (NavMesh.SamplePosition(randomPoint, out hit, 2.0f, NavMesh.AllAreas))
        {
            result = hit.position;
            return true;
        }

        result = Vector3.zero;
        return false;
    }
}

using UnityEngine;
using UnityEngine.AI;

// Attach this script to your enemy game object
public class EnemyChase : MonoBehaviour
{
    public Transform player;        // Reference to the player
    public NavMeshAgent agent;      // NavMesh Agent component
    public Animator animator;       // Animator component
    public float chaseDistance = 10f; // Distance at which enemy starts chasing

    void Start()
    {
        // Ensure the agent and animator are assigned
        if (agent == null)
            agent = GetComponent<NavMeshAgent>();
        if (animator == null)
            animator = GetComponent<Animator>();
    }

    void Update()
    {
        float distance = Vector3.Distance(player.position, transform.position);

        if (distance <= chaseDistance)
        {
            // Enemy starts chasing the player
            agent.SetDestination(player.position);
            
            // Set animation to walking/running
            animator.SetBool("isWalking", true);
        }
        else
        {
            // Stop chasing
            agent.ResetPath();
            animator.SetBool("isWalking", false);
        }
    }
}
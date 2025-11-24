using UnityEngine;
using UnityEngine.SceneManagement; // Needed for scene reloading

public class PlayerDeath : MonoBehaviour
{
    [Header("Collision Settings")]
    [Tooltip("Tag of the object that will kill the player.")]
    public string deadlyTag = "Deadly";

    [Header("Death Settings")]
    [Tooltip("Delay before reloading the scene after death.")]
    public float deathDelay = 0.5f;

    private bool isDead = false;

    // Called when a collision with a non-trigger collider happens
    private void OnCollisionEnter(Collision collision)
    {
        if (!isDead && collision.gameObject.CompareTag(deadlyTag))
        {
            Die();
        }
    }

    // Called when a collision with a trigger collider happens
    private void OnTriggerEnter(Collider other)
    {
        if (!isDead && other.CompareTag(deadlyTag))
        {
            Die();
        }
    }

    // Handles player death
    private void Die()
    {
        isDead = true;
        Debug.Log("Player has died!");

        // Optional: disable player controls or play animation
        GetComponent<Rigidbody>().linearVelocity = Vector3.zero;
        GetComponent<Collider>().enabled = false;

        // Reload the current scene after a delay
        Invoke(nameof(ReloadScene), deathDelay);
    }

    private void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}   
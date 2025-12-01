using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Attach this script to an empty GameObject with a BoxCollider set as Trigger.
/// When the player enters, it triggers the end-of-level sequence.
/// </summary>
public class EndOfLevelTrigger : MonoBehaviour
{
    [Header("Settings")]
    [Tooltip("Tag of the player GameObject.")]
    public string playerTag = "Player";

    [Tooltip("Delay before loading the next scene (seconds).")]
    public float loadDelay = 2f;

    [Tooltip("Optional particle effect to play on trigger.")]
    public ParticleSystem endEffect;

    [Tooltip("Optional audio clip to play on trigger.")]
    public AudioClip endSound;

    private bool levelCompleted = false;
    private AudioSource audioSource;

    private void Awake()
    {
        // Ensure collider is set as trigger
        Collider col = GetComponent<Collider>();
        if (col == null)
        {
            Debug.LogError("EndOfLevelTrigger requires a Collider component.");
        }
        else if (!col.isTrigger)
        {
            Debug.LogWarning("Collider is not set as Trigger. Setting it now.");
            col.isTrigger = true;
        }

        // Prepare audio source if needed
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (levelCompleted) return; // Prevent multiple triggers

        if (other.CompareTag(playerTag))
        {
            levelCompleted = true;
            Debug.Log("End of level reached!");

            // Play particle effect
            if (endEffect != null)
            {
                endEffect.Play();
            }

            // Play sound
            if (endSound != null)
            {
                audioSource.clip = endSound;
                audioSource.Play();
            }

            // Delay scene load
            Invoke(nameof(LoadNextScene), loadDelay);
        }
    }

    private void LoadNextScene()
    {
        int currentIndex = SceneManager.GetActiveScene().buildIndex;
        int nextIndex = currentIndex + 1;

        if (nextIndex < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(nextIndex);
        }
        else
        {
            Debug.Log("No more scenes in Build Settings. Restarting first scene.");
            SceneManager.LoadScene(0);
        }
    }
}
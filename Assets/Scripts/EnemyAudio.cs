using UnityEngine;

public class EnemyAudio : MonoBehaviour
{
    public AudioSource source;
    public AudioClip chase;
    public float maxVolumeDistance = 5f;  // Distance at which the volume is at maximum
    public float minVolumeDistance = 20f;  // Distance at which the volume is at minimum

    private Transform player;  // Reference to the player's transform

    public static EnemyAudio instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        if (source == null)
        {
            source = GetComponent<AudioSource>();
        }

        if (source == null)
        {
            Debug.LogError("AudioSource component is missing!");
            return;
        }

        if (chase == null)
        {
            Debug.LogError("AudioClip is not assigned!");
            return;
        }

        source.clip = chase;
        source.loop = true;
        source.Play();

        // Find the player transform by tag
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
        if (playerObject != null)
        {
            player = playerObject.transform;
        }
        else
        {
            Debug.LogError("Player object not found!");
        }
    }

    void Update()
    {
        if (player == null)
        {
            Debug.LogError("Player Transform is not assigned!");
            return;
        }

        float distance = Vector3.Distance(transform.position, player.position);
        AdjustVolumeBasedOnDistance(distance);
    }

    void AdjustVolumeBasedOnDistance(float distance)
    {
        if (distance <= maxVolumeDistance)
        {
            source.volume = 1f;  // Full volume
        }
        else if (distance >= minVolumeDistance)
        {
            source.volume = 0f;  // Mute
        }
        else
        {
            // Linear interpolation between maxVolumeDistance and minVolumeDistance
            float t = (distance - maxVolumeDistance) / (minVolumeDistance - maxVolumeDistance);
            source.volume = 1f - t;
        }
    }
}

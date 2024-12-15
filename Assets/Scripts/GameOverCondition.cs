using UnityEngine;

public class GameOverCondition : MonoBehaviour
{
    public Transform playerTransform;
    public Transform enemyTransform;
    public float gameOverDistance = 1.0f;

    private void Update()
    {
        if (playerTransform == null || enemyTransform == null)
        {
            Debug.LogError("PlayerTransform or EnemyTransform is not assigned!");
            return;
        }

        float distance = Vector3.Distance(playerTransform.position, enemyTransform.position);
        Debug.Log("Current Distance: " + distance);

        if (distance <= gameOverDistance)
        {
            Debug.Log("Game Over Triggered");
            TriggerGameOver();
        }
    }

    public void AssignTransforms(Transform player, Transform enemy)
    {
        playerTransform = player;
        enemyTransform = enemy;
    }

    private void TriggerGameOver()
    {
        // Trigger game over
        Debug.Log("TriggerGameOver() called.");
        GameManager.instance.GameOver();
        EnemyAudio.instance.source.Stop();
    }
}

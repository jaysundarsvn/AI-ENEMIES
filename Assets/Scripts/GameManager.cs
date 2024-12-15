using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public GameObject joystickPlayerPrefab;
    public GameObject gameOverPanel;
    public GameObject enemyPrefab;
    public Transform playerTransform;
    private float currentSurvivalTime = 0f;
    private float highestSurvivalTime = 0f;
    public bool gameStart;
    public GameObject trigger_button;
    public Transform[] spawnPoints;
    public AudioSource source_bg;
    public AudioClip after;
    public AudioClip before;
    public float maxVolumeDistance = 5f;
    public float minVolumeDistance = 20f;
    public Transform playerSpawnPoint;
    public Vector2 joystickPlayerPosition;
    public Vector2 joystickCameraPosition;
    private GameOverCondition gameOverCondition;

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
        //InitializeGame();

        if (source_bg == null)
        {
            Debug.LogError("AudioSource is not assigned in the Inspector.");
            return;
        }

        source_bg.clip = before;
        source_bg.Play();
    }

    void Update()
    {
    }

    void InitializeGame()
    {
        //GameObject player = Instantiate(playerPrefab, playerSpawnPoint.position, Quaternion.identity);
    }

    void GameStart()
    {
        gameOverPanel.SetActive(false);
        joystickPlayerPrefab.SetActive(true);
    }

    public void GameOver()
    {
        gameOverPanel.SetActive(true);
        gameStart = false;
        trigger_button.SetActive(false);
        joystickPlayerPrefab.SetActive(false);
    }

    void EnemySpawn()
    {
        if (spawnPoints.Length > 0)
        {
            int randomIndex = Random.Range(0, spawnPoints.Length);
            Transform randomPosition = spawnPoints[randomIndex];
            GameObject spawnedEnemy = Instantiate(enemyPrefab, randomPosition.position, Quaternion.identity);

            Transform enemyTransform = spawnedEnemy.transform;
            gameOverCondition = FindObjectOfType<GameOverCondition>();
            gameOverCondition.AssignTransforms(playerTransform, enemyTransform);
        }
    }

    public void Trigger()
    {
        Invoke("EnemySpawn", 15f);
        trigger_button.SetActive(false);

        gameStart = true;

        source_bg.clip = after;
        source_bg.Play();

        GameStart();
    }


    public void RecordSurvivalTime()
    {
        if (currentSurvivalTime > highestSurvivalTime)
        {
            highestSurvivalTime = currentSurvivalTime;
        }
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("Start Game");
    }

    public void Restart()
    {
        SceneManager.LoadScene("Maze 1");
    }
}

using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SurvivalTimer : MonoBehaviour
{
    private float elapsedTime;
    private bool isGameRunning;

    public TextMeshProUGUI timerText; // Assign this in the Unity Editor to display the current time

    private const string BestTimeKey = "BestTime";

    void Start()
    {
        elapsedTime = 0f;
        isGameRunning = true;
        Debug.Log("Game Started. Best Time: " + FormatTime(PlayerPrefs.GetFloat(BestTimeKey, 0)));
    }

    void Update()
    {
        if (GameManager.instance.gameStart)
        {
            elapsedTime += Time.deltaTime;
            UpdateTimerDisplay();
        }
    }

    public void EndGame()
    {
        GameManager.instance.gameStart = false;
        SaveBestTime();
        DisplayFinalTime();
    }

    void UpdateTimerDisplay()
    {
        if (timerText != null)
        {
            timerText.text = FormatTime(elapsedTime);
        }
    }

    void DisplayFinalTime()
    {
        Debug.Log("Final Survival Time: " + FormatTime(elapsedTime));
    }

    void SaveBestTime()
    {
        float bestTime = PlayerPrefs.GetFloat(BestTimeKey, 0);
        if (elapsedTime > bestTime)
        {
            PlayerPrefs.SetFloat(BestTimeKey, elapsedTime);
            PlayerPrefs.Save();
            Debug.Log("New Best Time: " + FormatTime(elapsedTime));
        }
        else
        {
            Debug.Log("Best Time remains: " + FormatTime(bestTime));
        }
    }

    string FormatTime(float time)
    {
        int minutes = Mathf.FloorToInt(time / 60F);
        int seconds = Mathf.FloorToInt(time % 60F);
        return string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}

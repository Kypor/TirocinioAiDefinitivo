using TMPro;
using UnityEngine;
using UnityEngine.UI; // Required for UI elements like Text

public class CountdownTimer : MonoBehaviour
{
    public float totalTime = 90f; // Total time for the countdown in seconds
    public TextMeshProUGUI timerText; // Reference to a UI Text element to display the timer
    private bool timerActive = true; // Flag to control timer activation
    [SerializeField]
    GameObject GameOverPanel;
    void Update()
    {
        if (timerActive && totalTime > 0)
        {
            totalTime -= Time.deltaTime;
            UpdateTimerDisplay(); // Update the UI display
        }
        else if (timerActive)
        {
            timerActive = false;
            OnTimerEnd(); // Call a function when the timer expires
        }
    }

    void UpdateTimerDisplay()
    {
        if (timerText != null)
        {
            int minutes = Mathf.FloorToInt(totalTime / 60);
            int seconds = Mathf.FloorToInt(totalTime % 60);
            timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds); // Display minutes and seconds
        }
    }
    public void StopTimer()
    {
        timerActive = false;
    }

    void OnTimerEnd()
    {
        SoundManager.instance.PlaySoundFX(4);
        PauseMenuManager pauseMenuManager = GetComponent<PauseMenuManager>();
        Debug.Log("Time's up!");
        StartCoroutine(pauseMenuManager.Fade(1, GameOverPanel.GetComponent<CanvasGroup>()));
        GameOverPanel.GetComponent<CanvasGroup>().interactable = true;
        GameOverPanel.GetComponent<CanvasGroup>().blocksRaycasts = true;
    }
}
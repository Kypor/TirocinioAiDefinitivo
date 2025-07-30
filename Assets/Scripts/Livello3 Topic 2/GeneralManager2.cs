using UnityEngine;
using UnityEngine.SceneManagement;

public class GeneralManager2 : MonoBehaviour
{
    QuestManager2 questManager;
    [SerializeField] AudioSource backgroundMusic;
    [SerializeField] private GameObject victoryPanel;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        questManager = FindAnyObjectByType<QuestManager2>();

        victoryPanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (questManager.allQuestsCompleted == true)
        {
            Time.timeScale = 0;
            backgroundMusic.Stop();
            victoryPanel.SetActive(true);
        }
    }

    public void ReturnToMenu()
    {
        SceneManager.LoadScene(0);
    }
}

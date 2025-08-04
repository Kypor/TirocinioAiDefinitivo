using UnityEngine;
using UnityEngine.SceneManagement;

public class GeneralManager : MonoBehaviour
{
    QuestManager questManager;
    [SerializeField] AudioSource backgroundMusic;
    [SerializeField] private GameObject victoryPanel;
    GameObject bgMusic;
    bool coroutineStarted = false;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
        questManager = FindAnyObjectByType<QuestManager>();
        bgMusic = GameObject.Find("BackgroundMusic");

        victoryPanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (questManager.allQuestsCompleted == true)
        {
            if (!coroutineStarted)
            {
                StartCoroutine(SoundManager.instance.FadeCore(bgMusic.GetComponent<AudioSource>(), 1f, SoundManager.instance.victoryMusic));
                coroutineStarted = true;
            }

            victoryPanel.SetActive(true);
        }
    }

    public void ReturnToMenu()
    {
        SceneManager.LoadScene(0);
    }
}

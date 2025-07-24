using UnityEngine;

public class GeneralManager : MonoBehaviour
{
    QuestManager questManager;
    [SerializeField] private GameObject victoryPanel;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        questManager = FindAnyObjectByType<QuestManager>();

        victoryPanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (questManager.allQuestsCompleted == true)
        {
            Time.timeScale = 0;
            victoryPanel.SetActive(true);
        }
    }
}

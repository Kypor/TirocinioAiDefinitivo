using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    [SerializeField] public Quests questsData;
    public Quest currentQuest;
    [SerializeField] public TextMeshProUGUI textMeshPro;
    private CharacterBehaviour character;
    private int currentQuestID = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        character = FindAnyObjectByType<CharacterBehaviour>();

        foreach (Quest quest in questsData.quests)
        {
            Debug.Log("QuestID: " + quest.questID);
            quest.isCompleted = false;
        }
        textMeshPro.text = questsData.quests[currentQuestID].description;
    }

    // Update is called once per frame
    void Update()
    {
        if (currentQuestID >= questsData.quests.Count) return;

        currentQuest = questsData.quests[currentQuestID];


        if (currentQuest.CheckCondition(character.currentVerb, character.currentNoun))
        {
            currentQuest.isCompleted = true;
            Debug.Log("Quest completata: " + currentQuest.description);
            currentQuestID++;
            StartCoroutine(waitForNextQuest());
        }

        
    }

    private IEnumerator waitForNextQuest()
    {
        yield return new WaitForSeconds(3);
        textMeshPro.text = currentQuest.description;
    }
}

using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    [SerializeField] public Quests questsData;
    public Quest currentQuest;
    [SerializeField] public TextMeshProUGUI textMeshPro;
    private CharacterBehaviour character;
    private int currentQuestID = 0;
    public bool allQuestsCompleted { get; private set; }


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        allQuestsCompleted = false;

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

            if (currentQuestID >= questsData.quests.Count)
            {
                Debug.Log("Hai completato l'ultima quest!");
                StartCoroutine(finalQuestWait());

                return;
            }


            StartCoroutine(waitForNextQuest());
        }


    }

    private IEnumerator waitForNextQuest()
    {
        yield return new WaitForSeconds(3);
        textMeshPro.text = currentQuest.description;
    }
    private IEnumerator finalQuestWait()
    {
        yield return new WaitForSeconds(5f);
        textMeshPro.text = null;
        allQuestsCompleted = true;
    }
}

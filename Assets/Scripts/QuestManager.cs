using System.Collections;
using TMPro;
using UnityEngine;

public class QuestManager : MonoBehaviour
{

    [SerializeField] public Quests questsData;
    public Quest currentQuest;
    [SerializeField] public TextMeshProUGUI textMeshPro;
    [SerializeField] private GameObject questPanel;
    private CharacterBehaviour character;
    private int currentQuestID = 0;
    public bool allQuestsCompleted { get; private set; }


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

        questPanel.SetActive(true);
        allQuestsCompleted = false;

        character = FindAnyObjectByType<CharacterBehaviour>();

        foreach (Quest quest in questsData.quests)
        {
            //Debug.Log("QuestID: " + quest.questID);
            quest.isCompleted = false;
        }

        textMeshPro.text = questsData.quests[currentQuestID].description;
    }

    // Update is called once per frame
    void Update()
    {
        if (currentQuestID >= questsData.quests.Count)
        {
            textMeshPro.color = Color.green;
            StartCoroutine(TextLerp(false));


            if (CharacterBehaviour.gameFinished == true)
            {
                Debug.Log("Hai completato l'ultima quest!");
                questPanel.SetActive(false);
                textMeshPro.text = null;
                allQuestsCompleted = true;
            }
            //StartCoroutine(finalQuestWait());

            return;
        }

        //if (currentQuestID >= questsData.quests.Count) return;

        currentQuest = questsData.quests[currentQuestID];

        if (currentQuest.CheckCondition(character.currentVerb, character.currentNoun))
        {
            currentQuest.isCompleted = true;
            Debug.Log("Quest completata: " + currentQuest.description);
            currentQuestID++;
            SoundManager.instance.PlaySoundFX(2);


            StartCoroutine(waitForNextQuest());
        }
    }

    private IEnumerator waitForNextQuest()
    {
        textMeshPro.color = Color.green;
        yield return new WaitForSeconds(2);
        StartCoroutine(TextLerp(false));
        yield return new WaitForSeconds(2);
        textMeshPro.color = Color.white;
        StartCoroutine(TextLerp(true));
    }

    private IEnumerator finalQuestWait()
    {
        yield return new WaitForSeconds(2f);
        questPanel.SetActive(false);
        textMeshPro.text = null;
        allQuestsCompleted = true;
    }

    public IEnumerator TextLerp(bool reversed)
    {
        Vector2 startPoint;
        Vector2 endPoint;

        if (!reversed)
        {
            startPoint = textMeshPro.transform.position;
            endPoint = new Vector2(startPoint.x - 1000, startPoint.y);
        }
        else
        {
            startPoint = textMeshPro.transform.position;
            endPoint = new Vector2(startPoint.x + 1000, startPoint.y);
        }

        float elapsedTime = 0.0f;

        while (elapsedTime < 1f)
        {
            // Debug.Log("Pu");
            elapsedTime += Time.deltaTime;
            //canvasGroup.alpha = Mathf.Lerp(start, end, elapsedTime / 0.5f);
            textMeshPro.transform.position = Vector2.Lerp(startPoint, endPoint, elapsedTime);
            yield return null;
        }

        textMeshPro.text = currentQuest.description;
    }
}

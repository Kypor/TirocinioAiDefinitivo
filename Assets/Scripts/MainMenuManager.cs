using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField]
    GameObject backgroundPanel, mainPanel, topicPanel, levelPanel, blackCanvas, saveFilePanel;
    public float minPointsLv1Top1, minPointsLv2Top1, minPointsLv3Top1;
    public float minPointsLv1Top2, minPointsLv2Top2, minPointsLv3Top2;
    [SerializeField]
    private JapaneseIdeoArray IdeoArray1, IdeoArray2;
    [SerializeField]
    private JapaneseWordArray WordArray1, WordArray2;
    [SerializeField]
    private Quests QuestArray1, QuestArray2;
    [SerializeField]
    TextMeshProUGUI thirdLevelText;
    [SerializeField]
    EventTrigger eventTriggerThirdLevelText;
    public GameObject pointsLabel;
    [SerializeField]
    public static int topicChosen = 2, levelIndex = 1;
    public List<int> AllStars = new();

    void Start()
    {

        mainPanel.SetActive(true);
        topicPanel.SetActive(false);
        levelPanel.SetActive(false);

    }

    public void ClickedPlay()
    {
        SoundManager.instance.PlaySoundFX(0);
        saveFilePanel.SetActive(true);
        mainPanel.SetActive(false);
    }

    public void ChoseTopic()
    {
        if (SavePlayerDataManager.currentPlayerData != null)
        {
            SoundManager.instance.PlaySoundFX(0);
            mainPanel.SetActive(false);
            saveFilePanel.SetActive(false);
            topicPanel.SetActive(true);
            levelPanel.SetActive(false);
        }

    }

    public void ChoseLevel(int topic)
    {
        FillStars();
        topicChosen = topic;
        switch (topic)
        {
            case 1:
                SoundManager.instance.PlaySoundFX(0);
                //PlayerPrefs.GetFloat("FirstTopicLevel1Points") < minPointsLv1Top1 || PlayerPrefs.GetFloat("FirstTopicLevel2Points") < minPointsLv2Top1
                if (SavePlayerDataManager.currentPlayerData.pointsLv1To1 < minPointsLv1Top1 || SavePlayerDataManager.currentPlayerData.pointsLv2To1 < minPointsLv2Top1)
                {
                    eventTriggerThirdLevelText.enabled = false;
                    thirdLevelText.color = Color.gray;
                }
                else
                {
                    eventTriggerThirdLevelText.enabled = true;
                    thirdLevelText.color = Color.black;
                }
                Debug.Log("caso primo topic");
                mainPanel.SetActive(false);
                topicPanel.SetActive(false);
                levelPanel.SetActive(true);
                break;
            case 2:
                SoundManager.instance.PlaySoundFX(0);
                //PlayerPrefs.GetFloat("SecondTopicLevel1Points") < minPointsLv1Top2 || PlayerPrefs.GetFloat("SecondTopicLevel2Points") < minPointsLv2Top2
                if (SavePlayerDataManager.currentPlayerData.pointsLv1To2 < minPointsLv1Top2 || SavePlayerDataManager.currentPlayerData.pointsLv2To2 < minPointsLv2Top2)
                {
                    eventTriggerThirdLevelText.enabled = false;
                    thirdLevelText.color = Color.gray;
                }
                else
                {
                    eventTriggerThirdLevelText.enabled = true;
                    thirdLevelText.color = Color.black;
                }
                Debug.Log("caso secondo topic");
                mainPanel.SetActive(false);
                topicPanel.SetActive(false);
                levelPanel.SetActive(true);
                break;
            default:
                Debug.Log("no topic");
                break;
        }
    }

    public void BackToTopic()
    {
        SoundManager.instance.PlaySoundFX(0);
        mainPanel.SetActive(false);
        topicPanel.SetActive(true);
        levelPanel.SetActive(false);
        saveFilePanel.SetActive(false);
    }

    public void BackToSave()
    {
        SoundManager.instance.PlaySoundFX(0);
        mainPanel.SetActive(false);
        topicPanel.SetActive(false);
        levelPanel.SetActive(false);
        saveFilePanel.SetActive(true);
    }

    public void BackToMain()
    {
        SoundManager.instance.PlaySoundFX(0);
        mainPanel.SetActive(true);
        topicPanel.SetActive(false);
        levelPanel.SetActive(false);
        saveFilePanel.SetActive(false);
    }

    public void Level1()
    {
        levelIndex = 1;
        SoundManager.instance.PlaySoundFX(0);
        StartCoroutine(LoadLevel(1));
    }

    public void Level2()
    {
        levelIndex = 2;
        SoundManager.instance.PlaySoundFX(0);
        switch (topicChosen)
        {
            case 1:
                StartCoroutine(LoadLevel(2));
                break;
            case 2:
                StartCoroutine(LoadLevel(3));
                break;
            default:
                Debug.Log(" no level");
                break;
        }
    }

    public void Level3()
    {
        levelIndex = 3;
        switch (topicChosen)
        {
            case 1:
                SoundManager.instance.PlaySoundFX(0);
                //PlayerPrefs.GetFloat("FirstTopicLevel1Points") >= minPointsLv1Top1 && PlayerPrefs.GetFloat("FirstTopicLevel2Points") >= minPointsLv2Top1
                if (SavePlayerDataManager.currentPlayerData.pointsLv1To1 >= minPointsLv1Top1 && SavePlayerDataManager.currentPlayerData.pointsLv2To1 >= minPointsLv2Top1)
                {
                    StartCoroutine(LoadLevel(4));
                }
                break;
            case 2:
                SoundManager.instance.PlaySoundFX(0);
                //PlayerPrefs.GetFloat("SecondTopicLevel1Points") >= minPointsLv1Top2 && PlayerPrefs.GetFloat("SecondTopicLevel2Points") >= minPointsLv2Top2
                if (SavePlayerDataManager.currentPlayerData.pointsLv1To2 >= minPointsLv1Top2 && SavePlayerDataManager.currentPlayerData.pointsLv2To2 >= minPointsLv2Top2)
                {
                    StartCoroutine(LoadLevel(5));
                }
                break;
            default:
                Debug.Log("no topic");
                break;
        }
    }

    public IEnumerator LoadLevel(int level)
    {
        //levelIndex = level;
        levelPanel.SetActive(false);
        StartCoroutine(Fade(1, blackCanvas.GetComponent<CanvasGroup>()));
        yield return new WaitForSeconds(1f);
        StartCoroutine(LoadYourAsyncScene(level));
    }

    IEnumerator LoadYourAsyncScene(int level)
    {
        if (SavePlayerDataManager.currentPlayerData == null)
        {
            Debug.LogWarning("no data loaded");

            yield return null;
        }
        // The Application loads the Scene in the background as the current Scene runs.

        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(level);

        // Wait until the asynchronous scene fully loads
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
        Debug.Log("scena caricata");
    }

    public void Exit()
    {
        Application.Quit();
    }

    public IEnumerator Fade(float end, CanvasGroup canvasGroup)
    {
        SoundManager.instance.PlaySoundFX(0);
        float elapsedTime = 0.0f;
        float start = canvasGroup.alpha;
        while (elapsedTime < 0.5f)
        {
            elapsedTime += Time.deltaTime;
            canvasGroup.alpha = Mathf.Lerp(start, end, elapsedTime / 0.5f);
            yield return null;
        }
        canvasGroup.alpha = end;
    }
    public void FillStars()
    {
        float RatiostarsLv1T1 = SavePlayerDataManager.currentPlayerData.pointsLv1To1 / (WordArray1.paroleConPronunce.Count * 100);
        float RatiostarsLv1T2 = SavePlayerDataManager.currentPlayerData.pointsLv1To2 / (IdeoArray1.ideos.Count * 100);
        float RatiostarsLv2T1 = SavePlayerDataManager.currentPlayerData.pointsLv2To1 / (WordArray2.paroleConPronunce.Count * 100);
        float RatiostarsLv2T2 = SavePlayerDataManager.currentPlayerData.pointsLv2To2 / (IdeoArray2.ideos.Count * 100);
        float RatiostarsLv3T1 = SavePlayerDataManager.currentPlayerData.pointsLv3To1 / (QuestArray1.quests.Count * 100);
        float RatiostarsLv3T2 = SavePlayerDataManager.currentPlayerData.pointsLv3To2 / (QuestArray2.quests.Count * 100);
        List<float> Ratios = new()
        {
            RatiostarsLv1T1,
            RatiostarsLv1T2,
            RatiostarsLv2T1,
            RatiostarsLv2T2,
            RatiostarsLv3T1,
            RatiostarsLv3T2
        };
        int index = 0;
        foreach (float ratio in Ratios)
        {
            if (ratio >= 0.90f) AllStars[index] = 3;
            else if (ratio >= 0.66f) AllStars[index] = 2;
            else if (ratio >= 0.33f) AllStars[index] = 1;
            else AllStars[index] = 0;
            index++;
        }
    }
}

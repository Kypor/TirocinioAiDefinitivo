using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField]
    GameObject backgroundPanel, mainPanel, topicPanel, levelPanel, blackCanvas;
    public float minPointsLv1Top1, minPointsLv2Top1;
    public float minPointsLv1Top2, minPointsLv2Top2;
    [SerializeField]
    TextMeshProUGUI thirdLevelText;
    [SerializeField]
    EventTrigger eventTriggerThirdLevelText;
    public GameObject pointsLabel;

    public static int topicChosen = 1;
    void Start()
    {
        mainPanel.SetActive(true);
        topicPanel.SetActive(false);
        levelPanel.SetActive(false);
    }
    public void ChoseTopic()
    {
        SoundManager.instance.PlaySoundFX(0);
        mainPanel.SetActive(false);
        topicPanel.SetActive(true);
        levelPanel.SetActive(false);
    }
    public void ChoseLevel(int topic)
    {
        topicChosen = topic;
        switch (topic)
        {
            case 1:
                SoundManager.instance.PlaySoundFX(0);
                if (PlayerPrefs.GetFloat("FirstTopicLevel1Points") < minPointsLv1Top1 || PlayerPrefs.GetFloat("FirstTopicLevel2Points") < minPointsLv2Top1)
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
                if (PlayerPrefs.GetFloat("SecondTopicLevel1Points") < minPointsLv1Top2 || PlayerPrefs.GetFloat("SecondTopicLevel2Points") < minPointsLv2Top2)
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
    }
    public void BackToMain()
    {
        SoundManager.instance.PlaySoundFX(0);
        mainPanel.SetActive(true);
        topicPanel.SetActive(false);
        levelPanel.SetActive(false);
    }
    public void Level1()
    {
        SoundManager.instance.PlaySoundFX(0);
        StartCoroutine(LoadLevel(1));
    }
    public void Level2()
    {
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
        switch (topicChosen)
        {
            case 1:
                SoundManager.instance.PlaySoundFX(0);
                if (PlayerPrefs.GetFloat("FirstTopicLevel1Points") >= minPointsLv1Top1 && PlayerPrefs.GetFloat("FirstTopicLevel2Points") >= minPointsLv2Top1)
                {
                    StartCoroutine(LoadLevel(4));
                }
                break;
            case 2:
                SoundManager.instance.PlaySoundFX(0);
                if (PlayerPrefs.GetFloat("SecondTopicLevel1Points") >= minPointsLv1Top2 && PlayerPrefs.GetFloat("SecondTopicLevel2Points") >= minPointsLv2Top2)
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
        levelPanel.SetActive(false);
        StartCoroutine(Fade(1, blackCanvas.GetComponent<CanvasGroup>()));
        yield return new WaitForSeconds(1f);
        StartCoroutine(LoadYourAsyncScene(level));
    }
    IEnumerator LoadYourAsyncScene(int level)
    {
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
}

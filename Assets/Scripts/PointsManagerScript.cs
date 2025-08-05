using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Whisper.Samples;
using UnityEngine.SceneManagement;
using System;

public class PointsManagerScript : MonoBehaviour
{
    public TextMeshProUGUI totalPointsText, finalPointText;
    public GameObject finishGamePanel;
    private float totalPoints;
    [SerializeField]
    public float basePoints, penalityPercentage;
    private int numberOfWords;
    [SerializeField] Sprite fullStarSprite, emptyStarSprite;
    [SerializeField] List<Image> starsImages = new List<Image>();
    int currSceneIndex;

    GameObject bgMusic;


    private void Awake()
    {
        bgMusic = GameObject.Find("BackgroundMusic");


        switch (MainMenuManager.levelIndex)
        {
            case 1:
                numberOfWords = GetComponent<MicrophoneDemo>().JapaneseWords[MainMenuManager.topicChosen - 1].paroleConPronunce.Count;
                break;
            case 2:
                numberOfWords = GetComponent<DrawRandomIdeo>().japaneseIdeoArray.ideos.Count;
                break;
            case 3:
                numberOfWords = GetComponent<QuestManager>().questsData.quests.Count;
                break;
            default:
                break;

        }
        EmptyStars();
        totalPoints = 0f;
        totalPointsText.text = "Points : " + totalPoints.ToString();
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

    void EmptyStars()
    {
        foreach (Image image in starsImages)
        {
            image.sprite = emptyStarSprite;
        }
    }



    public IEnumerator ShowResults()
    {
        StartCoroutine(SoundManager.instance.FadeCore(bgMusic.GetComponent<AudioSource>(), 1f, SoundManager.instance.victoryMusic));
        //bgMusic.GetComponent<AudioSource>().Stop();
        //SoundManager.instance.PlaySoundFX(SoundManager.instance.victoryMusic);
        //StartCoroutine(SoundManager.instance.FadeInCore(bgMusic.GetComponent<AudioSource>(), 2f, SoundManager.instance.victoryMusic));


        StartCoroutine(Fade(1, finishGamePanel.GetComponent<CanvasGroup>()));
        finishGamePanel.GetComponent<CanvasGroup>().interactable = true;
        finishGamePanel.GetComponent<CanvasGroup>().blocksRaycasts = true;
        totalPointsText.gameObject.SetActive(false);
        finalPointText.text = "Points : " + totalPoints.ToString();
        yield return new WaitForSeconds(1f);
        StartCoroutine(FillStars());
    }

    public void AddPoints(int level)
    {
        totalPoints += basePoints;
        totalPointsText.text = "Points : " + totalPoints.ToString();
        if (level == 1)
        {
            switch (MainMenuManager.topicChosen)
            {
                case 1:
                    //PlayerPrefs.SetFloat("FirstTopicLevel1Points", totalPoints);
                    Debug.Log("aggiunti punti primo livello primo topic");
                    SavePlayerDataManager.SetPoints(level, MainMenuManager.topicChosen, totalPoints);

                    break;
                case 2:
                    //PlayerPrefs.SetFloat("SecondTopicLevel1Points", totalPoints);
                    Debug.Log("aggiunti punti primo livello secondo topic");
                    SavePlayerDataManager.SetPoints(level, MainMenuManager.topicChosen, totalPoints);
                    break;
                default:
                    break;
            }
        }
        else if (level == 2)
        {
            switch (MainMenuManager.topicChosen)
            {
                case 1:
                    //PlayerPrefs.SetFloat("FirstTopicLevel2Points", totalPoints);
                    Debug.Log("aggiunti punti secondo livello primo topic");
                    SavePlayerDataManager.SetPoints(level, MainMenuManager.topicChosen, totalPoints);
                    break;
                case 2:
                    //PlayerPrefs.SetFloat("SecondTopicLevel2Points", totalPoints);
                    Debug.Log("aggiunti punti secondo livello secondo topic");
                    SavePlayerDataManager.SetPoints(level, MainMenuManager.topicChosen, totalPoints);
                    break;
                default:
                    break;
            }
        }
        else if (level == 3)
        {
            switch (MainMenuManager.topicChosen)
            {
                case 1:
                    //PlayerPrefs.SetFloat("FirstTopicLevel2Points", totalPoints);
                    Debug.Log("aggiunti punti secondo livello primo topic");
                    SavePlayerDataManager.SetPoints(level, MainMenuManager.topicChosen, totalPoints);
                    break;
                case 2:
                    //PlayerPrefs.SetFloat("SecondTopicLevel2Points", totalPoints);
                    Debug.Log("aggiunti punti secondo livello secondo topic");
                    SavePlayerDataManager.SetPoints(level, MainMenuManager.topicChosen, totalPoints);
                    break;
                default:
                    break;
            }
        }
    }

    public void SubPoints(int level)
    {
        float points = penalityPercentage / 100f * basePoints;
        totalPoints -= points;
        totalPointsText.text = "Points : " + totalPoints.ToString();
        if (level == 1)
        {
            switch (MainMenuManager.topicChosen)
            {
                case 1:
                    //PlayerPrefs.SetFloat("FirstTopicLevel1Points", totalPoints);
                    Debug.Log("sottratti punti primo livello primo topic");
                    SavePlayerDataManager.SetPoints(level, MainMenuManager.topicChosen, totalPoints);
                    break;
                case 2:
                    //PlayerPrefs.SetFloat("SecondTopicLevel1Points", totalPoints);
                    Debug.Log("sottratti punti primo livello secondo topic");
                    SavePlayerDataManager.SetPoints(level, MainMenuManager.topicChosen, totalPoints);
                    break;
                default:
                    break;
            }
        }
        else if (level == 2)
        {
            switch (MainMenuManager.topicChosen)
            {
                case 1:
                    //PlayerPrefs.SetFloat("FirstTopicLevel2Points", totalPoints);
                    Debug.Log("sottratti punti secondo livello primo topic");
                    SavePlayerDataManager.SetPoints(level, MainMenuManager.topicChosen, totalPoints);
                    break;
                case 2:
                    //PlayerPrefs.SetFloat("SecondTopicLevel2Points", totalPoints);
                    Debug.Log("sottratti punti secondo livello secondo topic");
                    SavePlayerDataManager.SetPoints(level, MainMenuManager.topicChosen, totalPoints);
                    break;
                default:
                    break;
            }
        }
        else if (level == 3)
        {
            switch (MainMenuManager.topicChosen)
            {
                case 1:
                    //PlayerPrefs.SetFloat("FirstTopicLevel2Points", totalPoints);
                    Debug.Log("aggiunti punti secondo livello primo topic");
                    SavePlayerDataManager.SetPoints(level, MainMenuManager.topicChosen, totalPoints);
                    break;
                case 2:
                    //PlayerPrefs.SetFloat("SecondTopicLevel2Points", totalPoints);
                    Debug.Log("aggiunti punti secondo livello secondo topic");
                    SavePlayerDataManager.SetPoints(level, MainMenuManager.topicChosen, totalPoints);
                    break;
                default:
                    break;
            }
        }
    }

    IEnumerator FillStars()
    {
        float performanceRatio = totalPoints / (numberOfWords * basePoints);
        int stars;
        Debug.Log(numberOfWords);
        if (performanceRatio >= 0.90f) stars = 3;
        else if (performanceRatio >= 0.66f) stars = 2;
        else if (performanceRatio >= 0.33f) stars = 1;
        else stars = 0;
        Debug.Log(stars);
        for (int i = 0; i < stars; i++)
        {
            SoundManager.instance.PlaySoundFX(3);
            starsImages[i].sprite = fullStarSprite;
            yield return new WaitForSeconds(0.5f);
        }
    }
}
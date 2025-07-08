using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField]
    GameObject backgroundPanel, mainPanel, levelPanel;
    public float minPointsLevel1, minPointsLevel2;
    [SerializeField]
    TextMeshProUGUI thirdLevelText;
    public GameObject pointsLabel;
    void Start()
    {
        mainPanel.SetActive(true);
        levelPanel.SetActive(false);
    }
    public void ChoseLevel()
    {
        if (PlayerPrefs.GetFloat("Level1Points") < minPointsLevel1 || PlayerPrefs.GetFloat("Level2Points") < minPointsLevel2)
        {
            thirdLevelText.color = Color.gray;
        }
        else
        {
            thirdLevelText.color = Color.white;
        }
        mainPanel.SetActive(false);
        levelPanel.SetActive(true);
    }
    public void Back()
    {
        mainPanel.SetActive(true);
        levelPanel.SetActive(false);
    }
    public void Level1()
    {
        StartCoroutine(LoadLevel(1));
    }
    public void Level2()
    {
        StartCoroutine(LoadLevel(2));
    }
    public void Level3()
    {
        if (PlayerPrefs.GetFloat("Level1Points") >= minPointsLevel1 && PlayerPrefs.GetFloat("Level2Points") >= minPointsLevel2)
        {
            StartCoroutine(LoadLevel(3));
        }
    }
    public IEnumerator LoadLevel(int level)
    {
        levelPanel.SetActive(false);
        float elapsedTime = 0.0f;
        float start = backgroundPanel.transform.localScale.x;
        float end = start + 6;
        while (elapsedTime < 1f)
        {
            elapsedTime += Time.deltaTime;
            backgroundPanel.transform.localScale = new Vector3(Mathf.Lerp(start, end, elapsedTime / 1f), Mathf.Lerp(start, end, elapsedTime / 1f), Mathf.Lerp(start, end, elapsedTime / 1f));
            yield return null;
        }
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
}

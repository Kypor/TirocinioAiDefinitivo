using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SettingsManagerLv3 : MonoBehaviour
{
    [SerializeField] private GameObject settingsPanel;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        ShowSettings();
        Debug.Log("SettingsManagerLv3 Start");
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            Settings();
        }
    }

    void Settings()
    {
        if (settingsPanel.GetComponent<CanvasGroup>().alpha == 0)
        {
            ShowSettings();
        }
        else if (settingsPanel.GetComponent<CanvasGroup>().alpha == 1)
        {
            UnShowSettings();
        }
    }


    void ShowSettings()
    {
        StopAllCoroutines();
        StartCoroutine(Fade(1, settingsPanel.GetComponent<CanvasGroup>()));
        settingsPanel.GetComponent<CanvasGroup>().interactable = true;
        settingsPanel.GetComponent<CanvasGroup>().blocksRaycasts = true;
    }

    public void UnShowSettings()
    {
        StopAllCoroutines();
        StartCoroutine(Fade(0, settingsPanel.GetComponent<CanvasGroup>()));
        settingsPanel.GetComponent<CanvasGroup>().interactable = false;
        settingsPanel.GetComponent<CanvasGroup>().blocksRaycasts = false;
    }

    public IEnumerator Fade(float end, CanvasGroup canvasGroup)
    {
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
    public void Restart()
    {
        StopAllCoroutines();
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().name);
    }
    public void BackToMenu()
    {
        SceneManager.LoadScene(0);
    }
}

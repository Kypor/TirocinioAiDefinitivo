using System.Collections;
using TMPro;
//using UnityEditor.Rendering.Canvas.ShaderGraph;
using UnityEngine;
using UnityEngine.UI;

public class MouseOverScript : MonoBehaviour
{
    TextMeshProUGUI text;
    string txt;
    Color textColor;
    MainMenuManager mainMenuManager;
    [SerializeField]
    float fadeTime = 0.2f;

    private int topic = MainMenuManager.topicChosen;
    void Start()
    {
        text = GetComponent<TextMeshProUGUI>();
        txt = text.text;
        textColor = text.color;
    }
    public void MouseOver()
    {
        if (gameObject.tag != "ThirdLevelTag")
        {
            text.text = "<b>" + txt + "</b>";
            text.outlineWidth = 0.2f;
            text.outlineColor = Color.white;
            text.color = Color.white;
        }
        else
        {
            mainMenuManager = FindFirstObjectByType<MainMenuManager>().GetComponent<MainMenuManager>();

            if (topic == 1)
            {
                if (PlayerPrefs.GetFloat("Level1Points") >= mainMenuManager.minPointsLv1Top1 && PlayerPrefs.GetFloat("Level2Points") >= mainMenuManager.minPointsLv2Top1)
                {
                    text.text = "<b>" + txt + "</b>";
                    text.outlineWidth = 0.2f;
                    text.outlineColor = Color.white;
                    text.color = Color.white;
                }
            }
            else
            {
                if (PlayerPrefs.GetFloat("Level1Points") >= mainMenuManager.minPointsLv1Top2 && PlayerPrefs.GetFloat("Level2Points") >= mainMenuManager.minPointsLv2Top2)
                {
                    text.text = "<b>" + txt + "</b>";
                    text.outlineWidth = 0.2f;
                    text.outlineColor = Color.white;
                    text.color = Color.white;
                }
            }

        }
    }
    public void MouseExit()
    {
        if (gameObject.tag != "ThirdLevelTag")
        {
            text.text = txt;
            text.outlineWidth = 0f;
            text.outlineColor = Color.black;
            text.color = textColor;
        }
        else
        {
            mainMenuManager = FindFirstObjectByType<MainMenuManager>().GetComponent<MainMenuManager>();
            if (topic == 1)
            {
                if (PlayerPrefs.GetFloat("Level1Points") >= mainMenuManager.minPointsLv1Top1 && PlayerPrefs.GetFloat("Level2Points") >= mainMenuManager.minPointsLv2Top1)
                {
                    text.text = "<b>" + txt + "</b>";
                    text.outlineWidth = 0.2f;
                    text.outlineColor = Color.white;
                    text.color = Color.white;
                }
            }
            else
            {
                if (PlayerPrefs.GetFloat("Level1Points") >= mainMenuManager.minPointsLv1Top2 && PlayerPrefs.GetFloat("Level2Points") >= mainMenuManager.minPointsLv2Top2)
                {
                    text.text = "<b>" + txt + "</b>";
                    text.outlineWidth = 0.2f;
                    text.outlineColor = Color.white;
                    text.color = Color.white;
                }
            }
        }
    }
    public void ShowPoints()
    {
        StopAllCoroutines();
        mainMenuManager = FindFirstObjectByType<MainMenuManager>().GetComponent<MainMenuManager>();
        StartCoroutine(Fade(1, mainMenuManager.pointsLabel.GetComponent<CanvasGroup>()));
    }
    public void UnShowPoints()
    {
        StopAllCoroutines();
        mainMenuManager = FindFirstObjectByType<MainMenuManager>().GetComponent<MainMenuManager>();
        StartCoroutine(Fade(0, mainMenuManager.pointsLabel.GetComponent<CanvasGroup>()));
    }
    public IEnumerator Fade(float end, CanvasGroup canvasGroup)
    {
        float elapsedTime = 0.0f;
        float start = canvasGroup.alpha;
        while (elapsedTime < fadeTime)
        {
            elapsedTime += Time.deltaTime;
            canvasGroup.alpha = Mathf.Lerp(start, end, elapsedTime / fadeTime);
            yield return null;
        }
        canvasGroup.alpha = end;
    }
}

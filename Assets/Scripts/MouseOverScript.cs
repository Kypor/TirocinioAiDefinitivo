using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MouseOverScript : MonoBehaviour
{
    //[SerializeField]
    TextMeshProUGUI text;
    string txt;
    MainMenuManager mainMenuManager;
    void Start()
    {
        text = GetComponent<TextMeshProUGUI>();
        mainMenuManager = FindFirstObjectByType<MainMenuManager>().GetComponent<MainMenuManager>();
        txt = text.text;
    }
    public void MouseOver()
    {
        if (gameObject.tag != "ThirdLevelTag")
        {
            text.text = "<b>" + txt + "</b>";
            text.outlineWidth = 0.2f;
            text.outlineColor = Color.white;
            text.color = Color.black;
        }
        else
        {
            if (PlayerPrefs.GetFloat("Level1Points") >= mainMenuManager.minPointsLevel1 && PlayerPrefs.GetFloat("Level2Points") >= mainMenuManager.minPointsLevel2)
            {
                text.text = "<b>" + txt + "</b>";
                text.outlineWidth = 0.2f;
                text.outlineColor = Color.white;
                text.color = Color.black;
            }
        }
    }
    public void MouseExit()
    {
        if (gameObject.tag != "ThirdLevelTag")
        {
            text.text = txt;
            text.outlineWidth = 0f;
            text.outlineColor = Color.white;
            text.color = Color.white;
        }
        else
        {
            if (PlayerPrefs.GetFloat("Level1Points") >= mainMenuManager.minPointsLevel1 && PlayerPrefs.GetFloat("Level2Points") >= mainMenuManager.minPointsLevel2)
            {
                text.text = txt;
                text.outlineWidth = 0f;
                text.outlineColor = Color.white;
                text.color = Color.white;
            }
        }
    }
    public void ShowPoints()
    {
        mainMenuManager.pointsLabel.SetActive(true);
    }
    public void UnShowPoints()
    {
        mainMenuManager.pointsLabel.SetActive(false);
    }
}

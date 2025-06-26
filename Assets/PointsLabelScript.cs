using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PointsLabelScript : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI firstLevelPoints, secondLevelPoints, fistLevelMinPoint, secondLevelMinPoint;

    void OnEnable()
    {
        MainMenuManager mainMenuManager = FindFirstObjectByType<MainMenuManager>().GetComponent<MainMenuManager>();
        firstLevelPoints.text = PlayerPrefs.GetFloat("Level1Points").ToString();
        if (PlayerPrefs.GetFloat("Level1Points") >= mainMenuManager.minPointsLevel1)
        {
            firstLevelPoints.color = Color.green;
        }
        fistLevelMinPoint.text = "/" + mainMenuManager.minPointsLevel1;
        secondLevelPoints.text = PlayerPrefs.GetFloat("Level2Points").ToString();
        if (PlayerPrefs.GetFloat("Level2Points") >= mainMenuManager.minPointsLevel2)
        {
            secondLevelPoints.color = Color.green;
        }
        secondLevelMinPoint.text = "/" + mainMenuManager.minPointsLevel2;
    }
}

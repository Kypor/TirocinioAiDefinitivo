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
        switch (MainMenuManager.topicChosen)
        {
            case 1:
                firstLevelPoints.text = PlayerPrefs.GetFloat("FirstTopicLevel1Points").ToString();
                if (PlayerPrefs.GetFloat("FirstTopicLevel1Points") >= mainMenuManager.minPointsLevel1)
                {
                    firstLevelPoints.color = Color.green;
                }
                else
                {
                    firstLevelPoints.color = Color.white;
                }
                fistLevelMinPoint.text = "/" + mainMenuManager.minPointsLevel1;
                secondLevelPoints.text = PlayerPrefs.GetFloat("FirstTopicLevel2Points").ToString();
                if (PlayerPrefs.GetFloat("FirstTopicLevel2Points") >= mainMenuManager.minPointsLevel2)
                {
                    secondLevelPoints.color = Color.green;
                }
                else
                {
                    secondLevelPoints.color = Color.white;
                }
                secondLevelMinPoint.text = "/" + mainMenuManager.minPointsLevel2;
                Debug.Log("caso primo topic punteggio");
                break;
            case 2:
                firstLevelPoints.text = PlayerPrefs.GetFloat("SecondTopicLevel1Points").ToString();
                if (PlayerPrefs.GetFloat("SecondTopicLevel1Points") >= mainMenuManager.minPointsLevel1)
                {
                    firstLevelPoints.color = Color.green;
                }
                else
                {
                    firstLevelPoints.color = Color.white;
                }
                fistLevelMinPoint.text = "/" + mainMenuManager.minPointsLevel1;
                secondLevelPoints.text = PlayerPrefs.GetFloat("SecondTopicLevel2Points").ToString();
                if (PlayerPrefs.GetFloat("SecondTopicLevel2Points") >= mainMenuManager.minPointsLevel2)
                {
                    secondLevelPoints.color = Color.green;
                }
                else
                {
                    secondLevelPoints.color = Color.white;
                }
                secondLevelMinPoint.text = "/" + mainMenuManager.minPointsLevel2;
                Debug.Log("caso secondo topic punteggio");
                break;
            default:
                Debug.Log("no topic");
                break;
        }
    }
}

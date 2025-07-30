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
                if (PlayerPrefs.GetFloat("FirstTopicLevel1Points") >= mainMenuManager.minPointsLv1Top1)
                {
                    firstLevelPoints.color = Color.green;
                }
                else
                {
                    firstLevelPoints.color = Color.white;
                }
                fistLevelMinPoint.text = "/" + mainMenuManager.minPointsLv1Top1;
                secondLevelPoints.text = PlayerPrefs.GetFloat("FirstTopicLevel2Points").ToString();
                if (PlayerPrefs.GetFloat("FirstTopicLevel2Points") >= mainMenuManager.minPointsLv2Top1)
                {
                    secondLevelPoints.color = Color.green;
                }
                else
                {
                    secondLevelPoints.color = Color.white;
                }
                secondLevelMinPoint.text = "/" + mainMenuManager.minPointsLv2Top1;
                Debug.Log("caso primo topic punteggio");
                break;
            case 2:
                firstLevelPoints.text = PlayerPrefs.GetFloat("SecondTopicLevel1Points").ToString();
                if (PlayerPrefs.GetFloat("SecondTopicLevel1Points") >= mainMenuManager.minPointsLv1Top2)
                {
                    firstLevelPoints.color = Color.green;
                }
                else
                {
                    firstLevelPoints.color = Color.white;
                }
                fistLevelMinPoint.text = "/" + mainMenuManager.minPointsLv1Top2;
                secondLevelPoints.text = PlayerPrefs.GetFloat("SecondTopicLevel2Points").ToString();
                if (PlayerPrefs.GetFloat("SecondTopicLevel2Points") >= mainMenuManager.minPointsLv2Top2)
                {
                    secondLevelPoints.color = Color.green;
                }
                else
                {
                    secondLevelPoints.color = Color.white;
                }
                secondLevelMinPoint.text = "/" + mainMenuManager.minPointsLv2Top2;
                Debug.Log("caso secondo topic punteggio");
                break;
            default:
                Debug.Log("no topic");
                break;
        }
    }
}
